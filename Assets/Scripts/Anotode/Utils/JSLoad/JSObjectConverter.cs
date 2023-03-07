using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Puerts;

namespace Anotode.Utils.JSLoad {

	public struct JSObjectEntry {
		public string key;
		public object value;

		public JSObjectEntry(string key, object value) {
			this.key = key;
			this.value = value;
		}
	}

	public class JSObjectConverter {

		public delegate List<JSObjectEntry> ObjectEntryGetter(JSObject obj);

		private static ObjectEntryGetter getEntries;

		private const BindingFlags bindingFlags = BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

		private static readonly Dictionary<IntPtr, object> parsedObjects = new();

		internal static void Init(JsEnv vm) {
			getEntries = vm.ExecuteModule<ObjectEntryGetter>("core/converter.js", "getEntries");

		}

		public static T ConvertInplace<T>(T res, JSObject obj) {
			return (T)ConvertInplace(typeof(T), res, obj);
		}

		public static object ConvertInplace(Type type, object res, JSObject obj) {
			// The type should not be List or Dictionary
			foreach (var e in getEntries(obj)) {
				var field = type.GetField(e.key, bindingFlags);
				if (field != null) {
					field.SetValue(res, GetValue(field.FieldType, e.value));
				} else {
					var property = type.GetProperty(e.key, bindingFlags);
					property?.SetValue(res, GetValue(property.PropertyType, e.value));
				}
			}
			return res;
		}

		public static T Convert<T>(JSObject obj) {
			return (T)Convert(typeof(T), obj);
		}

		/// <summary>
		/// Convert a JSObject to the given CSharp type.
		/// </summary>
		/// <param name="type"></param>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static object Convert(Type type, JSObject obj) {
			if (parsedObjects.TryGetValue(obj.getJsObjPtr(), out var value)) {
				return value;
			}
			if (type == typeof(JSObject)) {
				return obj;
			} else  if (TypeHelper.IsList(type)) {
				return ConvertToList(type, obj);
			} else if (TypeHelper.IsDict(type)) {
				return ConvertToDict(type, obj);
			}
			var res = Activator.CreateInstance(type);
			ConvertInplace(type, res, obj);
			parsedObjects.Add(obj.getJsObjPtr(), res);
			return res;
		}

		public static IList ConvertToList(Type type, JSObject obj) {
			var res = (IList)Activator.CreateInstance(type);
			if (obj != null) {
				var gtype = type.GenericTypeArguments[0];
				foreach (var e in getEntries(obj)) {
					res.Add(GetValue(gtype, e.value));
				}
			}
			return res;
		}

		public static IDictionary ConvertToDict(Type type, JSObject obj) {
			var res = (IDictionary)Activator.CreateInstance(type);
			if (obj != null) {
				var gtype = type.GenericTypeArguments[1];
				foreach (var e in getEntries(obj)) {
					res.Add(e.key, GetValue(gtype, e.value));
				}
			}
			return res;
		}

		private static object GetValue(Type type, object obj) {
			if (obj is JSObject jsobj) {
				return Convert(type, jsobj);
			}
			if (type.BaseType == typeof(Enum)) {
				if (obj is string str) {
					return Enum.Parse(type, str, true);
				}
				return Enum.ToObject(type, System.Convert.ToInt32(obj));
			}
			return System.Convert.ChangeType(obj, type);
		}

		public static void ClearReferences() {
			parsedObjects.Clear();
		}

	}
}

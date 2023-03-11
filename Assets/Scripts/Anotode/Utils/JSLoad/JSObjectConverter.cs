using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
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

		public delegate List<JSObjectEntry> GetEntriesDelegate(JSObject obj);
		private static GetEntriesDelegate getEntries;

		//public delegate Hashtable ToDictDelegate(JSObject obj);
		//private static ToDictDelegate toDict;

		private const BindingFlags bindingFlags = BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

		private static readonly Dictionary<IntPtr, object> parsedObjects = new();

		private static readonly Dictionary<Type, IValueParser> valueInterfaces = new();

		internal static void Init(JsEnv vm) {
			getEntries = vm.ExecuteModule<GetEntriesDelegate>("core/converter.js", "getEntries");
			//toDict = vm.ExecuteModule<ToDictDelegate>("core/converter.js", "toDict");
		}

		public static T ConvertInplace<T>(T res, JSObject obj) {
			return (T)ConvertInplace(typeof(T), res, obj);
		}

		public static object ConvertInplace(Type type, object res, JSObject obj) {
			// The type should not be List or Dictionary
			var dict = getEntries(obj).ToDictionary(t => t.key, t => t.value);
			foreach (var field in type.GetFields(bindingFlags)) {
				if (dict.TryGetValue(GetMemberName(field), out var value)) {
					field.SetValue(res, GetValue(field, field.FieldType, value));
				}
				if (!field.FieldType.IsValueType && field.GetCustomAttribute<AllowNullAttribute>() == null && field.GetValue(res) == null) {
					field.SetValue(res, GetValue(field, field.FieldType, null));
				}
			}
			foreach (var property in type.GetProperties(bindingFlags)) {
				if (property.SetMethod == null) continue;
				if (dict.TryGetValue(GetMemberName(property), out var value)) {
					property.SetValue(res, GetValue(property, property.PropertyType, value));
				}
				if (!property.PropertyType.IsValueType && property.GetCustomAttribute<AllowNullAttribute>() == null && property.GetValue(res) == null) {
					property.SetValue(res, GetValue(property, property.PropertyType, null));
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
			// obj can be null!!!
			if (obj != null && parsedObjects.TryGetValue(obj.getJsObjPtr(), out var value)) {
				return value;
			}
			if (type == typeof(JSObject)) {
				return obj;
			} else if (TypeHelper.IsList(type)) {
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
			var entries = getEntries(obj);
			if (type.IsArray) {
				var etype = type.GetElementType();
				var arr = Array.CreateInstance(etype, entries.Count);
				for (int i = 0; i < entries.Count; i++) {
					arr.SetValue(GetValue(etype, entries[i].value), i);
				}
				return arr;
			} else {
				var list = (IList)Activator.CreateInstance(type);
				var gtype = type.GenericTypeArguments[0];
				foreach (var e in entries) {
					list.Add(GetValue(gtype, e.value));
				}
				return list;
			}
		}

		public static IDictionary ConvertToDict(Type type, JSObject obj) {
			var dict = (IDictionary)Activator.CreateInstance(type);
			if (obj != null) {
				var gtype = type.GenericTypeArguments[1];
				foreach (var e in getEntries(obj)) {
					dict.Add(e.key, GetValue(gtype, e.value));
				}
			}
			return dict;
		}

		private static string GetMemberName(MemberInfo member) {
			var attr = member.GetCustomAttribute<AliasAttribute>();
			return attr?.alias ?? member.Name;
		}

		private static object GetValue(MemberInfo member, Type type, object value) {
			var attr = member.GetCustomAttribute<CustomParserAttribute>();
			if (attr != null) {
				return attr.type.GetMethod("Parse").Invoke(null, new object[] { value });
			} else if (valueInterfaces.TryGetValue(type, out var parser)) {
				return parser.Parse(value as JSObject);
			} else {
				return GetValue(type, value);
			}
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

		public static void SetValueInterface<T>(IValueParser<T> parser) {
			valueInterfaces.Add(typeof(T), parser);
		}
	}
}
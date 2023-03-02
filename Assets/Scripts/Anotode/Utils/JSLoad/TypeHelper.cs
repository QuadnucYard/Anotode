using System;
using System.Collections;
using System.Collections.Generic;

namespace Anotode.Utils.JSLoad {
	internal static class TypeHelper {

		public static bool IsList(Type type) {
			if (typeof(IList).IsAssignableFrom(type)) {
				return true;
			}
			foreach (var it in type.GetInterfaces()) {
				if (it.IsGenericType && typeof(IList<>) == it.GetGenericTypeDefinition())
					return true;
			}
			return false;
		}

		public static bool IsDict(Type type) {
			if (typeof(IDictionary).IsAssignableFrom(type)) {
				return true;
			}
			foreach (var it in type.GetInterfaces()) {
				if (it.IsGenericType && typeof(IDictionary<,>) == it.GetGenericTypeDefinition())
					return true;
			}
			return false;
		}
	}
}

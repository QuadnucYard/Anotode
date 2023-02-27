using System.Collections.Generic;
using System.Linq;

namespace Quadnuc.Utils {
	public static class Enumerables {

		public static string ToString<T>(IEnumerable<T> list) {
			if (list == null) return "null";
			return $"[{string.Join(',', list.Select(x => x.ToString()))}]";
		}

		public static bool IsNullOrEmpty<T>(IEnumerable<T> first) {
			return first == null || first.Count() == 0;
		}

	}
}

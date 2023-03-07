using System.Collections.Generic;
using System.Linq;

namespace Quadnuc.Utils {
	public static class Enumerables {

		public static string ToString<T>(IEnumerable<T> list) {
			if (list == null) return "null";
			return $"[{string.Join(',', list.Select(x => x.ToString()))}]";
		}

		public static bool IsNullOrEmpty<T>(this IEnumerable<T> first) {
			return first == null || first.Count() == 0;
		}

		public static T[,] ToArray2D<T>(this IEnumerable<IEnumerable<T>> list) {
			int n = list.Count(), m = list.ElementAt(0).Count();
			var array = new T[n, m];
			int i = 0;
			foreach (var a in list) {
				int j = 0;
				foreach (var b in a) {
					array[i, j++] = b;
				}
				i++;
			}
			return array;
		}

		public static T[] ToArraySafe<T>(this IEnumerable<T> e) {
			return e == null ? new T[0] : e.ToArray();
		}

		public static List<T> ToListSafe<T>(this IEnumerable<T> e) {
			return e==null ? new() : e.ToList();
		}

	}
}

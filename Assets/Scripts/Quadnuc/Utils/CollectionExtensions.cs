using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Quadnuc.Utils {
	public static class CollectionExtensions {

		#region General Lists


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Empty<T>(this IEnumerable<T> collection) {
			return collection.Count() == 0;
		}

		public static T Pop<T>(this List<T> list) {
			T tmp = list[^1];
			list.RemoveAt(list.Count - 1);
			return tmp;
		}

		public static T Shift<T>(this List<T> list) {
			T tmp = list[0];
			list.RemoveAt(0);
			return tmp;
		}

		public static int IndexOf<T>(this IEnumerable<T> e, Func<T, bool> pred) {
			int i = 0;
			foreach (var t in e) {
				if (pred(t)) return i;
				i++;
			}
			return -1;
		}

		public static void ForEach<T>(this IEnumerable<T> self, Action<T> action) {
			foreach (var p in self) action(p);
		}

		public static void ForEach<T>(this IEnumerable<T> self, Action<int, T> action) {
			int i = 0;
			foreach (var p in self) {
				action(i, p);
				i++;
			}
		}

		public static float Product<T>(this IEnumerable<T> source, Func<T, float> pred, float initial = 1) {
			float cur = initial;
			foreach (T p in source) {
				cur *= pred(p);
			}
			return cur;
		}

		public static T MaxBy<T, TR>(this IEnumerable<T> en, Func<T, TR> evaluate) where TR : IComparable<TR> {
			return en.Select(t => new Tuple<T, TR>(t, evaluate(t)))
				.Aggregate((max, next) => next.Item2.CompareTo(max.Item2) > 0 ? next : max).Item1;
		}

		public static T MinBy<T, TR>(this IEnumerable<T> en, Func<T, TR> evaluate) where TR : IComparable<TR> {
			return en.Select(t => new Tuple<T, TR>(t, evaluate(t)))
				.Aggregate((max, next) => next.Item2.CompareTo(max.Item2) < 0 ? next : max).Item1;
		}

		public static IEnumerable<TSource> ExceptOrDefault<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second) {
			return second == null ? first : first.Except(second);
		}

		public static List<T> Clone<T>(this IList<T> list) {
			List<T> clone = new();
			clone.AddRange(list);
			return clone;
		}

		public static T[] Clone<T>(this T[] list) {
			T[] clone = new T[list.Length];
			list.CopyTo(clone, 0);
			return clone;
		}

		public static IEnumerable<T> Reversed<T>(this IList<T> list) {
			int len = list.Count;
			for (int i = len - 1; i >= 0; i--) {
				yield return list[i];
			}
		}

		/// <summary>
		/// 降序插入
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <param name="item"></param>
		/// <returns></returns>
		public static int InsertDescending<T>(this List<T> list, T item) where T : IComparable<T> {
			int i = 0, count = list.Count;
			while (i < count && list[i].CompareTo(item) == 1) i++;
			list.Insert(i, item);
			return i;
		}

		public static int InsertDescending<T, U>(this List<T> list, T item, Func<T, U> keySelector) where U : IComparable<U> {
			int i = 0, count = list.Count;
			while (i < count && keySelector(list[i]).CompareTo(keySelector(item)) == 1) i++;
			list.Insert(i, item);
			return i;
		}

		public static int InsertAscending<T>(this List<T> list, T item) where T : IComparable<T> {
			int i = 0, count = list.Count;
			while (i < count && list[i].CompareTo(item) == -1) i++;
			list.Insert(i, item);
			return i;
		}

		public static int InsertAscending<T, U>(this List<T> list, T item, Func<T, U> keySelector) where U : IComparable<U> {
			int i = 0, count = list.Count;
			while (i < count && keySelector(list[i]).CompareTo(keySelector(item)) == -1) i++;
			list.Insert(i, item);
			return i;
		}

		#endregion

		#region Arrays

		public static U[] Like<T, U>(this T[] arr) {
			return new U[arr.GetLength(0)];
		}

		public static U[,] Like<T, U>(this T[,] arr) {
			return new U[arr.GetLength(0), arr.GetLength(1)];
		}

		public static T[] Fill<T>(this T[] array, T x) {
			for (int i = 0; i < array.Length; ++i) array[i] = x;
			return array;
		}

		public static T[,] Fill<T>(this T[,] array, T x) {
			int n = array.GetLength(0), m = array.GetLength(1);
			for (int i = 0; i < n; ++i)
				for (int j = 0; j < m; ++j)
					array[i, j] = x;
			return array;
		}

		public static T[] Generate<T>(this T[] array, Func<int, T> gen) {
			for (int i = 0; i < array.Length; ++i) array[i] = gen(i);
			return array;
		}

		public static T[,] Generate<T>(this T[,] array, Func<int, int, T> gen) {
			int n = array.GetLength(0), m = array.GetLength(1);
			for (int i = 0; i < n; ++i)
				for (int j = 0; j < m; ++j)
					array[i, j] = gen(i, j);
			return array;
		}

		public static U[] Map<T, U>(this T[] array, Func<T, U> gen) {
			var ret = array.Like<T, U>();
			for (int i = 0; i < array.Length; ++i) ret[i] = gen(array[i]);
			return ret;
		}

		public static U[] Map<T, U>(this T[] array, Func<T, int, U> gen) {
			var ret = array.Like<T, U>();
			for (int i = 0; i < array.Length; ++i) ret[i] = gen(array[i], i);
			return ret;
		}

		public static U[,] Map<T, U>(this T[,] array, Func<T, U> gen) {
			var ret = array.Like<T, U>();
			int n = array.GetLength(0), m = array.GetLength(1);
			for (int i = 0; i < n; ++i)
				for (int j = 0; j < m; ++j)
					ret[i, j] = gen(array[i, j]);
			return ret;
		}

		public static U[,] Map<T, U>(this T[,] array, Func<T, int, int, U> gen) {
			var ret = array.Like<T, U>();
			int n = array.GetLength(0), m = array.GetLength(1);
			for (int i = 0; i < n; ++i)
				for (int j = 0; j < m; ++j)
					ret[i, j] = gen(array[i, j], i, j);
			return ret;
		}

		public static void CopyRow<T>(this T[,] array, int src, int dst) {
			int m = array.GetLength(1);
			for (int i = 0; i < m; i++) {
				array[dst, i] = array[src, i];
			}
		}

		public static void FillRow<T>(this T[,] array, int row, T x) {
			int m = array.GetLength(1);
			for (int j = 0; j < m; ++j)
				array[row, j] = x;
		}

		public static void ForEach<T>(this T[,] arr, Action<T, int, int> action) {
			int n = arr.GetLength(0), m = arr.GetLength(1);
			for (int i = 0; i < n; i++) {
				for (int j = 0; j < m; j++) {
					action(arr[i, j], i, j);
				}
			}
		}

		public static void ForEach<T>(this T[,] arr, Action<T> action) {
			int n = arr.GetLength(0), m = arr.GetLength(1);
			for (int i = 0; i < n; i++) {
				for (int j = 0; j < m; j++) {
					action(arr[i, j]);
				}
			}
		}

		public static T[,] Resized<T>(this T[,] arr, int n, int m) {
			int n0 = arr.GetLength(0), m0 = arr.GetLength(1);
			if (n0 == n && m0 == m) return arr;
			T[,] brr = new T[n, m];
			for (int i = 0; i < n; i++) {
				for (int j = 0; j < m; j++) {
					brr[i, j] = i < n0 && j < m0 ? arr[i, j] : default;
				}
			}
			return brr;
		}

		public static T[,] Transposed<T>(this T[,] arr) {
			int n = arr.GetLength(0), m = arr.GetLength(1);
			T[,] brr = new T[m, n];
			for (int i = 0; i < m; i++) {
				for (int j = 0; j < n; j++) {
					brr[i, j] = arr[j, i];
				}
			}
			return brr;
		}

		public static bool IsomorphicTo<T>(this T[,] arr, T[,] brr) {
			return arr.GetLength(0) == brr.GetLength(0) && arr.GetLength(1) == brr.GetLength(1);
		}

		public static bool IsSizeOf<T>(this T[,] arr, int n, int m) {
			return arr.GetLength(0) == n && arr.GetLength(1) == m;
		}

		public static IEnumerable<T> Where<T>(this T[,] arr, Func<T, int, int, bool> selector) {
			return from i in Enumerable.Range(0, arr.GetLength(0))
				   from j in Enumerable.Range(0, arr.GetLength(1))
				   where selector(arr[i, j], i, j)
				   select arr[i, j];
		}

		public static IEnumerable<U> Select<T, U>(this T[,] arr, Func<T, U> selector) {
			return from i in Enumerable.Range(0, arr.GetLength(0))
				   from j in Enumerable.Range(0, arr.GetLength(1))
				   select selector(arr[i, j]);
		}

		public static IEnumerable<U> Select<T, U>(this T[,] arr, Func<T, int, int, U> selector) {
			return from i in Enumerable.Range(0, arr.GetLength(0))
				   from j in Enumerable.Range(0, arr.GetLength(1))
				   select selector(arr[i, j], i, j);
		}

		public static IEnumerator<T> GetRowEnumerator<T>(this T[,] arr, int row) {
			int m = arr.GetLength(1);
			for (int i = 0; i < m; i++) {
				yield return arr[row, i];
			}
		}

		#endregion

	}
}

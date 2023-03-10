namespace Quadnuc.Utils {
	public static class FuncTools {

		public static void NoAction() { }
		public static void NoAction<T>(T _) { }
		public static void NoAction<T1, T2>(T1 _1, T2 _2) { }

		public static TResult Default<TResult>() => default;
		public static TResult Default<T, TResult>(T _) => default;
		public static TResult Default<T1, T2, TResult>(T1 _1, T2 _2) => default;
		public static TResult Default<T1, T2, T3, TResult>(T1 _1, T2 _2, T3 _3) => default;

		public static T Identity<T>(T t) => t;

		//public static Func<T, T> identity<T, TResult>(this Func<T, TResult> _) => Identity;

		public static bool Tautology() => true;
		public static bool Tautology<T>(T _) => true;
		public static bool Contradiction() => false;

	}
}

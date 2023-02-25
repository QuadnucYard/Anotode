public static class BasicExtension {

	public static string ToFixed(this float x, int n) => x.ToString($"f{n}");
}

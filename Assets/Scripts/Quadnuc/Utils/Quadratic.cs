namespace Quadnuc.Utils {
	public struct Quadratic {
		public float a, b, c;

		public float Eval(float x) {
			return (a * x + b) * x + c;
		}
	}
}

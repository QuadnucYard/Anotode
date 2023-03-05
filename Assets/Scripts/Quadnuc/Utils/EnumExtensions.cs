using System;

namespace Quadnuc.Utils {
	public static class EnumExtensions {

		public static bool Any(this Enum e) {
			return Convert.ToInt32(e) != 0;
		}

	}
}

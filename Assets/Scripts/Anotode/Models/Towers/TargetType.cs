namespace Anotode.Models.Towers {
	public enum TargetType {
		None,
		First,
		Last,
		Close,
		Far,
		Strong,
		Weak,
		Random,
		Any,
	}

	public static class TargetTypeExtension {
		public static bool IsActionable(this TargetType targetType) {
			return targetType != TargetType.None;
		}
		public static bool ActionOnCreate(this TargetType targetType) {
			return true;
		}
	}
}

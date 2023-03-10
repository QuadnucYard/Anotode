using System;

namespace Anotode.Models.Towers {
	public struct TargetType : IEquatable<TargetType> {

		public static readonly TargetType None = new("None", false);
		public static readonly TargetType First = new("First");
		public static readonly TargetType Last = new("Last");
		public static readonly TargetType Close = new("Close");
		public static readonly TargetType Far = new("Far");
		public static readonly TargetType Strong = new("Strong");
		public static readonly TargetType Random = new("Random");
		public static readonly TargetType Any = new("Any");

		public readonly string id;
		public readonly bool isActionable;
		public readonly bool actionOnCreate;
		public readonly int index;

		private static int indexCount = 0;

		public TargetType(string id, bool isActionable = true, bool actionOnCreate = false) {
			this.id = id;
			this.isActionable = isActionable;
			this.actionOnCreate = actionOnCreate;
			index = indexCount++;
		}

		public bool Equals(TargetType other) => index == other.index;
	}
}

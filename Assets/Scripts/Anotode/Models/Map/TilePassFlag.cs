using System;

namespace Anotode.Models.Map {

	/// <summary>
	/// Describe which type is allowed.
	/// </summary>
	[Flags]
	public enum TilePassFlag {
		None = 0,
		Land = 1,
		Air = 2,
		Water = 4,
	}
}

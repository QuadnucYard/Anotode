using System;

namespace Anotode.Models.Map {

	[Serializable]
	public struct TileInfo {

		public static TileInfo Void;

		public TileType type;
		public int height;

		public TileInfo(TileType type) {
			this.type = type;
			height = 0;
		}

	}
}

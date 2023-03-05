using System;

namespace Anotode.Models.Map {

	[Serializable]
	public class TileModel : Model {

		public string id;
		public int index;
		public TileType type;
		public int height;

		public override Model Clone() {
			return (TileModel)MemberwiseClone();
		}
	}
}

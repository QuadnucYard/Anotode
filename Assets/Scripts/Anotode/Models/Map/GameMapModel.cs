using System;

namespace Anotode.Models.Map {

	[Serializable]
	public class GameMapModel : Model {

		public TiledAreaModel[] tiledAreas;

		public override Model Clone() {
			throw new NotImplementedException();
		}
	}
}

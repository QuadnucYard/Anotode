using System;

namespace Anotode.Models.Map {

	[Serializable]
	public class GameMapModel : Model {

		public string id;
		public TiledAreaModel[] tiledAreas;


		public override Model Clone() {
			throw new NotImplementedException();
		}
	}
}

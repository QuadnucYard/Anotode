using System;

namespace Anotode.Models.Map {

	[Serializable]
	public class LevelModel : Model {

		public string id;
		public string mapId;
		public float hardnessA;
		public float hardnessB;
		public float spawnInterval;
		public int splitRule;
		public int populationMax;

		public override Model Clone() {
			throw new NotImplementedException();
		}
	}
}

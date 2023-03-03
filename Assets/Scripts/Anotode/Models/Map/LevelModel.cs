using System;
using Anotode.Models.Waves;

namespace Anotode.Models.Map {

	[Serializable]
	public class LevelModel : Model {

		public string id;
		public string mapId;
		public float hardnessA;
		public float hardnessB;
		public float spawnInterval;
		public int splitRule;

		public WaveModel[] waves;
		public EnemyGroupModel enemyGroup;

		public GameMapModel map;

		public override Model Clone() {
			throw new NotImplementedException();
		}
	}
}

using Anotode.Models.Map;

namespace Anotode.Data.Maps {
	public class LevelData {
		readonly string id;
		readonly string mapId;
		readonly float hardnessA;
		readonly float hardnessB;
		readonly float spawnInterval;
		readonly int splitRule;
		readonly int populationMax;

		public LevelModel def => new() {
			id = id,
			mapId = mapId,
			hardnessA = hardnessA,
			hardnessB = hardnessB,
			spawnInterval = spawnInterval,
			splitRule = splitRule,
			populationMax = populationMax
		};
	}
}

using Anotode.Models.Map;
using Anotode.Models.Waves;

namespace Anotode.Data.Maps {

	public class LevelData {
		readonly string id;
		readonly string mapId;
		readonly float hardnessA;
		readonly float hardnessB;
		readonly float spawnInterval;
		readonly int splitRule;

		readonly WaveModel[] waves;
		readonly EnemyGroupModel enemyGroup;

		public LevelModel def => new() {
			id = id,
			mapId = mapId,
			hardnessA = hardnessA,
			hardnessB = hardnessB,
			spawnInterval = spawnInterval,
			splitRule = splitRule,
			waves = waves,
			enemyGroup = enemyGroup,
			map = GameDataManager.getMap(mapId),
		};
	}
}

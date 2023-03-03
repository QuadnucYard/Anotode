using System.Linq;
using Anotode.Models;
using Anotode.Models.Map;

namespace Anotode.Data {
	public class GameDataManager {

		private static GameMapModel[] _allMaps;
		public static GameMapModel[] allMaps => _allMaps ??= GameData.instance.maps.Select(t => t.def).ToArray();


		private static LevelModel[] _allLevels;
		public static LevelModel[] allLevels => _allLevels ??= GameData.instance.levels.Select(t => t.def).ToArray();

		public static EnemyModel[] allEnemies { get; } = GameData.instance.enemies.Select(t => t.def).ToArray();

		public static LevelModel getLevel(string id) {
			return allLevels.First(t => t.id == id);
		}

		public static GameMapModel getMap(string id) {
			return allMaps.First(t => t.id == id);
		}

	}
}

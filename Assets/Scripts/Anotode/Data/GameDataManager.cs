using System.Linq;
using Anotode.Data.Maps;
using Anotode.Models;
using Anotode.Models.Map;
using Anotode.Models.Towers;

namespace Anotode.Data {
	public class GameDataManager {
		// 注意下面初始化是按顺序的！！！
		public static TileModel[] allTiles { get; } = GameData.instance.tiles.Select(t => t.def).ToArray();

		public static GameMapModel[] allMaps { get; } = GameData.instance.maps.Select(t => t.def).ToArray();

		public static LevelModel[] allLevels { get; } = GameData.instance.levels.Select(t => t.def).ToArray();

		public static EnemyModel[] allEnemies { get; } = GameData.instance.enemies.Select(t => t.def).ToArray();

		public static TowerModel[] allTowers { get; } = GameData.instance.towers.Select(t => t.def).ToArray();


		public static LevelModel getLevel(string id) {
			return allLevels.First(t => t.id == id);
		}

		public static GameMapModel getMap(string id) {
			return allMaps.First(t => t.id == id);
		}

		public static TileModel GetTile(int index) {
			return allTiles.First(t => t.index == index);
		}

		public static TileData GetTileData(int index) {
			return GameData.instance.tiles.Find(t => t.index == index);
		}

	}
}

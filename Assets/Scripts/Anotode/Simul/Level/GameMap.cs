using System.Collections.Generic;
using System.Linq;
using Anotode.Models.Map;
using Anotode.Models.Waves;
using Anotode.Simul.Enemies;
using Anotode.Simul.Objects;
using Anotode.Simul.Towers;
using Quadnuc.Utils;
using Vector2 = UnityEngine.Vector2;
using Vector2Int = UnityEngine.Vector2Int;

namespace Anotode.Simul.Level {
	public class GameMap : Simulatable {

		public GameMapModel mapModel;
		public List<TiledArea> areas;
		public Pathfinding pathfinding = new();

		public PositionInArea[] areaEntrances;
		public PositionInArea[] areaExits;

		public Dictionary<ObjectId, List<Tower>> areaTowers;
		public Dictionary<ObjectId, List<Enemy>> areaEnemies;
		// areaEnemies;

		public TiledArea mainArea => areas[0];

		public GameMap(GameMapModel mapModel) {
			this.mapModel = mapModel;
		}

		public void Init() {
			areas = mapModel.tiledAreas.Select((t, i) => {
				var a = new TiledArea(t) { index = i, map = this, sim = sim };
				a.Init();
				return a;
			}).ToList();
			areaEntrances = areas.SelectMany(t => t.areaModel.entrances.Map(s => new PositionInArea() { area = t, pos = s })).ToArray();
			areaExits = areas.SelectMany(t => t.areaModel.exits.Map(s => new PositionInArea() { area = t, pos = s })).ToArray();
			areaTowers = areas.ToDictionary(t => t.id, t => new List<Tower>());
			areaEnemies = areas.ToDictionary(t => t.id, t => new List<Enemy>());
		}

		public TiledArea GetAreaById(ObjectId id) {
			return areas.Find(t => t.id == id);
		}

		public TiledArea GetAreaAtPoint(Vector2 mapPos) {
			return areas.LastOrDefault(a => a.ContainsMapPoint(mapPos));
		}

		public void AddEnemy(Enemy enemy) {
			areaEnemies[enemy.areaIdIn].Add(enemy);
		}

		public void RemoveEnemy(Enemy enemy) {
			areaEnemies[enemy.areaIdIn].Remove(enemy);
		}

		public struct PositionInArea {
			public TiledArea area;
			public Vector2Int pos;
		}
	}
}

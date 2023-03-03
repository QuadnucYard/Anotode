using System.Collections.Generic;
using System.Linq;
using Anotode.Models.Map;
using Anotode.Models.Waves;
using Anotode.Simul.Objects;
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

		// public Dictionary<ObjectId, List<Tower>> areaTowers;
		public Dictionary<TiledArea, List<Enemy>> areaEnemies;
		// areaEnemies;

		public TiledArea mainArea => areas[0];

		public GameMap(GameMapModel mapModel) {
			this.mapModel = mapModel;
			areas = mapModel.tiledAreas.Select(t => new TiledArea(t) { map = this }).ToList();
		}

		public void Init() {
			areaEntrances = areas.SelectMany(t => t.areaModel.entrances.Map(s => new PositionInArea() { area = t, pos = s })).ToArray();
			areaExits = areas.SelectMany(t => t.areaModel.exits.Map(s => new PositionInArea() { area = t, pos = s })).ToArray();
			areaEnemies = new();
			foreach (var a in areas) areaEnemies.Add(a, new());
		}

		public struct PositionInArea {
			public TiledArea area;
			public Vector2Int pos;
		}
	}
}

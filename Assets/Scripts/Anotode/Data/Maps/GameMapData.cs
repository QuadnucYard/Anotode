using System.Collections.Generic;
using System.Linq;
using Anotode.Models.Map;
using Quadnuc.Utils;
using UnityEngine;

namespace Anotode.Data.Maps {

	public class AreaData {
		readonly List<List<int>> tiles;
		readonly List<Vector2Int> entrances;
		readonly List<Vector2Int> exits;

		public TiledAreaModel def => new() {
			tiles = Enumerables.ToArray2D(tiles.Reversed().Select(t => t.Select(t => new TileInfo() { type = (TileType)t }))).Transposed(),
			entrances = entrances.ToArray(),
			exits = exits.ToArray(),
		};
	}

	public class GameMapData {
		readonly string id;
		readonly List<AreaData> areas;

		public GameMapModel def => new() {
			id = id,
			tiledAreas = areas.Select(a => a.def).ToArray(),
		};
	}

}

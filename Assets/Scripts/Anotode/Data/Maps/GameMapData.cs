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

		readonly Vector2 pivotPoint;
		readonly Vector2 position;

		public TiledAreaModel def => new() {
			tiles = Enumerables.ToArray2D(tiles.Reversed()).Transposed()
				.Map((t, i, j) => (TileModel)GameDataManager.GetTile(t).Clone()),
			entrances = entrances.ToArray(),
			exits = exits.ToArray(),
			pivotPoint = pivotPoint,
			position = position,
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

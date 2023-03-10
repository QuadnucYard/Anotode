using System.Collections.Generic;
using System.Linq;
using Anotode.Models;
using Anotode.Models.Map;
using Puerts;
using Quadnuc.Utils;
using UnityEngine;

namespace Anotode.Data.Maps {
	public class AreaData {
		readonly List<List<int>> tiles;

		readonly List<Vector2Int> entrances;
		readonly List<Vector2Int> exits;

		readonly Vector2 pivotPoint;
		readonly Vector2 position;

		readonly List<JSObject> behaviors;

		public TiledAreaModel def => new() {
			tiles = tiles.Reversed().ToArray2D().Transposed()
				.Map((t, i, j) => (TileModel)GameDataManager.GetTile(t).Clone()),
			entrances = entrances.ToArraySafe(),
			exits = exits.ToArraySafe(),
			pivotPoint = pivotPoint,
			position = position,
			behaviors = AdaptedBehaviorModel.Create(behaviors).ToArray(),
		};
	}

}

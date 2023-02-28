using Anotode.Models.Map;
using Quadnuc.Utils;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Anotode.Display.Map {
	public class GameMap : MonoBehaviour {

		public GameMapModel mapModel;
		public Tilemap tilemap;
		public TileBase tile;

		public void CreateMap(GameMapModel mapModel) {
			this.mapModel = mapModel;
			var area = mapModel.tiledAreas[0];
			area.tiles.ForEach((t, i, j) => {
				if (t.type == TileType.Land) tilemap.SetTile(new(i, j, 0), tile);
			});
		}

	}
}

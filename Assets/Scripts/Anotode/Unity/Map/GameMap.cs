using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anotode.Models.Map;
using Quadnuc.Utils;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Anotode.Unity.Map {
	public class GameMap : MonoBehaviour {

		public GameMapModel mapModel;
		public Tilemap tilemap;
		public TileBase tile;

		public void CreateMap() {
			var area = mapModel.tiledAreas[0];
			area.tiles.ForEach((t, i, j) => {
				if (t.type == TileType.Land) tilemap.SetTile(new(i, j, 0), tile);
			});
		}

	}
}

using System;
using Vector2 = UnityEngine.Vector2;

namespace Anotode.Models.Map {

	[Serializable]
	public class TiledAreaModel : Model {

		public float height;

		public Vector2 pivotPoint;

		/// <summary> Tiles stored in (x, y) order. </summary>
		public TileInfo[,] tiles;

		public int xGrid => tiles.GetLength(0);
		public int yGrid => tiles.GetLength(1);

		public override Model Clone() {
			throw new System.NotImplementedException();
		}

		public bool ContainsPoint(Vector2 point) {
			return point.x >= 0 && point.y >= 0 && point.x < xGrid && point.y < yGrid;
		}

	}
}

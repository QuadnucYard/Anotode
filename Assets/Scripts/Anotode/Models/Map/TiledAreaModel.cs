﻿using System;
using System.Numerics;
using Vector2 = UnityEngine.Vector2;
using Vector2Int = UnityEngine.Vector2Int;

namespace Anotode.Models.Map {

	[Serializable]
	public class TiledAreaModel : Model {

		public float height;

		public Vector2 pivotPoint;
		public Vector2 position;

		/// <summary> Tiles stored in (x, y) order. </summary>
		public TileModel[,] tiles;

		public Vector2Int[] entrances;
		public Vector2Int[] exits;

		public int xGrid => tiles.GetLength(0);
		public int yGrid => tiles.GetLength(1);

		public override Model Clone() {
			return MemberwiseClone() as TiledAreaModel;
		}

		public bool ContainsPoint(Vector2 point) {
			return point.x >= 0 && point.y >= 0 && point.x < xGrid && point.y < yGrid;
		}

		public bool ContainsPointGlobal(Vector2 point) {
			return ContainsPoint(point + pivotPoint - position);
		}
	}
}

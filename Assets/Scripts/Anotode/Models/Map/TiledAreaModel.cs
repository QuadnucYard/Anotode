﻿using System;
using Quadnuc.Utils;
using Vector2 = UnityEngine.Vector2;
using Vector2Int = UnityEngine.Vector2Int;

namespace Anotode.Models.Map {

	[Serializable]
	public class TiledAreaModel : Model {

		public float height;

		public Vector2 pivotPoint;
		public Vector2 position; // 现在它是原始位置

		/// <summary> Tiles stored in (x, y) order. </summary>
		public TileModel[,] tiles;

		public Vector2Int[] entrances;
		public Vector2Int[] exits;

		public int xGrid => tiles.GetLength(0);
		public int yGrid => tiles.GetLength(1);

		public override Model Clone() {
			return MemberwiseClone() as TiledAreaModel;
		}

		public bool ContainsPoint(Vector2 point)
			=> point.x >= 0 && point.y >= 0 && point.x < xGrid && point.y < yGrid;

		public bool ContainsMapPoint(Vector2 point)
			=> ContainsPoint(point + pivotPoint - position);

		public Vector2 CellToLocal(Vector2Int pos)
			=> new(pos.x + 0.5f, pos.y + 0.5f);

		public Vector2 LocalToMap(Vector2 pos)
			=> pos - pivotPoint;

		public Vector2Int LocalToCell(Vector2 pos)
			=> pos.FloorToInt();

		public Vector2 MapToLocal(Vector2 pos)
			=> pos + pivotPoint;

	}
}

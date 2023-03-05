using System;
using Anotode.Models.Map;
using Quadnuc.Utils;
using UnityEngine.UIElements;
using Mathf = UnityEngine.Mathf;
using Vector2 = UnityEngine.Vector2;
using Vector2Int = UnityEngine.Vector2Int;

namespace Anotode.Simul.Level {

	public class TiledArea {

		public GameMap map;

		public readonly TiledAreaModel areaModel;
		public readonly TiledAreaFrame frame;

		public TiledArea(TiledAreaModel model) {
			this.areaModel = model;
			frame = new(this);
		}

		public void UpdateFrame() {
			frame.ClearCostMap();
			// 定义自然一些，charm为正表示吸引。
			static int charmFunc(int a, float d) => -Mathf.FloorToInt(a / (d * d + 1));
			foreach (var enemy in map.areaEnemies[this]) {
				var pos = enemy.localPos;
				for (int i = 0; i < areaModel.xGrid; i++) {
					for (int j = 0; j < areaModel.yGrid; j++) {
						frame.charmMap[i, j] += charmFunc(enemy.enemyModel.charm, Mathh.Hypot(i + 0.5f - pos.x, j + 0.5f - pos.y));
					}
				}
			}
		}


		public Vector2 CellToLocal(Vector2Int pos)
			=> areaModel.CellToLocal(pos);

		public Vector2 LocalToGlobal(Vector2 pos)
			=> areaModel.LocalToGlobal(pos);

		public Vector2Int LocalToCell(Vector2 pos)
			=> areaModel.LocalToCell(pos);

		public Vector2 GlobalToLocal(Vector2 pos)
			=> areaModel.GlobalToLocal(pos);

		public Vector2 CellToGlobal(Vector2Int pos)
			=> areaModel.CellToGlobal(pos);

		public Vector2Int GlobalToCell(Vector2 pos)
			=> areaModel.GlobalToCell(pos);


	}
}

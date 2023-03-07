using Anotode.Models.Map;
using Anotode.Simul.Objects;
using Quadnuc.Utils;
using Mathf = UnityEngine.Mathf;
using Vector2 = UnityEngine.Vector2;
using Vector2Int = UnityEngine.Vector2Int;

namespace Anotode.Simul.Level {

	public class TiledArea : Simulatable, IProcessable {

		public GameMap map;

		public readonly TiledAreaModel areaModel;
		public readonly TiledAreaFrame frame;

		private Vector2 position;

		public TiledArea(TiledAreaModel model) {
			this.areaModel = model;
			frame = new(this);
			displayNode = new("TiledArea");
			displayNode.Create();
		}

		public void Process() {
			// 问题来了，model要和node同步
			// 但很多时候model不需要知道position
			// 所以在这里两个都设置一下应该问题不大
			// 需要记录原始位置
			displayNode.position = position = areaModel.position + Vector2.up * Mathf.Sin(sim.timer.time / 10.0f / areaModel.yGrid);
			displayNode.Update();
			UpdateFrame();
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

		public Vector2 LocalToMap(Vector2 pos)
			=> areaModel.LocalToMap(pos + position);

		public Vector2Int LocalToCell(Vector2 pos)
			=> areaModel.LocalToCell(pos);

		public Vector2 MapToLocal(Vector2 pos)
			=> areaModel.MapToLocal(pos - position);

		public Vector2 CellToMap(Vector2Int pos)
			=> LocalToMap(CellToLocal(pos));

		public Vector2Int MapToCell(Vector2 pos)
			=> LocalToCell(MapToLocal(pos));


	}
}

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Anotode.Display.VM;
using Anotode.Models.Map;
using Anotode.Simul.Level.Areas;
using Anotode.Simul.Objects;
using Quadnuc.Utils;
using Mathf = UnityEngine.Mathf;
using Vector2 = UnityEngine.Vector2;
using Vector2Int = UnityEngine.Vector2Int;

namespace Anotode.Simul.Level {

	public class TiledArea : Simulatable, IProcessable {

		public int index;
		public GameMap map;

		public readonly TiledAreaModel areaModel;
		public readonly TiledAreaFrame frame;

		public Vector2 position { get; set; }

		public List<AreaBehavior> areaBehaviors;

		public DisplayNode subDisplayNode { get; set; }

		public TiledArea(TiledAreaModel model) {
			areaModel = model;
			frame = new(this);

			position = model.position;

			displayNode = new("TiledArea");
			displayNode.Create();
			subDisplayNode = displayNode.BindChild("Tilemap");
		}

		public void Init() {
			areaBehaviors = BehaviorProxyFactory.CreateFromModels<AreaBehavior>(areaModel.behaviors, sim);
			areaBehaviors.ForEach(t => t.area = this);
		}

		public void Process() {
			areaBehaviors.ForEach(t => t.onUpdate?.Invoke());
			displayNode.position = position;
			displayNode.Update();
			UpdateFrame();
		}

		public void UpdateFrame() {
			frame.ClearCostMap();
			// 定义自然一些，charm为正表示吸引。
			static int charmFunc(int a, float d) => -Mathf.FloorToInt(a / (d * d + 1));
			foreach (var enemy in map.areaEnemies[id]) {
				var pos = enemy.localPos;
				for (int i = 0; i < areaModel.xGrid; i++) {
					for (int j = 0; j < areaModel.yGrid; j++) {
						frame.charmMap[i, j] += charmFunc(enemy.enemyModel.charm, Mathh.Hypot(i + 0.5f - pos.x, j + 0.5f - pos.y));
					}
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool ContainsMapPoint(Vector2 point)
			=> areaModel.ContainsPoint(point + areaModel.pivotPoint - position);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector2 CellToLocal(Vector2Int pos)
			=> new(pos.x + 0.5f, pos.y + 0.5f);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector2 LocalToMap(Vector2 pos)
			=> pos + position - areaModel.pivotPoint;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector2Int LocalToCell(Vector2 pos)
			=> pos.FloorToInt();

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector2 MapToLocal(Vector2 pos)
			=> pos - position + areaModel.pivotPoint;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector2 CellToMap(Vector2Int pos)
			=> LocalToMap(CellToLocal(pos));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector2Int MapToCell(Vector2 pos)
			=> LocalToCell(MapToLocal(pos));


	}
}

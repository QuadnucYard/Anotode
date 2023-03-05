using Anotode.Models.Map;
using Quadnuc.Utils;

namespace Anotode.Simul.Level {
	/// <summary>
	/// 用于渲染pass和cost
	/// </summary>
	public class TiledAreaFrame {

		public readonly TiledArea area;
		public readonly TilePassFlag[,] passMap;
		public readonly int[,] costMap;

		public int xGrid => area.areaModel.xGrid;
		public int yGrid => area.areaModel.yGrid;

		public TiledAreaFrame(TiledArea area) {
			this.area = area;
			passMap = area.areaModel.tiles.Map(t => t.type == TileType.Land ? TilePassFlag.Land : TilePassFlag.None);
			costMap = area.areaModel.tiles.Like<TileModel, int>();
		}

		public void ClearCostMap() {
			costMap.Fill(0);
		}
	}
}

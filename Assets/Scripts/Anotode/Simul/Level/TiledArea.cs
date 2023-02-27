using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
			costMap = area.areaModel.tiles.Like<TileInfo, int>();
		}
	}

	public class TiledArea {

		public readonly TiledAreaModel areaModel;
		public readonly TiledAreaFrame frame;

		public TiledArea(TiledAreaModel model) {
			this.areaModel = model;
			frame = new(this);
		}

		public void UpdateFrame() {

		}

	}
}

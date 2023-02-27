using System.Collections.Generic;
using Anotode.Models.Map;

namespace Anotode.Simul.Level {
	public class GameMap {

		public GameMapModel mapModel;
		public List<TiledArea> areas;
		public Pathfinding pathfinding;
	}
}

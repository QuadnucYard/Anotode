using System.Collections.Generic;
using System.Linq;
using Anotode.Models.Map;
using Quadnuc.Utils;

namespace Anotode.Data.Maps {

	public class GameMapData {
		readonly string id;
		readonly List<AreaData> areas;

		public GameMapModel def => new() {
			id = id,
			tiledAreas = areas.Select(a => a.def).ToArray(),
		};
	}

}

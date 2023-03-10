using System.Collections.Generic;
using System.Linq;
using Anotode.Models;
using Anotode.Models.Towers;
using Puerts;
using Quadnuc.Utils;

namespace Anotode.Data.Towers {
	public class TowerData {

		readonly string id;
		readonly string baseId;
		readonly SpriteData icon;
		readonly SpriteData sprite;

		readonly List<JSObject> behaviors;
		readonly List<AttackData> attacks;

		public TowerModel def => new() {
			id = id,
			baseId = baseId,
			icon = icon.token,
			display = sprite.token,
			behaviors = AdaptedBehaviorModel.Create(behaviors).ToArray(),
			attacks = attacks.ToEnumerableSafe().Select(t => t.def).ToArray(),
		};
	}
}

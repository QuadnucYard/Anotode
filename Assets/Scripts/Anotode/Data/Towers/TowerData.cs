using System.Collections.Generic;
using Anotode.Models;
using Anotode.Models.Towers;
using Anotode.Models.Towers.Behaviors.Attacks;
using Quadnuc.Utils;

namespace Anotode.Data.Towers {
	public class TowerData {

		readonly string id;
		readonly string baseId;
		readonly SpriteData icon;
		readonly SpriteData sprite;

		readonly float cost;
		readonly float radius;
		readonly float range;

		readonly AdaptedBehaviorModel[] behaviors;
		readonly List<AttackModel> attacks;

		public TowerModel def => new() {
			id = id,
			baseId = baseId,
			icon = icon.token,
			display = sprite.token,
			cost = cost,
			radius = radius,
			range = range,
			behaviors = behaviors,
			attacks = attacks.ToArraySafe(),
		};
	}
}

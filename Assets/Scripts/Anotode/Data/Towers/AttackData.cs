using System.Collections.Generic;
using System.Linq;
using Anotode.Models;
using Anotode.Models.Towers.Behaviors.Attacks;
using Puerts;
using UnityEngine;

namespace Anotode.Data.Towers {
	public class AttackData {
		readonly List<WeaponData> weapons;
		readonly List<JSObject> behaviors;
		readonly float range;
		readonly JSObject targetSupplier;
		readonly Vector3 offset;
		readonly bool attackThroughWalls;
		readonly bool fireWithoutTarget;

		public AttackModel def => new() {
			weapons = weapons.Select(t => t.def).ToArray(),
			behaviors = AdaptedBehaviorModel.Create(behaviors).ToArray(),
			range = range,
			targetSupplier = AdaptedBehaviorModel.Create(targetSupplier),
			offset = offset,
			attackThroughWalls = attackThroughWalls,
			fireWithoutTarget = fireWithoutTarget,
		};
	}
}

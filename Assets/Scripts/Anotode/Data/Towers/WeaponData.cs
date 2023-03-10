using System.Collections.Generic;
using System.Linq;
using Anotode.Models;
using Anotode.Models.Towers.Weapons;
using Puerts;
using UnityEngine;

namespace Anotode.Data.Towers {
	public class WeaponData {
		readonly Vector3 eject; // 这个不知道是什么
		readonly ProjectileData projectile;
		readonly bool fireWithoutTarget;
		readonly bool fireBetweenWaves;
		readonly bool useAttackPosition;
		readonly bool startInCooldown;
		readonly float rate;
		readonly List<JSObject> behaviors;

		public WeaponModel def => new() {
			eject = eject,
			projectile = projectile.def,
			fireWithoutTarget = fireWithoutTarget,
			fireBetweenWaves = fireBetweenWaves,
			useAttackPosition = useAttackPosition,
			startInCooldown = startInCooldown,
			rate = rate,
			behaviors = AdaptedBehaviorModel.Create(behaviors).ToArray(),
		};
	}
}

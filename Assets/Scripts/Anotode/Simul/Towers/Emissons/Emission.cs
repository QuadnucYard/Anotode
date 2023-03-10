using System;
using System.Collections.Generic;
using Anotode.Models.Towers.Behaviors.Emissions;
using Anotode.Models.Towers.Projectiles;
using Anotode.Simul.Objects;
using Anotode.Simul.Towers.Projectiles;
using Anotode.Simul.Towers.Weapons;
using UnityEngine;

namespace Anotode.Simul.Towers.Emissons {
	public class Emission : Simulatable {

		public EmissionModel emissionModel;
		// private SizedList<RootBehavior> createdBehaviors;

		public Action onDestroy;

		public delegate void EmitDelegate(
			ProjectileModel def,
			Vector3 ejectPoint,
			Target collidedTarget,
			int elapsed,
			Tower owner,
			Weapon weapon,
			List<Projectile> created,
			List<Ulid> collidedWith = null
		);

		public EmitDelegate Emit; // 核心逻辑应该在这

		public Projectile BaseEmit(
			ProjectileModel def,
			Vector3 ejectPoint,
			Target target,
			int elapsed,
			Tower owner,
			Weapon weapon,
			List<Projectile> created
		) {
			var proj = new Projectile {
				sim = sim,
				emittedFrom = ejectPoint,
				createdAt = elapsed,
				target = target,
				emittedBy = owner,
				weapon = weapon,
				direction = GetDirection(def, ejectPoint, target, elapsed, owner, weapon),
			};
			created.Add(proj);
			proj.Init(def);
			return proj;
		}

		public delegate Vector3 GetDirectionDelegate(
			ProjectileModel def,
			Vector3 ejectPoint,
			Target target,
			int elapsed,
			Tower owner,
			Weapon weapon
		);

		public GetDirectionDelegate GetDirection;

	}
}

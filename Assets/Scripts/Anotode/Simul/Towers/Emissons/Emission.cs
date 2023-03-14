using System;
using System.Collections.Generic;
using Anotode.Models.Towers.Behaviors.Emissions;
using Anotode.Models.Towers.Projectiles;
using Anotode.Simul.Objects;
using Anotode.Simul.Towers.Projectiles;
using Anotode.Simul.Towers.Weapons;
using Anotode.Utils.JSLoad;
using UnityEngine;

namespace Anotode.Simul.Towers.Emissons {
	public class Emission : Simulatable {

		public EmissionModel emissionModel;
		// private SizedList<RootBehavior> createdBehaviors;

		public Action onDestroy;

		// 因为delegate没法传基本类型，把elapsed去掉了

		public delegate void EmitDelegate(
			ProjectileModel def,
			Vector3 ejectPoint,
			Target collidedTarget,
			Tower owner,
			Weapon weapon,
			List<Projectile> created
		);

		public EmitDelegate Emit;

		public Projectile BaseEmit(
			ProjectileModel def,
			Vector3 ejectPoint,
			Target target,
			Tower owner,
			Weapon weapon,
			List<Projectile> created
		) {
			var proj = new Projectile {
				sim = sim,
				emittedFrom = ejectPoint,
				createdAt = owner.sim.timer.time,
				target = target,
				emittedBy = owner,
				weapon = weapon,
				direction = GetDirection(def, ejectPoint, target, weapon),
			};
			created.Add(proj);
			proj.Init(def);
			proj.position = ejectPoint;
			proj.displayNode.Update();
			return proj;
		}

		public delegate Vector3 GetDirectionDelegate(
			ProjectileModel def,
			Vector3 ejectPoint,
			Target target,
			Weapon weapon
		);

		public GetDirectionDelegate GetDirection = (t1, t2, t3, t4) => default;

		public Emission() {
			Emit = (def, ejectPoint, target, owner, weapon, created) => BaseEmit(def, ejectPoint, target, owner, weapon, created);
		}

		static Emission() {
			JSDataLoader.vm.UsingAction<ProjectileModel, Vector3, Target, Tower, Weapon, List<Projectile>>();
			JSDataLoader.vm.UsingFunc<ProjectileModel, Vector3, Target, Weapon, Vector3>();
		}

	}
}

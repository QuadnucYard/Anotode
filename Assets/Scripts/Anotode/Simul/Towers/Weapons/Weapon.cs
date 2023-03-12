using System.Collections.Generic;
using System.Linq;
using Anotode.Models.Towers.Projectiles;
using Anotode.Models.Towers.Weapons;
using Anotode.Simul.Objects;
using Anotode.Simul.Towers.Behaviors.Attacks;
using Anotode.Simul.Towers.Emissons;
using Anotode.Simul.Towers.Projectiles;
using Vector3 = UnityEngine.Vector3;

namespace Anotode.Simul.Towers.Weapons {
	public class Weapon : AttackBehavior {

		public WeaponModel weaponModel;
		public Emission emission;

		public int lastEmission;
		public float lastEmissionTime;

		//private int rateFrames;
		private float missedRate;
		//private bool hasFiredThisFrame;
		/*
		 * 猜测一下它的做法
		 * 如果当前帧射击了，并且missed凑够了1，那就再射一发
		 */

		// private SizedList<RootBehavior> createdBehaviors;
		private List<WeaponBehavior> weaponBehaviors;
		public Attack mainAttack;

		private List<Projectile> createdProjectiles = new();

		public void Init(WeaponModel weaponModel) {
			this.weaponModel = weaponModel;
			//rateFrames = (int)(weaponModel.rate * GameTimer.framesPerSecond);
			missedRate = 0;

			weaponBehaviors = BehaviorProxyFactory.CreateFromModels<WeaponBehavior>(weaponModel.behaviors, sim);
			weaponBehaviors.ForEach(t => t.weapon = this);
			mainAttack = attack;
			emission = BehaviorProxyFactory.CreateFromModel<Emission>(weaponModel.emission, sim);
			process += Process;
		}

		public void Process() {

			//mainAttack.tower;
			if (!weaponBehaviors.All(t => t.canFire())) {
				return;
			}
			float rate = weaponBehaviors.Aggregate(weaponModel.rate, (rate, next) => next.getRate(rate));
			var emissionPosition = weaponBehaviors.Aggregate(default(Vector3?), (e, next) => next.getEmitPosition() ?? e) ?? mainAttack.tower.mapPos;

			missedRate += GameTimer.fixedUpdateTime / rate;

			int elapsed = sim.timer.time;
			for (; missedRate >= 1; missedRate -= 1) {
				Emit(emissionPosition, elapsed, mainAttack.tower, createdProjectiles);
			}
		}

		public void Emit(Vector3 ejectPoint, int elapsed, Tower owner, List<Projectile> created) {
			if (!weaponBehaviors.All(t => t.filterEmission())) {
				return;
			}
			var emission = weaponBehaviors.Aggregate(this.emission, (e, next) => next.getEmission(e));
			//var emissionRoation = weaponBehaviors.Aggregate(default(float?), (e, next) => next.getEmitRotation() ?? e) ?? 0;
			// 这个rotation没找到用处

			weaponBehaviors.ForEach(t => t.onEmissionStart());

			var projectile = weaponBehaviors.Aggregate((ProjectileModel)weaponModel.projectile.Clone(), (pm, next) => next.getProjectile(pm));
			emission.Emit(projectile, ejectPoint, mainAttack.target, elapsed, owner, this, created);

			weaponBehaviors.ForEach(t => t.onProjectilesCreated(created));

			createdProjectiles.Clear();

			lastEmission = elapsed;
			//hasFiredThisFrame = true;
		}

		public Vector3 GetEjectPosition(bool addDisplaySpaceHeight = true) {
			return default;
		}
	}
}

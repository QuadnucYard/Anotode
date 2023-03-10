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
		public int lastEmission;
		public WeaponModel weaponModel;
		public Emission emmision; // 想办法绑定这个
		// private SizedList<RootBehavior> createdBehaviors;
		private List<WeaponBehavior> weaponBehaviors;
		public Attack mainAttack;

		public void Init(WeaponModel weaponModel) {
			this.weaponModel = weaponModel;
			weaponBehaviors = BehaviorProxyFactory.CreateFromModels<WeaponBehavior>(weaponModel.behaviors, sim);
			mainAttack = attack;
			process += Process;
		}

		public void Process() {
			//mainAttack.tower;
			float rate = weaponBehaviors.Aggregate(weaponModel.rate, (rate, next) => next.getRate(rate));
			if (sim.timer.time - lastEmission >= rate * GameTimer.framesPerSecond) {
				// 要攻击了
				var projectile = weaponBehaviors.Aggregate((ProjectileModel)weaponModel.projectile.Clone(), (pm, next) => next.getProjectile(pm));
				UnityEngine.Debug.Log("create projectile");
				lastEmission = sim.timer.time;
			}
		}

		public void Emit(Vector3 ejectPoint, int elapsed, Tower owner, List<Projectile> created) {
			// 这个应该是behavior调用的
		}

		public Vector3 GetEjectPosition(bool addDisplaySpaceHeight = true) {
			return default;
		}
	}
}

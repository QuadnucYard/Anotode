using Anotode.Models.Towers.Projectiles;
using Vector3 = UnityEngine.Vector3;

namespace Anotode.Models.Towers.Weapons {
	public class WeaponModel : Model {
		public Vector3 eject; // 这个不知道是什么
		public ProjectileModel projectile;
		public bool fireWithoutTarget;
		public bool fireBetweenWaves;
		public bool useAttackPosition;
		public bool startInCooldown;
		public float rate { get; set; }
		public AdaptedBehaviorModel[] behaviors;

		public override Model Clone() {
			throw new System.NotImplementedException();
		}
	}
}

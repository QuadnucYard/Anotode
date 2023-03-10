using Anotode.Models.Towers.Weapons;
using Vector3 = UnityEngine.Vector3;

namespace Anotode.Models.Towers.Behaviors.Attacks {
	public class AttackModel {
		public WeaponModel[] weapons;
		public float range;
		public AdaptedBehaviorModel[] behaviors;
		public AdaptedBehaviorModel targetSupplier;
		public Vector3 offset;
		public bool attackThroughWalls; 
		public bool fireWithoutTarget;
	}
}

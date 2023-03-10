using System;
using System.Collections.Generic;
using Anotode.Models.Towers.Projectiles;
using Anotode.Simul.Objects;
using Anotode.Simul.Physics;
using Anotode.Simul.Towers.Weapons;
using Vector3 = UnityEngine.Vector3;

namespace Anotode.Simul.Towers.Projectiles {
	public class Projectile : Collidable {
		public ProjectileModel projectileModel;
		public List<ObjectId> collidedWith;
		//private List<RootBehavior> createdBehaviors;
		private List<ProjectileBehavior> projectileBehaviors;

		public Vector3 direction;
		public Vector3 emittedFrom;
		public int createdAt;
		public int lifespan;
		public int timeSinceLastCollisionCheck;
		public bool canCollideWithEnemies;
		public Tower emittedBy { get; set; }
		public Target target { get; set; }
		public Weapon weapon { get; set; }

		public Vector3 position {
			get => displayNode.position;
			set => displayNode.position = value;
		}

		public void Init(ProjectileModel projectileModel) {
			this.projectileModel = projectileModel;
			projectileBehaviors = BehaviorProxyFactory.CreateFromModels<ProjectileBehavior>(projectileModel.behaviors, sim);
			projectileBehaviors.ForEach(t => t.projectile = this);
			emittedBy = weapon.attack.tower;
			lifespan = 0;
			displayNode = new("Projectile");
			displayNode.Create();
			process += () => {
				ProcessBehaviors(projectileBehaviors);
			};
			process += displayNode.Update;
			emittedBy.process += process;
		}

		public void SetStartPosition(Vector3 pos) {
			emittedFrom = pos;
		}

		public void OnDestroy() {
			displayNode.Destroy();
			emittedBy.process -= process;
		}

		public void Expire() {
			OnDestroy();
		}
		
		//public void 
		// 这边有几个碰撞的

	}
}

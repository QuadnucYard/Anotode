using System;
using System.Collections.Generic;
using System.Linq;
using Anotode.Models.Towers.Projectiles;
using Anotode.Simul.Objects;
using Anotode.Simul.Physics;
using Anotode.Simul.Towers.Weapons;
using Quadnuc.Utils;
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
			canCollideWithEnemies = true;

			displayNode = new("Projectile");
			displayNode.Create();
			process += () => {
				ProcessBehaviors(projectileBehaviors);
				// 暂时做法是每帧检查一次
				if (canCollideWithEnemies) {
					sim.collisionChecker.CheckHit(this);
				}
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

		public void Deplete() {
			projectileBehaviors.ForEach(t => t.onDepleted?.Invoke());
			OnDestroy();
		}

		public void Expire() {
			OnDestroy();
		}

		public void CollideEnemies(IEnumerable<Enemy> enemies) {
			enemies.ForEach(CollideEnemy);
			// Check deplete
			if (projectileBehaviors.All(t => t.canBeDepleted())) {
				Deplete();
			}
		}

		public void CollideEnemy(Enemy enemy) {
			projectileBehaviors.ForEach(t => t.onCollision?.Invoke(enemy));
		}

		public void CollideBlocker() { }

		public bool IsCollisionValid(Enemy enemy) {
			float r = projectileModel.radius;
			return (enemy.mapPos.Vec3() - this.position).sqrMagnitude < r * r;
		}

		public bool FilterEnemy(Enemy enemy) { return true; }

	}
}

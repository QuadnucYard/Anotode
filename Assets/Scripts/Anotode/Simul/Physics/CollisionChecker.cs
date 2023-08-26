using System.Linq;
using Anotode.Simul.Towers.Projectiles;

namespace Anotode.Simul.Physics {
	public class CollisionChecker {

		public Simulation sim;

		public void CheckHit(Projectile projectile) {
			var e = sim.enemyManager.GetEnemies().ToList();
			var hits = sim.enemyManager.GetEnemies().Where(e => projectile.IsCollisionValid(e)).ToArray();
			if (hits.Length > 0) {
				projectile.CollideEnemies(hits);
			}
		}
	}
}

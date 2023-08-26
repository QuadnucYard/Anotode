using Anotode.Simul.Towers.Projectiles;

namespace Anotode.Simul {
	public struct DamagePayload {
		public int amount;
		public Projectile projectile;

		public DamagePayload(int amount, Projectile projectile) {
			this.amount = amount;
			this.projectile = projectile;
		}
	}
}

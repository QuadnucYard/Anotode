using System;
using Anotode.Simul.Enemies;
using Anotode.Simul.Objects;
using Vector3 = UnityEngine.Vector3;

namespace Anotode.Simul.Towers.Projectiles {
	public class ProjectileBehavior : Simulatable {
		public Projectile projectile;

		public Action<Enemy> onCollision;
		public Action onDepleted;
		public Func<bool> canBeDepleted = () => true;
		public Action onExpired;
		public Action<Target> setTarget;
		public Func<Enemy, bool> filterCollision;
		// bool FilterMapCollision(MapBlocker blocker)
		// bool CollideMap(MapBlocker blocker)
		public Action<Vector3> setStartPosition;

	}
}

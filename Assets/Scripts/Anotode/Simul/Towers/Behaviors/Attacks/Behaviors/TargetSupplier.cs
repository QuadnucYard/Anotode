using System;
using System.Collections.Generic;

namespace Anotode.Simul.Towers.Behaviors.Attacks.Behaviors {
	public class TargetSupplier : AttackBehavior {

		public Func<string> getName;
		public Func<bool> canHaveTarget;
		public Func<Target> getTarget;
		public Func<IEnumerable<Target>> getTargets;

		protected TargetSupplier defaultSupplier;

		public static Target EnemyToTarget(Enemy enemy) => new() { enemy = enemy, position = enemy.mapPos };

	}
}

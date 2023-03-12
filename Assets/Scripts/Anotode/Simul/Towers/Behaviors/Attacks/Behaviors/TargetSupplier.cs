using System;
using System.Collections.Generic;
using Quadnuc.Utils;

namespace Anotode.Simul.Towers.Behaviors.Attacks.Behaviors {
	public class TargetSupplier : AttackBehavior {

		public Func<string> getName = FuncTools.Default<string>;
		public Func<bool> canHaveTarget = FuncTools.Tautology;
		public Func<Target> getTarget;
		public Func<IEnumerable<Target>> getTargets;

		protected TargetSupplier defaultSupplier;

		public static Target EnemyToTarget(Enemy enemy) => new() { enemy = enemy };

	}
}

using System;
using Anotode.Simul.Objects;

namespace Anotode.Simul.Towers.Behaviors.Attacks {
	public class AttackBehavior : Simulatable {
		public Attack attack;

		public Func<Enemy, bool> filterTarget;
		public Action setupAttack;
		public Func<float?> getRange;
	}
}

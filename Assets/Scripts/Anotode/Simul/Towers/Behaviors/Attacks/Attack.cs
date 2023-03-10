using System.Collections.Generic;
using System.Linq;
using Anotode.Models.Towers;
using Anotode.Models.Towers.Behaviors.Attacks;
using Anotode.Simul.Objects;
using Anotode.Simul.Towers.Behaviors.Attacks.Behaviors;
using Anotode.Simul.Towers.Weapons;

namespace Anotode.Simul.Towers.Behaviors.Attacks {
	public class Attack : TowerBehavior {
		public AttackModel attackModel;
		// private SizedList<RootBehavior> createdBehaviors;
		public List<Weapon> weapons;
		public List<AttackBehavior> attackBehaviors;
		private TargetSupplier activeTargetSupplier;
		public float range;
		public Target target { get; set; }

		public void Init(AttackModel attackModel) {
			this.attackModel = attackModel;
			attackBehaviors = BehaviorProxyFactory.CreateFromModels<AttackBehavior>(attackModel.behaviors, sim);
			attackBehaviors.ForEach(t => t.attack = this);
			weapons = attackModel.weapons.Select(t => {
				var a = new Weapon() { sim = sim };
				a.Init(t);
				return a;
			}).ToList();
			//activeTargetSupplier = BehaviorProxyFactory.CreateFromModel<TargetSupplier>(attackModel.targetSupplier, sim);

			onUpdate += UpdateBehaviors;
		}

		public bool HasValidTarget() { return true; }
		public void FindTarget() {
		}

		public void ClearTarget() {
		}

		private void UpdateBehaviors() {
			attackBehaviors.ForEach(t => t.process?.Invoke());
			weapons.ForEach(t => t.process?.Invoke());
		}

		public Target GetVisibleTarget(TargetType targetType) => new Target();

		public IEnumerable<Target> GetVisibleTargets(TargetType targetType) => (IEnumerable<Target>)null;

		public Target GetTarget() => new Target();
		public IEnumerable<Target> GetTargets() => (IEnumerable<Target>)null;
		private void UpdateActiveTargetSupplier() {
		}
		public void SetActiveTargetSupplier(TargetType targetType) {
		}
		//public bool FilterTargetWithLineOfSight(Enemy enemy) { }

		//public bool FilterTarget(Enemy enemy) { }
		public void SetupAttack() {
		}
		public TargetSupplier GetActiveTargetSupplier() => (TargetSupplier)null;

	}
}

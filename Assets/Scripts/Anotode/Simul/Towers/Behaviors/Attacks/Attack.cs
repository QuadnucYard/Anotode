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
		public TargetSupplier activeTargetSupplier { get; private set; }
		public float range;
		public Target target { get; set; }

		public void Init(AttackModel attackModel) {
			this.attackModel = attackModel;
			attackBehaviors = BehaviorProxyFactory.CreateFromModels<AttackBehavior>(attackModel.behaviors, sim);
			attackBehaviors.ForEach(t => t.attack = this);
			weapons = attackModel.weapons.Select(t => {
				var a = new Weapon() { sim = sim, attack = this };
				a.Init(t);
				return a;
			}).ToList();
			if (attackModel.targetSupplier !=null) {
				activeTargetSupplier = new() { sim = sim, attack = this };
				BehaviorProxyFactory.BindBehavior(activeTargetSupplier, attackModel.targetSupplier);
				//activeTargetSupplier = BehaviorProxyFactory.CreateFromModel<TargetSupplier>(attackModel.targetSupplier, sim);
			} else {
				SetActiveTargetSupplier(TargetType.First);
			}

			range = attackModel.range;

			onUpdate += UpdateBehaviors;
		}

		public bool HasValidTarget() => target.valid;
		public void FindTarget() => target = GetTarget();

		public void ClearTarget() => target = default;

		private void UpdateBehaviors() {
			FindTarget();
			ProcessBehaviors(attackBehaviors);
			ProcessBehaviors(weapons);
			UpdateActiveTargetSupplier();
		}

		public bool CanHaveTarget() => activeTargetSupplier.canHaveTarget();

		//public Target GetVisibleTarget(TargetType targetType) => new Target();

		//public IEnumerable<Target> GetVisibleTargets(TargetType targetType) => (IEnumerable<Target>)null;

		public Target GetTarget() => activeTargetSupplier.getTarget();
		public IEnumerable<Target> GetTargets() => activeTargetSupplier.getTargets();

		private void UpdateActiveTargetSupplier() {
			activeTargetSupplier.process?.Invoke();
		}

		public void SetActiveTargetSupplier(TargetType targetType) {
			activeTargetSupplier = new() {
				attack = this,
				getTarget = () => TargetSupplier.EnemyToTarget(sim.enemyManager.GetTarget(tower.mapPos, range, targetType)),
			};
		}

		//public bool FilterTargetWithLineOfSight(Enemy enemy) { }

		public bool FilterTarget(Enemy enemy) {
			return attackBehaviors.All(t => t.filterTarget(enemy));
		}
		
		public void SetupAttack() {
			attackBehaviors.ForEach(t => t.setupAttack());
		}

	}
}

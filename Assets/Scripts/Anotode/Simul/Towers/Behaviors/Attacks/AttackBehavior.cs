using System;
using Anotode.Simul.Enemies;
using Anotode.Simul.Objects;
using Quadnuc.Utils;

namespace Anotode.Simul.Towers.Behaviors.Attacks {
	public class AttackBehavior : Simulatable {
		public Attack attack;

		public EnemyManager.EnemyFilter filterTarget = FuncTools.Tautology;
		public Action setupAttack = FuncTools.NoAction; // 应该是准备攻击时调用
		public Func<float?> getRange = FuncTools.Default<float?>;
	}
}

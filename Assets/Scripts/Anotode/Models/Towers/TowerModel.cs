using System;
using Anotode.Models.Towers.Behaviors.Attacks;

namespace Anotode.Models.Towers {

	[Serializable]
	public class TowerModel : Model {

		public string id;
		public string baseId;
		public int icon;
		public int display;

		public float cost;
		public float radius;
		public float range;

		public AdaptedBehaviorModel[] behaviors;
		public AttackModel[] attacks;

		public override Model Clone() {
			throw new NotImplementedException();
		}
	}
}

using System;

namespace Anotode.Models.Towers.Behaviors.Emissions {
	public class EmissionModel : Model {
		public AdaptedBehaviorModel[] behaviors;
		public override Model Clone() {
			throw new NotImplementedException();
		}
	}
}

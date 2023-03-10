using System;
using Anotode.Simul.Objects;

namespace Anotode.Simul.Towers {
	public class TowerBehavior : Simulatable {

		public Tower tower { get; set; }

		public Action onUpdate;

	}
}

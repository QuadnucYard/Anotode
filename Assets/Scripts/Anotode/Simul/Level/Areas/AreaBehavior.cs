using System;
using Anotode.Simul.Objects;

namespace Anotode.Simul.Level.Areas {
	public class AreaBehavior : Simulatable {

		public TiledArea area { get; set; }

		public Action onUpdate;

	}
}

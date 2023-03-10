using System;

namespace Anotode.Simul.Objects {
	public abstract class BaseObject {
		public Ulid id { get; protected set; }

		public Simulation.ProcessDelegate process;

		public BaseObject() {
			id = Ulid.NewUlid();
		}

	}
}

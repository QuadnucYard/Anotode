using System.Collections.Generic;
using Anotode.Display.VM;
using Quadnuc.Utils;

namespace Anotode.Simul.Objects {
	public abstract class Simulatable : BaseObject {

		public Simulation sim { get; set; }
		public DisplayNode displayNode { get; set; }

		public Simulatable() : base() { }	

		protected void ProcessBehaviors(IEnumerable<Simulatable> behaviors) {
			behaviors.ForEach(t => t.process?.Invoke());
		}

	}
}

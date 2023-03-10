using Anotode.Display.VM;

namespace Anotode.Simul.Objects {
	public abstract class Simulatable : BaseObject {

		public Simulation sim { get; set; }
		public DisplayNode displayNode { get; set; }

		public Simulatable() : base() { }	

	}
}

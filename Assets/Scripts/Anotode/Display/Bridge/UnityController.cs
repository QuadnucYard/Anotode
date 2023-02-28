using Anotode.Simul;

namespace Anotode.Display.Bridge {
	public class UnityController {

		public Simulation simulation { get; set; }

		public void StartWave() {
			simulation.StartWave();
		}
	}
}

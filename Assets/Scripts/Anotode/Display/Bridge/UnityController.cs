using System;
using Anotode.Simul;

namespace Anotode.Display.Bridge {
	public class UnityController {

		public Simulation simulation { get; set; }

		public event Action<int> onWaveStart;
		public event Action<int> onWaveSpawnEnd;
		public event Action<int> onWaveEnd;

		public void Init(Simulation sim) {
			simulation = sim;
			sim.onWaveStart += onWaveStart;
			sim.onWaveSpawnEnd += onWaveSpawnEnd;
			sim.onWaveEnd += onWaveEnd;
		}

		public void StartWave() {
			simulation.StartWave();
		}
	}
}

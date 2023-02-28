using Anotode.Models;
using Anotode.Models.Map;
using Anotode.Simul.Level;
using Quadnuc.Utils;

namespace Anotode.Simul {
	public class Simulation {

		public GameModel model;
		public GameMap map;
		public int time;
		public int waveStartTime { get; private set; }

		public Simulation() {
			time = 0;
		}

		public void Init(GameModel model) {
			this.model = model;
		}

		public void InitMap(GameMapModel newMap) {
			map = new(newMap) { sim = this };
			map.Init();
			model.map = newMap;
		}

		public void Simulate() {
			map.spawner.Process(time);
			map.areas.ForEach(t => t.UpdateFrame());
			map.areaEnemies.ForEach(t => t.Value.ForEach(t => t.Process(time)));
			map.areaEnemies.ForEach(t => t.Value.RemoveAll(t => t.dead));
			time++; // 需要循环模拟步长次
		}

		public void StartWave() {
			waveStartTime = time;
			WaveStart(0);
		}

		public void WaveStart(int spawnedRound) {
			map.spawner.StartWave();
		}

		public void WaveEnd(int round) {
		}

		public void SpawnEnemy() {

		}
	}
}

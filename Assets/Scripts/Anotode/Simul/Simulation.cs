using System;
using System.Linq;
using Anotode.Models;
using Anotode.Models.Map;
using Anotode.Models.Waves;
using Anotode.Simul.Enemies;
using Anotode.Simul.Level;
using Anotode.Simul.Towers;
using Quadnuc.Utils;

namespace Anotode.Simul {
	public class Simulation {

		public delegate void ProcessDelegate();

		public GameModel model;
		public GameMap map;

		public Spawner spawner;

		public readonly GameTimer timer;

		public readonly TowerManager towerManager;
		public readonly EnemyManager enemyManager;

		public event Action<int> onWaveStart; // 正式开始
		public event Action<int> onWaveSpawnEnd; // 生成结束
		public event Action<int> onWaveEnd; // 完全消灭

		public Simulation() {
			timer = new();
			towerManager = new() { sim = this };
			enemyManager = new() { sim = this };
		}

		public void Init(GameModel model) {
			this.model = model;
		}

		public void InitLevel(LevelModel level) {
			map = new(level.map) { sim = this };
			map.Init();
			model.map = level.map;
			spawner = new(level) {
				sim = this,
				spawnJunction = null,
				waveProvider = new(model, level),
			};
		}

		public void InitEvents() {
			spawner.onSpawnComplete += onWaveSpawnEnd;
		}

		public void Simulate() {
			spawner.Process();
			map.areas.ForEach(t => t.Process());
			enemyManager.GetEnemies().ToArray().ForEach(t => t.process?.Invoke());
			map.areaTowers.Values.ForEach(t => t.ForEach(t => t.process?.Invoke()));
			map.areaEnemies.Values.ForEach(t => t.RemoveAll(t => t.dead));
			timer.Update(1); // 需要循环模拟步长次
		}

		public void StartWave() {
			timer.waveStartTime = timer.time;
			WaveStart(spawner.nextRound);
		}

		public void WaveStart(int spawnedRound) {
			spawner.StartWave();
			onWaveStart?.Invoke(spawnedRound);
		}

		public void WaveEnd(int round) {
			onWaveEnd?.Invoke(round);
		}

		public void SpawnEnemy() {

		}

	}
}

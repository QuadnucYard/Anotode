using System;
using System.Collections.Generic;
using System.Linq;
using Anotode.Models;
using Anotode.Models.Map;
using Anotode.Models.Waves;
using Anotode.Simul.Objects;
using Quadnuc.Utils;
using Vector2 = UnityEngine.Vector2;

namespace Anotode.Simul.Level {
	public class Spawner : Simulatable, IProcessable {

		public Splitter spawnJunction;

		public LevelModel levelModel;
		public WaveProvider waveProvider;

		private WaveModel _currentWave;
		private List<EnemyModel> _spawnList;
		private int _nextSpawnIndex = 0;

		private GameTimer.Timer _timer;

		public int nextRound => waveProvider.currentWave;

		public event Action<int> onSpawnComplete;

		public Spawner(LevelModel levelModel) {
			this.levelModel = levelModel;
		}

		public void StartWave() {
			if (!waveProvider.HasNext()) return;
			_currentWave = waveProvider.NextWave();
			_spawnList = _currentWave.enemyList.Select(t => sim.model.GetEnemy(t)).ToList();
			_timer = sim.timer.GetTimer(levelModel.spawnInterval);
		}

		private Enemy Spawn() {
			// TODO 设置生成的area
			var area = sim.map.mainArea;
			var model = (EnemyModel)_spawnList.Pop().Clone();
			model.spawnIndex = _nextSpawnIndex++;
			Enemy enemy = new(model) { sim = sim, areaIn = area };
			enemy.Init();
			enemy.localPos = area.areaModel.entrances[0] + Vector2.one / 2;
			sim.map.areaEnemies[area.id].Add(enemy);
			return enemy;
		}

		public void Process() {
			if (_spawnList?.Count > 0) {
				foreach (var _ in _timer.CheckUpdate()) {
					var enemy = Spawn();
					if (_spawnList.Empty()) {
						onSpawnComplete?.Invoke(nextRound - 1);
					}
				}
			}
		}

	}
}

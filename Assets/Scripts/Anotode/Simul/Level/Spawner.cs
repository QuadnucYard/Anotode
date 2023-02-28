using System.Collections.Generic;
using System.Linq;
using Anotode.Models;
using Anotode.Models.Waves;
using Anotode.Simul.Objects;
using Quadnuc.Utils;
using Vector2 = UnityEngine.Vector2;

namespace Anotode.Simul.Level {
	public class Spawner : Simulatable, IProcessable {

		public Splitter spawnJunction;

		//要存wavedata
		public WaveProvider waveProvider;

		private WaveModel _currentWave;
		private List<EnemyModel> _spawnList;
		private int _nextSpawnIndex = 0;

		public void StartWave() {
			if (!waveProvider.HasNext()) return;
			_currentWave = waveProvider.NextWave();
			_spawnList = _currentWave.enemyList.Select(t => sim.model.GetEnemy(t)).ToList();
		}

		private Enemy Spawn() {
			// TODO 设置生成的area
			var area = sim.map.mainArea;
			var model = (EnemyModel)_spawnList.Pop().Clone();
			model.spawnIndex = _nextSpawnIndex++;
			Enemy enemy = new(model) { sim = sim, spawnTime = sim.time, areaIn = area };
			enemy.enemyModel.pos = (Vector2)area.areaModel.entrances[0] + Vector2.one / 2;
			sim.map.areaEnemies[area].Add(enemy);
			return enemy;
		}

		public void Process(int elapsed) {
			if ((elapsed - sim.waveStartTime) % 30 == 0 && _spawnList?.Count > 0) {
				var enemy = Spawn();
				enemy.Init();
			}
		}

	}
}

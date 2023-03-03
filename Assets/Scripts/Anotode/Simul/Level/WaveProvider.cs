using System;
using System.Collections.Generic;
using System.Linq;
using Anotode.Models;
using Anotode.Models.Map;
using Anotode.Models.Waves;

namespace Anotode.Simul.Level {
	public class WaveProvider {

		private readonly GameModel _game;
		private readonly WaveModel[] _waves;
		private readonly EnemyGroupModel _enemyGroup;
		private int _currentWave;
		private Random _rng;

		public int currentWave => _currentWave;

		public WaveProvider(GameModel game, LevelModel level) {
			_game = game;
			_waves = level.waves;
			_enemyGroup = level.enemyGroup;
			_currentWave = 0;
		}

		public bool HasNext() {
			return _currentWave < _waves?.Length || _enemyGroup != null;
		}

		public WaveModel NextWave() {
			if (_currentWave < _waves?.Length) {
				return _waves[_currentWave++];
			}

			int n = _enemyGroup.proportions.Count;
			var propList = _enemyGroup.proportions.Values.ToList();
			var enemyList = _enemyGroup.proportions.Keys.Select(t => _game.GetEnemy(t)).ToList();

			_rng = new(_enemyGroup.seed + _currentWave);

			List<string> spawnList = new();
			for (int pop = _enemyGroup.populationMax; pop > 0;) {
				while (n > 0 && pop < enemyList[n - 1].population) n--;
				if (n == 0) break;
				int k;
				do {
					float p = (float)_rng.NextDouble() * propList[n - 1];
					k = propList.FindIndex(t => p < t);
				} while (pop < enemyList[k].population);
				pop -= enemyList[k].population;
				spawnList.Add(enemyList[k].id);
			}
			_currentWave++;
			return new() { difficulty = 1.0f, enemyList = spawnList.ToArray() };
		}

	}
}

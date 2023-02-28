using System;
using System.Collections.Generic;
using System.Linq;
using Anotode.Models;
using Anotode.Models.Waves;

namespace Anotode.Simul.Level {
	public class WaveProvider {

		private readonly GameModel _game;
		private readonly WaveModel[] _waves;
		private readonly EnemyGroupModel _enemyGroup;
		private int _currentWave;
		private readonly Random _rng;

		public WaveProvider(GameModel game, WaveModel[] waves) {
			_game = game;
			_waves = waves;
			_currentWave = 0;
		}

		public WaveProvider(GameModel game, EnemyGroupModel enemyGroup) {
			_game = game;
			_enemyGroup = enemyGroup;
			_currentWave = -1;
			_rng = new(); // TODO: Need a seed
		}

		public bool HasNext() {
			return _currentWave == -1 || _currentWave < _waves.Length;
		}

		public WaveModel NextWave() {
			// 需要知道GameModel
			if (_currentWave != -1) {
				return _waves[_currentWave++];
			}
			int n = _enemyGroup.proportions.Count;
			var propList = _enemyGroup.proportions.Values.ToList();
			var enemyList = _enemyGroup.proportions.Keys.Select(t => _game.GetEnemy(t)).ToList();

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
			return new() { difficulty = 1.0f, enemyList = spawnList.ToArray() };
		}

	}
}

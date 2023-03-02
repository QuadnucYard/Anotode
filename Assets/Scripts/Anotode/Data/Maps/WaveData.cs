using System.Collections.Generic;
using Anotode.Models.Waves;

namespace Anotode.Data.Maps {
	public class WaveData {
		readonly List<string> enemies;
		readonly float difficulty;

		public WaveModel def => new() {
			enemyList = enemies.ToArray(),
			difficulty = difficulty
		};
	}
}

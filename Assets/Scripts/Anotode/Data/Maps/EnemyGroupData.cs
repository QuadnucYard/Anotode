using System.Collections.Generic;
using Anotode.Models.Waves;

namespace Anotode.Data.Maps {
	public class EnemyGroupData {
		readonly int seed;
		readonly int populationMax;
		readonly Dictionary<string, float> proportions;

		public EnemyGroupModel def => new() {
			seed = seed,
			populationMax = populationMax,
			proportions = proportions,
		};
	}
}

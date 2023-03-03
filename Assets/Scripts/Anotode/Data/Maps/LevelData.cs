﻿using System.Collections.Generic;
using System.Linq;
using Anotode.Models.Map;
using Anotode.Models.Waves;

namespace Anotode.Data.Maps {

	public class EnemyGroupData {
		readonly int populationMax;
		readonly Dictionary<string, float> proportions;

		public EnemyGroupModel def => new() {
			populationMax = populationMax,
			proportions = proportions,
		};
	}

	public class LevelData {
		readonly string id;
		readonly string mapId;
		readonly float hardnessA;
		readonly float hardnessB;
		readonly float spawnInterval;
		readonly int splitRule;

		readonly List<WaveData> waves;
		readonly EnemyGroupData enemyGroup;

		public LevelModel def => new() {
			id = id,
			mapId = mapId,
			hardnessA = hardnessA,
			hardnessB = hardnessB,
			spawnInterval = spawnInterval,
			splitRule = splitRule,
			waves = waves.Select(t => t.def).ToArray(),
			enemyGroup = enemyGroup.def,
			map = GameDataManager.getMap(mapId),
		};
	}
}

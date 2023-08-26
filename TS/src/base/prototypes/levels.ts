const E1 = "basic",
  E2 = "strong",
  E3 = "fast";

const waveTemplate = [
  {
    enemies: [E1, E1, E3, E2, E2, E1, E1, E3, E2, E2, E3, E3],
    difficulty: 2.0,
  },
  {
    enemies: [E1, E1, E3, E2, E2, E1, E1, E3, E2, E2],
    difficulty: 3.0,
  },
];

const data: LevelData[] = [
  {
    id: "level-0",
    mapId: "map-0-0-0",
    hardnessA: 1.0,
    hardnessB: 10.0,
    spawnInterval: 0.2,
    splitRule: 2,
    enemyGroup: {
      populationMax: 50,
      proportions: {
        [E1]: 0.4,
        [E2]: 0.7,
        [E3]: 1.0,
      },
    },
    waves: waveTemplate,
  },
  {
    id: "level-1",
    mapId: "map-0-0-1",
    hardnessA: 2.0,
    hardnessB: 80.0,
    spawnInterval: 0.2,
    splitRule: 2,
    enemyGroup: {
      populationMax: 50,
      proportions: {
        [E1]: 0.4,
        [E2]: 0.7,
        [E3]: 1.0,
      },
    },
    waves: waveTemplate,
  },
  {
    id: "level-2",
    mapId: "map-0-0-2",
    hardnessA: 2.0,
    hardnessB: 80.0,
    spawnInterval: 0.2,
    splitRule: 2,
    enemyGroup: {
      populationMax: 100,
      proportions: {
        [E1]: 0.4,
        [E2]: 0.7,
        [E3]: 1.0,
      },
    },
    waves: [],
  },
];

export default data;

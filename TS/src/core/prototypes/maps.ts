interface TileData {
  id: string;
  index: number;
  type: CS.Anotode.Models.Map.TileType;
  sprite: SpriteData;
}

interface AreaData {
  tiles: int[][];
  entrances?: vector2[];
  exits?: vector2[];
  pivotPoint: vector2;
  position: vector2;
  behaviors?: BehaviorModel[];
}

interface MapData {
  id: string;
  areas: AreaData[];
}

interface LevelData {
  id: string;
  mapId: string;
  hardnessA: number;
  hardnessB: number;
  spawnInterval: number;
  splitRule: number;
  enemyGroup: {
    populationMax: number;
    proportions: { [id: string]: number };
  };
  waves: {
    enemies: string[];
    difficulty: number;
  }[];
}

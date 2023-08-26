interface EnemyData {
  id: string;
  baseId: string | null;
  sprite: SpriteData;
  speed: number;
  population: number;
  charm: number;
  charmSelf: number;
  invasionDamage: number;
  hpFactor: number;
  passFlag: CS.Anotode.Models.Map.TilePassFlag;
  behaviors: any[];
}

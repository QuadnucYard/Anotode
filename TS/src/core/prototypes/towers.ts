interface ProjectileData {
  id: string;
  sprite: SpriteData;
  radius: number;
  ignoreBlockers: boolean;
  usePointCollisionWithEnemies: boolean;
  scale: number;
  behaviors?: BehaviorModel[];
}

interface WeaponData {
  emission: BehaviorModel;
  eject: vector3;
  projectile: ProjectileData;
  fireWithoutTarget: boolean;
  fireBetweenWaves: boolean;
  useAttackPosition: boolean;
  startInCooldown: boolean;
  rate: number;
  behaviors?: BehaviorModel[];
}

interface AttackData {
  range: number;
  targetSupplier?: BehaviorModel;
  offset: vector3;
  attackThroughWalls: boolean;
  fireWithoutTarget: boolean;
  weapons: WeaponData[];
  behaviors?: BehaviorModel[];
}

interface TowerData {
  id: string;
  baseId: string | null;
  sprite: SpriteData;
  icon: SpriteData;
  cost: number;
  radius: number;
  range: number;
  behaviors?: BehaviorModel[];
  attacks?: AttackData[];
}

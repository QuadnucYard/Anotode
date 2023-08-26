import { defineSprite } from "../../core/prototypes/utils";

const { TilePassFlag } = CS.Anotode.Models.Map;

const data: EnemyData[] = [
  {
    id: "basic",
    baseId: null,
    sprite: defineSprite("__base__/graphics/enemies/basic.png", 64),
    speed: 2.0,
    population: 1,
    charm: -5,
    charmSelf: 10,
    invasionDamage: 1,
    hpFactor: 10,
    passFlag: TilePassFlag.Land,
    behaviors: [],
  },
  {
    id: "strong",
    baseId: null,
    sprite: defineSprite("__base__/graphics/enemies/strong.png", 64),
    speed: 1.0,
    population: 2,
    charm: 20,
    charmSelf: 2,
    invasionDamage: 2,
    hpFactor: 20,
    passFlag: TilePassFlag.Land,
    behaviors: [],
  },
  {
    id: "fast",
    baseId: null,
    sprite: defineSprite("__base__/graphics/enemies/fast.png", 64),
    speed: 4.0,
    population: 1,
    charm: -20,
    charmSelf: -5,
    invasionDamage: 1,
    hpFactor: 5,
    passFlag: TilePassFlag.Land,
    behaviors: [],
  },
];

export default data;

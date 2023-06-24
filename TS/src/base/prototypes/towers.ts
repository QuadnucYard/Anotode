import { basicAttack, multipleAttack } from "./towers/attacks";

const { TowerBehavior } = CS.Anotode.Simul.Towers;
const { Attack } = CS.Anotode.Simul.Towers.Behaviors.Attacks;
const { Weapon } = CS.Anotode.Simul.Towers.Weapons;
const { WeaponBehavior } = CS.Anotode.Simul.Towers.Weapons;

function createIconSprite(name: string) {
  return {
    icon: {
      filename: `__base__/graphics/towers/${name}.png`,
      ppu: 64,
    },
    sprite: {
      filename: `__base__/graphics/towers/${name}.png`,
      ppu: 64,
    },
  };
}

const data: TowerData[] = [
  {
    id: "tower-1",
    baseId: null,
    ...createIconSprite("tower1"),
    cost: 100,
    radius: 0.4,
    range: 2,
    behaviors: [],
    attacks: [multipleAttack],
  },
  {
    id: "tower-2",
    baseId: null,
    ...createIconSprite("tower2"),
    cost: 100,
    radius: 0.4,
    range: 2,
  },
  {
    id: "tower-3",
    baseId: null,
    ...createIconSprite("tower3"),
    cost: 100,
    radius: 0.4,
    range: 2,
  },
  {
    id: "tower-4",
    baseId: null,
    ...createIconSprite("tower4"),
    cost: 100,
    radius: 0.4,
    range: 2,
  },
  {
    id: "tower-5",
    baseId: null,
    ...createIconSprite("tower5"),
    cost: 100,
    radius: 0.4,
    range: 2,
  },
  {
    id: "tower-6",
    baseId: null,
    ...createIconSprite("tower6"),
    cost: 100,
    radius: 0.4,
    range: 2,
  },
];

export default data;

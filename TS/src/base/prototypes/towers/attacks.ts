import { BehaviorProxy } from "../../../core/adapter";
import { vec3 } from "../../../core/prototypes/utils";
import { basicWeapon, multiWeapon } from "./weapons";

type TargetType = CS.Anotode.Models.Towers.TargetType;
type TargetSupplier = CS.Anotode.Simul.Towers.Behaviors.Attacks.Behaviors.TargetSupplier;

const { TargetType } = CS.Anotode.Models.Towers;
const { TargetSupplier } = CS.Anotode.Simul.Towers.Behaviors.Attacks.Behaviors;

export const basicAttack: AttackData = {
  weapons: [basicWeapon],
  range: 3,
  offset: vec3(0, 0, 0),
  attackThroughWalls: false,
  fireWithoutTarget: false,
};

interface MultipleTargetSupplierModel extends BehaviorModel {
  targetType: TargetType;
  shots: int;
}

// puer.$extension(
//   CS.System.Collections.Generic.List$1,
//   CS.System.Linq.Enumerable
// );

class MultipleTargetSupplier extends BehaviorProxy<MultipleTargetSupplierModel, TargetSupplier> {
  constructor(bindTo: TargetSupplier, modelToUse: MultipleTargetSupplierModel) {
    super(bindTo, modelToUse);
    const atk = bindTo.attack;
    bindTo.getTarget = () =>
      TargetSupplier.EnemyToTarget(
        bindTo.sim.enemyManager.GetTarget(atk.tower.mapPos, atk.range, TargetType.Last)
      );
    bindTo.getTargets = () => {
      const enemies = bindTo.sim.enemyManager.GetTargets(
        atk.tower.mapPos,
        atk.range,
        modelToUse.targetType,
        modelToUse.shots
      );
      return TargetSupplier.EnemiesToTargets(enemies);
    };
  }
}

export const multipleAttack: AttackData = {
  weapons: [multiWeapon],
  targetSupplier: def<MultipleTargetSupplierModel>({
    $impl: MultipleTargetSupplier,
    targetType: TargetType.Last,
    shots: 3,
  }),
  range: 3,
  offset: vec3(0, 0, 0),
  attackThroughWalls: false,
  fireWithoutTarget: false,
};

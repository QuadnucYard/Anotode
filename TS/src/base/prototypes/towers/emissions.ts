import { BehaviorProxy } from "../../../core/adapter";
import { iterate } from "../../../core/utils";

import ue = CS.UnityEngine;

const { GameTimer } = CS.Anotode.Simul;

type Emission = CS.Anotode.Simul.Towers.Emissons.Emission;

interface SingleEmissionModel extends BehaviorModel {}

export class SingleEmission extends BehaviorProxy<SingleEmissionModel, Emission> {
  constructor(bindTo: Emission, modelToUse: BehaviorModel) {
    super(bindTo, modelToUse);
    bindTo.GetDirection = (def, ejectPoint, target, weapon) =>
      ue.Vector2.op_Implicit(
        ue.Vector2.op_Subtraction(target.enemy.mapPos, weapon.mainAttack.tower.mapPos).normalized
      );
  }
}

export class MultipleEmission extends BehaviorProxy<SingleEmissionModel, Emission> {
  constructor(bindTo: Emission, modelToUse: BehaviorModel) {
    super(bindTo, modelToUse);
    bindTo.Emit = (def, ejectPoint, collidedTarget, owner, weapon, created) => {
      for (const target of iterate(weapon.mainAttack.GetTargets())) {
        bindTo.BaseEmit(def, ejectPoint, target, owner, weapon, created);
      }
    };
    bindTo.GetDirection = (def, ejectPoint, target, weapon) =>
      ue.Vector2.op_Implicit(
        ue.Vector2.op_Subtraction(target.enemy.mapPos, weapon.mainAttack.tower.mapPos).normalized
      );
  }
}

import { BehaviorProxy } from "../../../core/adapter";
import { iterate } from "../../../core/utils";

import ue = CS.UnityEngine;

type Emission = CS.Anotode.Simul.Towers.Emissons.Emission;

interface SingleEmissionModel extends BehaviorModel {}

function toVec3(vec2: ue.Vector2) {
  return new ue.Vector3(vec2.x, vec2.y, 0);
}

export class SingleEmission extends BehaviorProxy<SingleEmissionModel, Emission> {
  constructor(bindTo: Emission, modelToUse: BehaviorModel) {
    super(bindTo, modelToUse);
    bindTo.GetDirection = (def, ejectPoint, target, weapon) =>
      toVec3(
        ue.Vector2.op_Subtraction(target.enemy.mapPos, weapon.mainAttack.tower.mapPos).normalized
      );
  }
}

export class MultipleEmission extends BehaviorProxy<SingleEmissionModel, Emission> {
  constructor(bindTo: Emission, modelToUse: BehaviorModel) {
    super(bindTo, modelToUse);
    bindTo.Emit = payload => {
      for (const target of iterate(payload.weapon.mainAttack.GetTargets())) {
        payload.collidedTarget = target;
        bindTo.BaseEmit(payload);
      }
    };
    bindTo.GetDirection = (def, ejectPoint, target, weapon) =>
      toVec3(
        ue.Vector2.op_Subtraction(target.enemy.mapPos, weapon.mainAttack.tower.mapPos).normalized
      );
  }
}

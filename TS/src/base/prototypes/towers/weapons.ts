import { BehaviorProxy } from "../../../core/adapter";
import { vec3 } from "../../../core/prototypes/utils";
import { MultipleEmission, SingleEmission } from "./emissions";
import { basicProjectile } from "./projectiles";

import ue = CS.UnityEngine;
import { def } from "../../../core/def";

type WeaponBehavior = CS.Anotode.Simul.Towers.Weapons.WeaponBehavior;

interface BasicWeaponModel extends BehaviorModel {}

class BasicWeapon extends BehaviorProxy<BasicWeaponModel, WeaponBehavior> {
  constructor(bindTo: WeaponBehavior, modelToUse: BasicWeaponModel) {
    super(bindTo, modelToUse);
    bindTo.canFire = () => bindTo.weapon.mainAttack.target.enemy != null;
    // bindTo.canFire = () => this.canFire();
    bindTo.getEmitRotation = () =>
      ue.Vector2.Angle(
        ue.Vector2.right,
        ue.Vector2.op_Subtraction(
          bindTo.weapon.mainAttack.target.enemy.mapPos,
          bindTo.weapon.mainAttack.tower.mapPos
        )
      );
  }
}

export const basicWeapon: WeaponData = {
  emission: { $impl: SingleEmission },
  eject: vec3(0, 0, 0),
  projectile: basicProjectile,
  fireWithoutTarget: false,
  fireBetweenWaves: false,
  useAttackPosition: true,
  startInCooldown: true,
  rate: 0.1,
  behaviors: [
    def<BasicWeaponModel>({
      $impl: BasicWeapon,
    }),
  ],
};

export const multiWeapon: WeaponData = {
  emission: { $impl: MultipleEmission },
  eject: vec3(0, 0, 0),
  projectile: basicProjectile,
  fireWithoutTarget: false,
  fireBetweenWaves: false,
  useAttackPosition: true,
  startInCooldown: true,
  rate: 0.1,
  behaviors: [
    def<BasicWeaponModel>({
      $impl: BasicWeapon,
    }),
  ],
};

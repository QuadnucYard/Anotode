import { BehaviorProxy } from "../../../core/adapter";
import { def } from "../../../core/def";
import { defineSprite } from "../../../core/prototypes/utils";

import ue = CS.UnityEngine;

const { GameTimer, DamagePayload } = CS.Anotode.Simul;

type ProjectileBehavior = CS.Anotode.Simul.Towers.Projectiles.ProjectileBehavior;

interface TravelStraightModel extends BehaviorModel {
  speed: number;
  lifespan: number;
}

class TravelStraight extends BehaviorProxy<TravelStraightModel, ProjectileBehavior> {
  speedFrames: number;
  lifespanFrames: number;
  constructor(bindTo: ProjectileBehavior, modelToUse: BehaviorModel) {
    super(bindTo, modelToUse);
    this.speedFrames = this.model.speed * GameTimer.fixedUpdateTime;
    this.lifespanFrames = this.model.lifespan * GameTimer.framesPerSecond;
    this.bindTo.process = () => this.process();
  }

  process() {
    const proj = this.bindTo.projectile;
    proj.position = proj.position.Add(proj.direction.Mul(this.speedFrames));
    if (this.bindTo.sim.timer.time - proj.createdAt > this.lifespanFrames) {
      proj.Expire();
    }
  }
}

interface GuidedTravelModel extends BehaviorModel {}

class GuidedTravel extends BehaviorProxy<GuidedTravelModel, ProjectileBehavior> {
  constructor(bindTo: ProjectileBehavior, modelToUse: BehaviorModel) {
    super(bindTo, modelToUse);
    this.bindTo.process = () => this.process();
  }

  process() {
    const proj = this.bindTo.projectile;
    if (proj.target.enemy.dead) {
      return;
    }
    proj.direction = proj.target.enemy.mapPos.Vec3().Sub(proj.position).normalized;
    // TODO 转弯限制
  }
}

interface DamageModel extends BehaviorModel {
  damage: number;
}

class Damage extends BehaviorProxy<DamageModel, ProjectileBehavior> {
  constructor(bindTo: ProjectileBehavior, modelToUse: DamageModel) {
    super(bindTo, modelToUse);
    bindTo.onCollision = enemy => {
      enemy.Damage(new DamagePayload(this.model.damage, this.bindTo.projectile));
    };
  }
}

interface BurnColdAttachModel extends BehaviorModel {
  value: number;
}

export const basicProjectile: ProjectileData = {
  id: "basic-projectile",
  sprite: defineSprite("", 64),
  radius: 0.1,
  ignoreBlockers: false,
  usePointCollisionWithEnemies: false,
  scale: 1,
  behaviors: [
    def<GuidedTravelModel>({ $impl: GuidedTravel }),
    def<TravelStraightModel>({
      $impl: TravelStraight,
      speed: 5,
      lifespan: 1,
    }),
    def<DamageModel>({
      $impl: Damage,
      damage: 10,
    }),
  ],
};

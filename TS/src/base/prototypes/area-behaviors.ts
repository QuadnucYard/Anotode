// type Model = CS.Anotode.Models.Model;
type AreaBehavior = CS.Anotode.Simul.Level.Areas.AreaBehavior;
import ue = CS.UnityEngine;

const { GameTimer } = CS.Anotode.Simul;

class AreaBehaviorBase<T> {
  protected model: T;
  protected bindTo: AreaBehavior;

  constructor(bindTo: AreaBehavior, modelToUse: T) {
    this.bindTo = bindTo;
    this.model = modelToUse;
  }
}

export interface AreaMoveModel extends BehaviorModel {
  motionFunc: (t: number) => ue.Vector2;
}

export class AreaMove extends AreaBehaviorBase<AreaMoveModel> {
  constructor(bindTo: AreaBehavior, modelToUse: AreaMoveModel) {
    super(bindTo, modelToUse);
    bindTo.onUpdate = () => this.onUpdate(); // 不套一层会导致this为global，即invoke时没提供this
  }

  onUpdate() {
    this.bindTo.area.position = ue.Vector2.op_Addition(
      this.bindTo.area.areaModel.position,
      this.model.motionFunc(this.bindTo.sim.timer.time * GameTimer.fixedUpdateTime)
    );
  }
}

export interface AreaLadderMoveModel extends AreaMoveModel {
  a: number;
  b: number;
  phase: number;
  amplitude: number;
  vertical: boolean;
}

export const LadderMove: AreaLadderMoveModel = {
  $impl: AreaMove,
  a: 2,
  b: 2,
  phase: 0,
  amplitude: 3,
  vertical: false,
  motionFunc(t) {
    const a = this.a,
      b = this.b,
      T = 2 * (a + b);
    t = (t + this.phase * T) % T;
    let d =
      t < a
        ? -1
        : t < a + b
        ? (2 / b) * t - (2 * a) / b - 1
        : t < 2 * a + b
        ? 1
        : -(2 / b) * t + (4 * a) / b + 3;
    d *= this.amplitude;
    return this.vertical ? new ue.Vector2(0, d) : new ue.Vector2(d, 0);
  },
};

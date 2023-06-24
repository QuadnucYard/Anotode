export function bindBehavior(bindTo: any, model: BehaviorModel) {
  Reflect.construct(model.$impl, [bindTo, Object.assign({}, model)]);
}

export function createBehavior(model: BehaviorModel) {
  if (!model.$base) {
    throw new Error("The behavior to create is null!");
  }
  const bindTo = Reflect.construct(model.$base, []);
  bindBehavior(bindTo, model);
  return bindTo;
}

export class BehaviorProxy<M extends BehaviorModel, B> {
  protected model: M;
  protected bindTo: B;

  constructor(bindTo: B, modelToUse: BehaviorModel) {
    this.bindTo = bindTo;
    this.model = modelToUse as M;
  }
}

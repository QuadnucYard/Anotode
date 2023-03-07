using System.Collections.Generic;
using System.Linq;
using Anotode.Models;
using Anotode.Utils.JSLoad;
using Puerts;

namespace Anotode.Simul.Objects {

	/// <summary>
	/// 生成JSObject描述的model对应的behavior，并绑定
	/// </summary>
	public class BehaviorAdapter<T> : Simulatable where T : BehaviorAdapter<T>, new() {

		private delegate void BehaviorCreateFunc(T bindTo, JSObject obj);
		private static readonly BehaviorCreateFunc createBehavior;

		static BehaviorAdapter() {
			createBehavior = JSDataLoader.vm.ExecuteModule<BehaviorCreateFunc>("core/adapter.js", "createBehavior");
		}

		public static T CreateFromModel(AdaptedBehaviorModel model, Simulation sim) {
			var beh = new T() { sim = sim };
			createBehavior(beh, model.obj);
			return beh;
		}

		public static List<T> CreateBehaviorsFromModels(IEnumerable<AdaptedBehaviorModel> models, Simulation sim) {
			return models.Select(t => CreateFromModel(t, sim)).ToList();
		}

	}
}

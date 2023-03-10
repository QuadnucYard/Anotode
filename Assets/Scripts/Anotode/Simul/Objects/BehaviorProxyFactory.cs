using System.Collections.Generic;
using System.Linq;
using Anotode.Models;
using Anotode.Utils.JSLoad;
using Puerts;

namespace Anotode.Simul.Objects {
	public class BehaviorProxyFactory {

		private delegate object BehaviorCreateFunc(JSObject obj);
		private static readonly BehaviorCreateFunc createBehavior;

		private delegate void BehaviorBindFunc(object bindTo, JSObject obj);
		private static readonly BehaviorBindFunc bindBehavior;

		static BehaviorProxyFactory() {
			createBehavior = JSDataLoader.vm.ExecuteModule<BehaviorCreateFunc>("core/adapter.js", "createBehavior");
			bindBehavior = JSDataLoader.vm.ExecuteModule<BehaviorBindFunc>("core/adapter.js", "bindBehavior");
		}

		public static T CreateFromModel<T>(AdaptedBehaviorModel model, Simulation sim) where T : Simulatable, new() {
			//var beh = (T)createBehavior(model.obj);
			var beh = new T();
			beh.sim = sim;
			bindBehavior(beh, model.obj);
			return beh;
		}

		public static List<T> CreateFromModels<T>(IEnumerable<AdaptedBehaviorModel> models, Simulation sim) where T : Simulatable, new() {
			return models.Select(t => CreateFromModel<T>(t, sim)).ToList();
		}
	}
}

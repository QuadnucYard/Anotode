using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Puerts;

namespace Anotode.Models {
	public class AdaptedBehaviorModel : Model {

		public JSObject obj;

		public override Model Clone() {
			throw new NotImplementedException();
		}

		public static AdaptedBehaviorModel Create(JSObject behavior) {
			return new AdaptedBehaviorModel() { obj = behavior };
		}

		public static AdaptedBehaviorModel[] Create(IEnumerable<JSObject> behaviors) {
			if (behaviors == null) return new AdaptedBehaviorModel[0];
			return behaviors.Select(t => Create(t)).ToArray();
		}
	}
}

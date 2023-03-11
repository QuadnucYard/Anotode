using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Anotode.Utils.JSLoad;
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

		public static IEnumerable<AdaptedBehaviorModel> Create(IEnumerable<JSObject> behaviors) {
			if (behaviors == null) return Enumerable.Empty<AdaptedBehaviorModel>();
			return behaviors.Select(t => Create(t));
		}

		public class JSObjectParser : IValueParser<AdaptedBehaviorModel> {
			public object Parse(JSObject value) {
				return Create(value);
			}
		}

		public class JSObjectParser2 : IValueParser<AdaptedBehaviorModel[]> {
			public object Parse(JSObject value) {
				if (value == null) return new AdaptedBehaviorModel[0];
				return Create(JSObjectConverter.Convert(typeof(List<JSObject>), value) as IEnumerable<JSObject>).ToArray();
			}
		}
	}
}

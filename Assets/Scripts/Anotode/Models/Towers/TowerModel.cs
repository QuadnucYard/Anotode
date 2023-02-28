using System;

namespace Anotode.Models.Towers {

	[Serializable]
	public class TowerModel : Model {

		public string id;

		public override Model Clone() {
			throw new NotImplementedException();
		}
	}
}

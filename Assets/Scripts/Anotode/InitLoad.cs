using Anotode.Data;
using Anotode.Utils.JSLoad;

namespace Anotode {
	public class InitLoad {

		public void Init() {
			JSDataLoader loader = new();
			loader.LoadTo(GameData.instance, "base/data");
		}
	}
}

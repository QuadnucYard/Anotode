using Anotode.Display.Towers;
using Anotode.Display.VM;
using Anotode.Models;
using Anotode.Models.Towers;

namespace Anotode.Display.Bridge {
	public class TowerController {

		public Tower towerView;
		private TowerModel towerModel;

		public void Init(UnityDisplayNode node, TowerModel model) {
			// 这里是简化的做法
			towerView = node.GetComponent<Tower>();
			towerModel = towerView.towerModel = model;
			towerView.SetSprite().Forget();
		}
	}
}

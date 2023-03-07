using Anotode.Display.VM;
using Anotode.Models;

namespace Anotode.Display.Bridge {
	public class EnemyController {
		//public UnityController ctrl;

		public Enemy enemyView;
		private EnemyModel enemyModel;

		public void Init(UnityDisplayNode node, EnemyModel model) {
			// 这里是简化的做法
			enemyView = node.GetComponent<Enemy>();
			enemyModel = enemyView.enemyModel = model;
			enemyView.SetSprite().Forget();
		}

		public void OnUpdate() {
		}

		public void OnDestroy() {
		}
	}
}

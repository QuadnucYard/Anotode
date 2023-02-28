using Anotode.Display.UI.InGame;
using Anotode.Models;
using UnityEngine;

namespace Anotode.Display.Bridge {
	public class EnemyController {
		//public UnityController ctrl;

		public Enemy enemyView;
		private EnemyModel enemyModel;

		public void Init(EnemyModel model) {
			// 这里是简化的做法
			enemyView = Object.Instantiate(Resources.Load<GameObject>("enemy")).GetComponent<Enemy>();
			enemyModel = enemyView.enemyModel = model;
			enemyView.transform.SetParent(GameStage.instance.enemyContainer);
			enemyView.SetSprite();
		}

		public void OnUpdate() {
			enemyView.transform.SetLocalPositionAndRotation(enemyModel.pos, Quaternion.Euler(0, 0, enemyModel.rotation));
		}

		public void OnDestroy() {
			Object.Destroy(enemyView.gameObject);
		}
	}
}

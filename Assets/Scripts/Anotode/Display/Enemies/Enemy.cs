using Anotode.Models;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Anotode.Display {
	public class Enemy : MonoBehaviour {

		public EnemyModel enemyModel;

		public async UniTaskVoid SetSprite() {
			var sr = gameObject.GetComponentInChildren<SpriteRenderer>();
			//sr.color = Random.ColorHSV();
			sr.sprite = await AssetsLoader.GetSprite(enemyModel.display);
			sr.sortingOrder = enemyModel.spawnIndex;
		}
	}
}

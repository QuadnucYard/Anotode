using Anotode.Models;
using UnityEngine;

namespace Anotode.Display {
	public class Enemy : MonoBehaviour {

		public EnemyModel enemyModel;

		public void SetSprite() {
			var sr = gameObject.GetComponentInChildren<SpriteRenderer>();
			//sr.color = Random.ColorHSV();
			sr.sprite = Resources.Load<Sprite>($"Graphics/Enemy/{enemyModel.id}");
			sr.sortingOrder = enemyModel.spawnIndex;
		}
	}
}

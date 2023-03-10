using Anotode.Models;
using Anotode.Models.Towers;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Anotode.Display.Towers {
	public class Tower : MonoBehaviour {
		public TowerModel towerModel;

		public async UniTaskVoid SetSprite() {
			var sr = gameObject.GetComponentInChildren<SpriteRenderer>();
			sr.sprite = await AssetsLoader.GetSprite(towerModel.display);
		}
	}
}

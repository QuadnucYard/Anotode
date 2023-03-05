using System.Collections.Generic;
using Anotode.Models.Map;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Anotode.Display.Map {
	public class GameMap : MonoBehaviour {

		public GameMapModel mapModel;
		public Tile tileTemplate;

		public List<TiledArea> areas;

		public async UniTaskVoid CreateMap(GameMapModel mapModel) {
			this.mapModel = mapModel;

			var areaPrefab = await AssetsManager.LoadAssetAsync<GameObject>("TiledArea");
			areas = new();
			foreach (var a in mapModel.tiledAreas) {
				var area = Instantiate(areaPrefab, transform).GetComponent<TiledArea>();
				await area.Create(a);
			}
		}

	}
}

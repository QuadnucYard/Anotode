using System.Linq;
using Anotode.Data;
using Anotode.Models.Map;
using Cysharp.Threading.Tasks;
using Quadnuc.Utils;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Anotode.Display.Map {
	public class TiledArea : MonoBehaviour {

		public TiledAreaModel areaModel;
		public Tilemap tilemap;

		public async UniTask Create(TiledAreaModel areaModel) {
			this.areaModel = (TiledAreaModel)areaModel.Clone();

			tilemap.transform.localPosition = -areaModel.pivotPoint;
			transform.localPosition = areaModel.position;

			var tileIndices = areaModel.tiles.Select(t => t.index).Distinct().ToArray();
			var tileAssets = await UniTask.WhenAll(
				tileIndices.Select(t => GameDataManager.GetTileData(t).AutoLoad())
			);

			areaModel.tiles.ForEach((t, i, j) => {
				tilemap.SetTile(new(i, j, 0), tileAssets[tileIndices.IndexOf(t.index)]);
			});

			var obj = await AssetsManager.LoadAssetAsync<GameObject>("MapObject");
			foreach (var p in areaModel.entrances) {
				var sr = Instantiate(obj, p).GetComponent<SpriteRenderer>();
				sr.sprite = await GameData.instance.others["spawner"].AutoLoad();
			}
			foreach (var p in areaModel.exits) {
				var sr = Instantiate(obj, p).GetComponent<SpriteRenderer>();
				sr.sprite = await GameData.instance.others["base"].AutoLoad();
			}
		}

		private GameObject Instantiate(GameObject original, Vector2Int position) {
			var obj = Instantiate(original, tilemap.transform, false);
			obj.transform.localPosition = new(position.x + 0.5f, position.y + 0.5f, 0);
			return obj;
		}
	}
}

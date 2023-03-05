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

			tilemap.transform.position = -areaModel.pivotPoint;
			transform.position = areaModel.position;

			var tileIndices = areaModel.tiles.Select(t => t.index).Distinct().ToArray();
			var tileAssets = await UniTask.WhenAll(
				tileIndices.Select(t => GameDataManager.GetTileData(t).LoadAsset())
			);

			areaModel.tiles.ForEach((t, i, j) => {
				tilemap.SetTile(new(i, j, 0), tileAssets[tileIndices.IndexOf(t.index)]);
			});
		}
	}
}

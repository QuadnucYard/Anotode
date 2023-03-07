using Anotode.Models.Map;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Anotode.Data.Maps {
	public class TileData {
		readonly string id;
		public readonly int index;
		readonly TileType type;
		readonly SpriteData sprite;

		private Tile tile;
		private AsyncLazy task;

		public async UniTask<Tile> AutoLoad() {
			task ??= LoadTile().ToAsyncLazy();
			await task;
			return tile;
		}

		private async UniTask LoadTile() {
			tile = ScriptableObject.CreateInstance<Tile>();
			tile.sprite = await sprite.AutoLoad();
		}

		public TileModel def => new() {
			id = id,
			index = index,
			type = type,
		};

	}
}

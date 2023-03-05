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
		public Tile tile;

		public async UniTask<Tile> LoadAsset() {
			if (tile) return tile;
			tile = ScriptableObject.CreateInstance<Tile>();
			tile.sprite = await sprite.AutoLoad();
			return tile;
		}

		public TileModel def => new() {
			id = id,
			index = index,
			type = type,
		};

	}
}

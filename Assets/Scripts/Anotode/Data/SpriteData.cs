using Anotode.Display;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Anotode.Data {
	public class SpriteData {
		readonly string filename;
		readonly float ppu;
		Sprite sprite;

		public async UniTask<Sprite> AutoLoad() {
			if (sprite) return sprite;
			var texture = await AssetsManager.LoadTexture(filename);
			sprite = texture.CreateSprite(ppu);
			return sprite;
		}
	}
}

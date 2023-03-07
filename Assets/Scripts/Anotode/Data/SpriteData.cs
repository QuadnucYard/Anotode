using Anotode.Display;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Anotode.Data {
	public class SpriteData {
		readonly string filename;
		readonly float ppu;

		Sprite sprite;
		AsyncLazy task;
		public readonly int token;

		public SpriteData() {
			token = AssetsLoader.AddSpriteReference(this);
		}

		public async UniTask<Sprite> AutoLoad() {
			task ??= LoadSprite().ToAsyncLazy();
			await task;
			return sprite;
		}

		private async UniTask LoadSprite() {
			var texture = await AssetsManager.LoadTexture(filename);
			sprite = texture.CreateSprite(ppu);
		}
	}
}

using System.Collections.Generic;
using Anotode.Data;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Anotode.Display {
	public class AssetsLoader {

		private static readonly List<SpriteData> _spriteReferences = new();

		public static int AddSpriteReference(SpriteData spriteData) {
			_spriteReferences.Add(spriteData);
			return _spriteReferences.Count - 1;
		}

		public static async UniTask<Sprite> GetSprite(int token) {
			return await _spriteReferences[token].AutoLoad();
		}

	}
}

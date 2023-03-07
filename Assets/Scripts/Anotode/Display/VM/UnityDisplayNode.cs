using UnityEngine;

namespace Anotode.Display.VM {
	public class UnityDisplayNode : MonoBehaviour {

		public string cloneOf;
		public bool isDestroyed;
		private bool initialized;

		public SpriteRenderer spriteRenderer { get; private set; }

		public void Initialize() { // 这个应该是首次创建调用的
			initialized = true;
			spriteRenderer = GetComponent<SpriteRenderer>();
		}

		public void Create() {
			if (!initialized) Initialize();
			gameObject.SetActive(true);
		}

		public void Destroy() {
			isDestroyed = true;
			gameObject.SetActive(false);
		}

	}
}

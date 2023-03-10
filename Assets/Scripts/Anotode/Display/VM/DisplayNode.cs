using System;
using UnityEngine;

namespace Anotode.Display.VM {
	public class DisplayNode {

		public DisplayNode parent { get; private set; }
		public UnityDisplayNode graphic { get; private set; }

		public readonly string prefabKey;

		public Vector3 position = Vector3.zero; // 局部坐标
		public Vector3 scale = Vector3.one;
		public float rotation = 0;

		private event Action<UnityDisplayNode> _onCreated;
		public event Action<UnityDisplayNode> onCreated {
			add {
				if (graphic) value?.Invoke(graphic);
				else _onCreated += value;
			}
			remove {
				_onCreated -= value;
			}
		}

		public DisplayNode(string prefabKey) {
			this.prefabKey = prefabKey;
		}

		public void SetParent(DisplayNode parent) {
			this.parent = parent;
			if (graphic != null) {
				graphic.transform.SetParent(parent.graphic.transform, false);
			}
		}

		public void Create() {
			// 目前不清楚是谁来调用create
			Game.instance.factory.CreateAsync(prefabKey, n => {
				graphic = n;
				if (parent != null) {
					if (parent.graphic == null) {
						parent.onCreated += n => graphic.transform.SetParent(n.transform);
					} else {
						graphic.transform.SetParent(parent.graphic.transform);
					}
				}
				Update();
				_onCreated?.Invoke(n);
				_onCreated = null;
			});
		}

		public void Destroy() {
			if (graphic)
				graphic.Destroy();
		}

		public void Update() {
			// 这个函数调用应该托管给behavior
			if (graphic) { // 这里是否做一个缓存更合适？
				graphic.transform.SetLocalPositionAndRotation(position, Quaternion.Euler(0, 0, rotation));
				graphic.transform.localScale = scale;
			}
		}

	}
}

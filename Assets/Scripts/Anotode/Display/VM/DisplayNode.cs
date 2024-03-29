﻿using System;
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
			if (prefabKey == null) return;
			Game.instance.factory.CreateAsync(prefabKey, n => {
				graphic = n;
				if (parent != null) {
					parent.onCreated += n => graphic.transform.SetParent(n.transform);
				}
				_onCreated?.Invoke(n);
				_onCreated = null;
				Update();
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

		public DisplayNode BindChild(int index) {
			DisplayNode node = new(null);
			node.SetParent(this);
			onCreated += n => node.graphic = n.transform.GetChild(index).gameObject.AddComponent<UnityDisplayNode>();
			return node;
		}

		public DisplayNode BindChild(string name) {
			DisplayNode node = new(null);
			node.SetParent(this);
			onCreated += n => node.graphic = n.transform.Find(name).gameObject.AddComponent<UnityDisplayNode>();
			return node;
		}

	}
}

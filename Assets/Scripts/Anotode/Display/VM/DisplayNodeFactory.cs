using System;
using System.Collections.Generic;
using System.Linq;
using Quadnuc.Utils;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Anotode.Display.VM {
	public class DisplayNodeFactory {

		private const bool DecayEnabled = true;
		private const int DecayInterval = 60;
		private const int DecayAtAge = 600;

		private readonly List<UnityDisplayNode> active = new();
		private readonly Dictionary<string, FactoryItem> items = new();

		private int timeAtLastDecay;

		private Transform DisplayRoot => Game.instance.displayObjects;
		//private Transform PrototypeRoot => Game.instance.prototypeObjects;

		public bool IsLoadingAnyPrototypes() => items.Values.Any(t => !t.prototypeHandle.IsDone);

		static DisplayNodeFactory() {
		}

		public DisplayNodeFactory() { }


		public void DestroyAllActive() {
			foreach (var n in active) {
				n.Destroy();
			}
		}

		public void Flush() {
			// 应该是把destroy的去掉
			UnusedFlush();
			ProtoFlush();
		}

		private void UnusedFlush() {
			// 这个是在干啥呢
			// 还缺啥？
		}

		private void ProtoFlush() {
			// 猜测是把handler清掉  清count为0的
			foreach (var item in items.Values) {
				if (item.count == 0 && item.prototypeHandle.IsDone) {
					Addressables.Release(item.prototypeHandle);
				}
			}
		}

		public void CreateAsync(string objectId, Action<UnityDisplayNode> onComplete) {
			if (!items.ContainsKey(objectId)) {
				items.Add(objectId, new() { prototypeHandle = Addressables.LoadAssetAsync<GameObject>(objectId) });
			}
			var item = items[objectId];
			if (item.pool.Count > 0) {
				FindAndSetupPrototypeAsync(objectId, onComplete);
				return;
			}
			item.prototypeHandle.Completed += h => {
				if (h.Status == AsyncOperationStatus.Succeeded) {
					var g = GameObject.Instantiate(h.Result, DisplayRoot).AddComponent<UnityDisplayNode>();
					g.cloneOf = objectId;
					onComplete?.Invoke(g);
					item.count++;
					g.Create();
				}
			};
		}

		private void FindAndSetupPrototypeAsync(string objectId, Action<UnityDisplayNode> onComplete) {
			var item = items[objectId];
			onComplete?.Invoke(item.pool.Pop().node);
			item.count++;
		}

		public void Tidy(int elapsed) {
			// 下面两个 可能做的是估算limit，和清理超过limit的
			// 另外要回收destroy的
			active.RemoveAll(n => {
				if (n.isDestroyed) {
					items[n.cloneOf].pool.Add(new() { node = n, entryTime = elapsed });
					return true;
				}
				return false;
			});
			DecayPools(elapsed);
		}

		private void DecayPools(int elapsed) {
			if (!DecayEnabled || elapsed - timeAtLastDecay < DecayInterval) {
				return;
			}
			// 这个应该是用来清理多余的
			timeAtLastDecay = elapsed;
			foreach (var item in items.Values) {
				item.pool.RemoveAll(n => {
					if (elapsed > n.entryTime + DecayAtAge) {
						GameObject.Destroy(n.node.gameObject);
						--item.count;
						return true;
					}
					return false;
				});
			}
		}

		private class FactoryItem {
			public List<PooledNode> pool = new();
			public int limit = 0;
			public int count = 0;
			public AsyncOperationHandle<GameObject> prototypeHandle;
		}

		private struct PooledNode {
			public UnityDisplayNode node;
			public int entryTime;
		}

	}
}

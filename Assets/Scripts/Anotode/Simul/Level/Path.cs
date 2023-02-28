using System.Collections.Generic;
using System.Linq;
using Quadnuc.Utils;

using Vector2 = UnityEngine.Vector2;

namespace Anotode.Simul.Level {

	public class Path {

		public readonly List<Vector2> path;
		public readonly int cost;

		public Path(List<Vector2> path = null, int cost = 0) {
			this.path = path ?? new();
			this.cost = cost;
		}

		public Vector2 this[int index] => path[index];

		public int Length => path?.Count > 0 ? path.Count : int.MaxValue;

		public bool Empty => path.Empty();

		public Vector2 First => path[^1];
		public Vector2 Last => path[0];

		public void Prepend(Vector2 v) {
			path.Insert(0, v);
		}

		public void Append(Vector2 v) {
			path.Add(v);
		}

		public Vector2 Pop() {
			return path.Pop();
		}

		/// <summary>
		/// 沿中轴线方向偏移一定距离
		/// </summary>
		/// <param name="d"></param>
		public void Offset(float d) {
			for (int i = 0; i < path.Count; i--) {
				int j = i + 1;
				while (j + 1 < path.Count && Mathh.IsColinear(path[i], path[j], path[j + 1]))
					j++;
				if (j == path.Count) break;
				Vector2 shift = Vector2.Perpendicular(path[i + 1] - path[i]) * d;
				while (i <= j) {
					path[i++] += shift;
				}
				// 注意拐角会移2次
			}
		}

		public override string ToString() {
			return $"Path({cost}): {string.Join(", ", path.Reversed().Select(p => $"({p.x},{p.y})"))}";
		}

	}
}

using System.Collections.Generic;
using System.Linq;
using Quadnuc.Utils;

using Vector2 = UnityEngine.Vector2;

namespace Anotode.Simul.Level {

	public class Path {

		public List<Vector2> path;
		public int cost;

		public Path(List<Vector2> path = null, int cost = 0) {
			this.path = path ?? new();
			this.cost = cost;
		}

		public Vector2 this[int index] => path[index];

		public int length => path?.Count > 0 ? path.Count : int.MaxValue;

		public bool empty => path.Empty();

		public void Prepend(Vector2 v) {
			path.Insert(0, v);
		}

		public void Append(Vector2 v) {
			path.Add(v);
		}

		public override string ToString() {
			return $"Path({cost}): {string.Join(", ", path.Select(p => $"({p.x},{p.y})"))}";
		}

	}
}

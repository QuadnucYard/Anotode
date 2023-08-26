using Anotode.Simul.Enemies;
using Vector3 = UnityEngine.Vector3;

namespace Anotode.Simul.Towers {
	// 这其实是一个union
	public struct Target {
		public Enemy enemy;
		public Vector3? position;
		public Tower tower;

		public bool valid => enemy != null || position != null || tower != null;
	}
}

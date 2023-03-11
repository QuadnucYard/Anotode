using System;
using Anotode.Data;
using Anotode.Utils.JSLoad;

namespace Anotode.Models.Towers.Projectiles {
	public class ProjectileModel : Model {
		public string id;
		[CustomParser(typeof(SpriteData.JSObjectParser))]
		public int display;
		public float radius;
		public bool ignoreBlockers;
		public bool usePointCollisionWithEnemies;
		public float scale;
		public AdaptedBehaviorModel[] behaviors;
		public string saveId;

		public override Model Clone() {
			return MemberwiseClone() as ProjectileModel;
		}
	}
}

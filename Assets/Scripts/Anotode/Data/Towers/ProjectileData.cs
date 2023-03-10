using System.Collections.Generic;
using System.Linq;
using Anotode.Display;
using Anotode.Models;
using Anotode.Models.Towers.Projectiles;
using Puerts;

namespace Anotode.Data.Towers {
	public class ProjectileData {
		readonly string id;
		readonly SpriteData sprite;
		readonly float radius;
		readonly bool ignoreBlockers;
		readonly bool usePointCollisionWithEnemies;
		readonly float scale;
		readonly List<JSObject> behaviors;

		public ProjectileModel def => new() {
			id = id,
			display = sprite.token,
			radius = radius,
			ignoreBlockers = ignoreBlockers,
			usePointCollisionWithEnemies = usePointCollisionWithEnemies,
			scale = scale,
			behaviors = AdaptedBehaviorModel.Create(behaviors).ToArray(),
		};
	}
}

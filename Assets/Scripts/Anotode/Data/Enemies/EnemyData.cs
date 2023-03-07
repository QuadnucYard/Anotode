using Anotode.Models;
using Anotode.Models.Map;

namespace Anotode.Data.Enemies {
	public class EnemyData {
		readonly string id;
		readonly string baseId;
		readonly SpriteData sprite;
		readonly float speed;
		readonly int population;
		readonly int charm;
		readonly int charmSelf;
		readonly int invasionDamage;
		readonly float hpFactor;
		readonly TilePassFlag passFlag;

		public EnemyModel def => new() {
			id = id,
			baseId = baseId,
			display = sprite.token,
			speed = speed,
			population = population,
			charm = charm,
			charmSelf = charmSelf,
			hpFactor = hpFactor,
			invasionDamage = invasionDamage,
			passFlag = passFlag,
		};
	}
}

using Anotode.Models;
using Anotode.Models.Map;

namespace Anotode.Data.Enemies {
	public class EnemyData {
		readonly string id;
		readonly string baseId;
		readonly float speed;
		readonly int population;
		readonly int charm;
		readonly int invasionDamage;
		readonly float hpFactor;
		readonly TilePassFlag passFlag;

		public EnemyModel def => new() {
			id = id,
			baseId = baseId,
			speed = speed * GlobalData.fixedUpdateTime,
			population = population,
			charm = charm,
			hpFactor = hpFactor,
			invasionDamage = invasionDamage,
			passFlag = passFlag,
		};
	}
}

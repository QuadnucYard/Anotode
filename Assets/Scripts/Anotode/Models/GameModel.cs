using System;
using System.Linq;
using Anotode.Models.Map;
using Anotode.Models.Towers;

namespace Anotode.Models {

	[Serializable]
	public class GameModel : Model {

		public float startingHealth;
		public float maxHealth;

		public bool towerSellEnabled;
		public int maxTowerCount;
		public int maxPowerCount;
		public int randomSeed;
		public bool reverseMode;

		public GameMapModel map;

		public TowerModel[] towers;
		public EnemyModel[] enemies;

		public GameModel() {
			
		}

		public override Model Clone() {
			throw new NotImplementedException();
		}

		public EnemyModel GetEnemy(string id) => enemies.First(t => t.id == id);
		public TowerModel GetTower(string id) => towers.First(t => t.id == id);
	}
}

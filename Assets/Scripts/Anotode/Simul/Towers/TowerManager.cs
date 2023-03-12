using System.Collections.Generic;
using System.Linq;
using Anotode.Models.Towers;
using Anotode.Simul.Objects;
using Vector2 = UnityEngine.Vector2;
using Vector2Int = UnityEngine.Vector2Int;
using Vector3 = UnityEngine.Vector3;

namespace Anotode.Simul.Towers {

	public class TowerManager : Simulatable {

		public delegate void TowerCreatedDelegate(Tower tower, TowerModel def, float upgradeCost, bool isFromSave, double moneyBefore, double moneyAfter);

		public event TowerCreatedDelegate onTowerCreated;

		public delegate void TowerSoldDelegate(Tower tower, float sellWorth, double moneyBefore, double moneAfter);

		public event TowerSoldDelegate onTowerSold;

		public delegate void TowerDestroyedDelegate(Tower tower);

		public event TowerDestroyedDelegate onTowerDestroyed;

		public delegate void TowerAreaChangedDelegate(Tower tower, ObjectId areaBefore, ObjectId areaAfter);

		public event TowerAreaChangedDelegate onTowerAreaChanged;

		public Tower CreateTower(
			TowerModel def,
			ObjectId areaPlacedOn,
			Vector2Int position,
			//TowerSaveDataModel loadingSaveData = null,
			bool deductCash = true,
			float rotation = 0.0f
		) {
			var tower = new Tower() { sim = sim, areaIdPlacedOn = areaPlacedOn };
			tower.Init(def);
			tower.cellPos = position;
			sim.map.areaTowers[areaPlacedOn].Add(tower);
			onTowerCreated?.Invoke(tower, def, 0, false, 0, 0);
			return null;
		}

		public void SellTower(Tower tower) {
			//onTowerSold?.Invoke(tower);
		}

		public void DestroyTower(Tower tower) {
			//onTowerDestroyed?.Invoke(tower);
		}

		public void ChangeTowerArea(Tower tower, ObjectId areaBefore, ObjectId areaAfter) {
			tower.areaIdPlacedOn = areaAfter;
			sim.map.areaTowers[areaBefore].Remove(tower);
			sim.map.areaTowers[areaAfter].Add(tower);
			onTowerAreaChanged?.Invoke(tower, areaBefore, areaAfter);
		}

		public Tower GetTowerById(ObjectId id) {
			return GetTowers().First(t => t.id == id);
		}

		public IEnumerable<Tower> GetTowersInRange(Vector3 position, float range) {
			return GetTowersInRange((Vector2)position, range);
		}

		public IEnumerable<Tower> GetTowersInRange(Vector2 position, float range) {
			return GetTowers().Where(t => Vector2.Distance(t.mapPos, position) < range);
		}

		//public IEnumerable<Tower> GetTowersByBaseId(string towerBaseId) => (IEnumerable<Tower>)null;

		public IEnumerable<Tower> GetTowers() => sim.map.areaTowers.Values.SelectMany(t => t);
	}
}
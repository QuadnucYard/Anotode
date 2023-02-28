using System;
using Anotode.Models.Map;
using UnityEngine;

namespace Anotode.Models {

	[Serializable]
	public class EnemyModel : Model {

		public string id;
		private float _level;
		public int hp;
		public int hpMax; //应该是根据等级算的
		public float hpRate;
		public int invasionDamage;
		public int charm;
		public float speed;
		public int population;
		public TilePassFlag passFlag;

		public Vector3 pos; // 暂时当它是全局坐标
		public float rotation;
		public int spawnIndex;

		public float level {
			get => _level;
			set {
				_level = value;
				RefreshLevel();
			}
		}

		public void RefreshLevel() {
			// 血量系数只用一个好了
			//this.hpMax = Mathf.Max(1, Mathf.RoundToInt(this.monsterData.getHpMax(m_level) * this.hpRate));
		}

		public override Model Clone() {
			return (EnemyModel)MemberwiseClone();
		}
	}
}

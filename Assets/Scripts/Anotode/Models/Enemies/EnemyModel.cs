﻿using System;
using Anotode.Models.Map;
using Quadnuc.Utils;
using UnityEngine;

namespace Anotode.Models
{

	[Serializable]
	public class EnemyModel : Model {

		public string id;
		public string baseId;
		public int display;
		public int invasionDamage;
		public int charm;
		public int charmSelf;
		public float speed;
		public int population;
		public Quadratic hpFactor;
		public TilePassFlag passFlag;

		private float _level;
		public int hp;
		public int hpMax { get; private set; }
		public float hpRate;

		//public Vector3 pos; // 暂时当它是全局坐标
		//public float rotation;
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
			hpMax = Mathf.Max(1, Mathf.RoundToInt(hpFactor.Eval(_level) * hpRate));
		}

		public override Model Clone() {
			return (EnemyModel)MemberwiseClone();
		}
	}
}

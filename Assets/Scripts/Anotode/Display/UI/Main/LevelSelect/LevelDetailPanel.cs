using System;
using System.Collections;
using Anotode.Data;
using Anotode.Display.UI.Menu;
using Anotode.Utils.Locale;
using TMPro;
using UnityEngine;

namespace Anotode.Display.UI.Main.LevelSelect {
	public class LevelDetailPanel : GameMenu {


		public Action onConfirm;
		public Action onCancel;

		/// <summary>
		/// 加载关卡数据
		/// </summary>
		/// <param name="levelId"></param>
		public void LoadLevel(string levelId) {
			var ld = GameDataManager.getLevel(levelId);
			//txtMapName.
			// 考虑加一个动态匹配的机制
			// 比如
		}
	}
}
using System;
using Anotode.Data;
using Anotode.Display.UI.Menu;
using UnityEngine.UIElements;

namespace Anotode.Display.UI.Main.LevelSelect {
	public class LevelSelectScreen : GameMenu {

		VisualElement doc;

		public event Action<string> onLevelSelect;

		private void Awake() {
			doc = GetComponent<UIDocument>().rootVisualElement;
		}

		private void Start() {
			Open(null);
		}

		public override void Open(object data) {
			base.Open(data);

			var itemContainer = doc.Q("ScrollView").Q("unity-content-container");
			itemContainer.Clear();
			foreach (var levelModel in GameDataManager.allLevels) {
				var item = new LevelSelectItem(levelModel);
				itemContainer.Add(item);
				item.onLevelSelect += levelId => onLevelSelect?.Invoke(levelId);
			}
		}

	}
}

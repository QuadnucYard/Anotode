using Anotode.Display.UI.Main.LevelSelect;
using Anotode.Display.UI.Menu;
using Quadnuc.Utils;
using UnityEngine.UI;

namespace Anotode.Display.UI.Main {
	public class MainMenu : GameMenu {

		public LevelSelectScreen levelSelectScreen;
		public Button btnPlay;

		private void OnEnable() {
			levelSelectScreen.Activate();
		}

		private void Start() {
			//btnPlay.onClick.AddListener(() => levelSelectScreen.Open(null));
			levelSelectScreen.onLevelSelect += levelId => {
				levelSelectScreen.Deactivate();
				Game.instance.StartGameLevel(levelId);
			};
		}
	}
}
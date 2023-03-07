using Anotode.Display.UI.InGame;
using Anotode.Display.VM;
using Anotode.Display.UI.Main;
using Quadnuc.Utils;
using UnityEngine;

namespace Anotode.Display {
	public class Game : MonoBehaviour {

		public static Game instance;

		public MainMenu mainMenu;
		public GameStage gameStage;

		public DisplayNodeFactory factory;
		public Transform prototypeObjects;
		public Transform displayObjects;

		private void Awake() {
			instance = this;
			factory = new();
		}

		private void OnEnable() {
			mainMenu.Activate();
			gameStage.Deactivate();
		}

		public void StartGameLevel(string levelId) {
			gameStage.Activate();
			gameStage.StartGame(levelId);
		}

	}
}
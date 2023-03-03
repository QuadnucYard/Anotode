using System.Collections;
using Anotode.Display.UI.InGame;
using Assets.Scripts.Anotode.Display.UI.Main;
using Quadnuc.Utils;
using UnityEngine;

namespace Assets.Scripts.Anotode.Display {
	public class Game : MonoBehaviour {

		public static Game instance;

		public MainMenu mainMenu;
		public GameStage gameStage;

		private void Awake() {
			instance = this;
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
using System.Collections;
using Anotode.Data;
using Anotode.Display.Bridge;
using Anotode.Models;
using Anotode.Models.Map;
using Anotode.Simul;
using Quadnuc.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Anotode.Display.UI.InGame {
	public class GameStage : MonoBehaviour {

		public static GameStage instance { get; private set; }
		public Camera sceneCamera;
		public UnityController bridge;
		public Map.GameMap map;

		public Transform enemyContainer;

		//Simulation sim;

		public Button btnStartWave;

		private void Awake() {
			instance = this;
			sceneCamera = Camera.main;
		}

		private void Start() {
			btnStartWave.onClick.AddListener(() => bridge.StartWave());
		}

		public void StartGame(string levelId) {
			Simulation sim = new();
			var level = GameDataManager.getLevel(levelId);
			sim.Init(new() {
				enemies = GameDataManager.allEnemies
			});

			sim.InitMap(level.map);
			map.CreateMap(sim.model.map);

			bridge = new() {
				simulation = sim
			};
			StartCoroutine(GameCycle());
		}

		private IEnumerator GameCycle() {
			while (true) {
				bridge.simulation.Simulate();
				yield return new WaitForSeconds(GlobalData.fixedUpdateTime);
			}
		}
	}
}
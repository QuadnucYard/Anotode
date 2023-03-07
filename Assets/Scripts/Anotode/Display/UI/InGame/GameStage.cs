using System.Collections;
using Anotode.Data;
using Anotode.Display.Bridge;
using Anotode.Display.Map;
using Anotode.Models;
using Anotode.Models.Map;
using Anotode.Simul;
using Cysharp.Threading.Tasks;
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
			// !important 事件的顺序很重要

			Simulation sim = new();
			var level = GameDataManager.getLevel(levelId);
			sim.Init(new() {
				enemies = GameDataManager.allEnemies
			});

			sim.InitLevel(level);
			foreach (var a in sim.map.areas) {
				a.displayNode.onCreated += n => n.GetComponent<TiledArea>().Create(a.areaModel).Forget();
			}

			// 这里应该异步先加载所有资源
			map.CreateMap(sim.model.map); // NOTE: 这里没有await

			bridge = new();
			// 初始化委托事件
			bridge.onWaveStart += r => {
				Debug.Log($"Wave start {r}");
				btnStartWave.enabled = false;
			};
			bridge.onWaveSpawnEnd += r => { 
				Debug.Log($"Wave spawn end {r}");
				btnStartWave.enabled = true;
			};
			bridge.onWaveEnd += r => Debug.Log(r);

			bridge.Init(sim);
			sim.InitEvents();

			StartCoroutine(GameCycle());
		}

		private IEnumerator GameCycle() {
			while (true) {
				Game.instance.factory.Tidy(bridge.simulation.timer.time);
				bridge.simulation.Simulate();
				yield return new WaitForSeconds(GlobalData.fixedUpdateTime);
			}
		}
	}
}
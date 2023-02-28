using System.Collections;
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
		}

		private void Start() {
			btnStartWave.onClick.AddListener(() => bridge.StartWave());
		}

		private void OnEnable() {
			Simulation sim = new();
			sim.Init(new() {
				enemies = new EnemyModel[] {
					new() {
						id = "basic",
						speed = 1.0f / 60.0f,
						population = 1,
						charm = -20,
						passFlag = TilePassFlag.Land,
					},
					new() {
						id = "strong",
						speed = 0.5f / 60.0f,
						population = 2,
						charm = 50,
						passFlag = TilePassFlag.Land,
					},
					new() {
						id = "fast",
						speed = 2.0f / 60.0f,
						population = 2,
						charm = -100,
						passFlag = TilePassFlag.Land,
					},
				}
			});

			int[,] tileTypes = new int[,] {
				{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
				{1,1,1,0,1,1,1,1,0,0,0,0,1,0,1 },
				{1,0,1,0,1,0,0,1,1,1,1,1,1,1,1 },
				{1,1,1,1,1,0,0,1,1,0,1,1,0,1,1 },
				{1,0,0,1,1,1,1,1,1,0,1,1,0,1,0 },
				{1,1,1,1,0,0,1,1,0,0,1,1,1,1,1 },
				{1,1,0,1,1,1,1,1,1,1,1,1,0,1,1 },
				{1,1,0,1,0,0,0,0,1,1,1,1,0,0,1 },
				{1,1,0,1,1,1,1,1,1,0,1,1,0,1,1 },
				{1,1,1,1,0,0,0,1,1,1,1,1,1,1,1 }, // 15*10
			}.Transposed();

			TileInfo[,] tiles = tileTypes.Map((t, i, j) => new TileInfo((TileType)tileTypes[i, tileTypes.GetLength(1) - 1 - j]));
			TiledAreaModel tiledAreaModel = new() {
				tiles = tiles,
				entrances = new Vector2Int[] { new(0, 0) },
				exits = new Vector2Int[] { new(14, 9) },
			};
			sim.InitMap(new() { tiledAreas = new TiledAreaModel[] { tiledAreaModel } });
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
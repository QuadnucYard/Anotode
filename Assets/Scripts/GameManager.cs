using System;
using System.Collections;
using Anotode.Models.Map;
using Anotode.Simul.Level;
using Anotode.Unity.Map;
using Quadnuc.Utils;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public Anotode.Unity.Map.GameMap map;
	Path thepath;

	// Start is called before the first frame update
	void Start() {
		int[,] tileTypes = new int[,] {
			{1,1,1,1,1,1,1,1 },
			{1,1,1,0,1,1,1,1 },
			{1,1,1,0,1,0,0,1 },
			{1,1,1,1,1,0,0,1 },
			{1,0,0,1,1,1,1,1 },
			{1,1,1,1,1,1,1,1 },
		}.Transposed();

		TileInfo[,] tiles = tileTypes.Map((t, i, j) => new TileInfo((TileType)tileTypes[tileTypes.GetLength(0) - 1 - i, j]));
		TiledAreaModel tiledAreaModel = new() {
			tiles = tiles
		};
		map.mapModel = new GameMapModel() { tiledAreas = new TiledAreaModel[] { tiledAreaModel } };
		map.CreateMap();

		var area = new TiledArea(tiledAreaModel);

		var pf = new Pathfinding();
		var p =pf.find(area.frame, new(0, 0), new(5, 5), TilePassFlag.Land);
		Debug.Log(Enumerables.ToString(p.path));
		thepath = p;
		
	}

	// Update is called once per frame
	void Update() {

	}

	private void OnDrawGizmos() {
		if (thepath == null || thepath.path == null) return;
		foreach (var pp in thepath.path) {
			Gizmos.DrawSphere(pp + new Vector2(0.5f, 0.5f), 0.2f);
		}
	}

}

﻿using System.Collections.Generic;
using Anotode.Data.Enemies;
using Anotode.Data.Maps;
using Anotode.Data.Towers;

namespace Anotode.Data {
	public class GameData {

		public static GameData instance { get; private set; } = new();

		public readonly List<EnemyData> enemies;
		public readonly List<GameMapData> maps;
		public readonly List<LevelData> levels;
		public readonly List<TileData> tiles;
		public readonly List<TowerData> towers;

		public readonly Dictionary<string, SpriteData> others;
	}
}

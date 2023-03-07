﻿using Anotode.Data;
using Anotode.Utils.JSLoad;
using Anotode.Utils.Locale;
using Quadnuc.Utils;
using UnityEngine;

namespace Anotode {
	public class InitLoad {

		public static bool initialized { get; private set; } = false;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		public static void Init() {
			Application.runInBackground = true;

			JSDataLoader loader = new(AssetPaths.dataPath.ToString());
			loader.LoadTo(GameData.instance, "base/data");
			JSObjectConverter.ClearReferences();

			Localization.InitLoad();
			initialized = true;
			Debug.Log("Initialized.");
		}
	}
}

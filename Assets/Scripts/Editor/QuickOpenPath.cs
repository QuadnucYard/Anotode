using UnityEditor;
using UnityEngine;

namespace Quadnuc.Editor {
	public class QuickOpenPath {

		private static void showInExplorer(string path) {
			Debug.Log("open: " + path);
			System.Diagnostics.Process.Start(path);
		}

		[MenuItem("Tools/Open persistentDataPath")]
		private static void open_persistentDataPath() {
			showInExplorer(Application.persistentDataPath);
		}

		[MenuItem("Tools/Open dataPath")]
		private static void open_dataPath() {
			showInExplorer(Application.dataPath);
		}

		[MenuItem("Tools/Open streamingAssetsPath")]
		private static void open_streamingAssetsPath() {
			showInExplorer(Application.streamingAssetsPath);
		}

		[MenuItem("Tools/Open consoleLogPath")]
		private static void open_consoleLogPath() {
			showInExplorer(Application.consoleLogPath);
		}

		[MenuItem("Tools/Open temporaryCachePath")]
		private static void open_temporaryCachePath() {
			showInExplorer(Application.temporaryCachePath);
		}
	}
}

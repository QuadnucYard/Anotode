using Cysharp.Threading.Tasks;
using Quadnuc.Utils;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

namespace Anotode.Display {
	public static class AssetsManager {

		private static readonly string StreamingAssetsPath =
#if UNITY_ANDROID && !UNITY_EDITOR
			Application.streamingAssetsPath;
#elif UNITY_IPHONE && !UNITY_EDITOR
			"file://" + Application.streamingAssetsPath;
#elif UNITY_STANDLONE_WIN || UNITY_EDITOR
			"file://" + Application.streamingAssetsPath;
#else
			string.Empty;
#endif

		public static T LoadAsset<T>(string key) {
			var op = Addressables.LoadAssetAsync<T>(key);
			return op.WaitForCompletion();
		}

		public static async UniTask<T> LoadAssetAsync<T>(string key) {
			return await Addressables.LoadAssetAsync<T>(key).Task;
		}

		private static string ToUrl(string file) =>
			file
			.Replace("__base__", System.IO.Path.Combine(StreamingAssetsPath, "data", "base"))
			.Replace("__core__", System.IO.Path.Combine(StreamingAssetsPath, "data", "core"));

		public static async UniTask<byte[]> LoadData(string path) {
			var re = await UnityWebRequest.Get(ToUrl(path)).SendWebRequest();
			return re.downloadHandler.data;
		}

		public static async UniTask<string> LoadText(string path) {
			var re = await UnityWebRequest.Get(ToUrl(path)).SendWebRequest();
			return re.downloadHandler.text;
		}

		public static async UniTask<Texture2D> LoadTexture(string path) {
			var re = await UnityWebRequestTexture.GetTexture(ToUrl(path)).SendWebRequest();
			return DownloadHandlerTexture.GetContent(re);
		}

		public static Rect Rect(this Texture2D texture) {
			return new(0, 0, texture.width, texture.height);
		}

		public static Sprite CreateSprite(this Texture2D texture, float ppu, Vector2 pivot) {
			return Sprite.Create(texture, texture.Rect(), pivot, ppu);
		}

		public static Sprite CreateSprite(this Texture2D texture, float ppu) {
			return CreateSprite(texture, ppu, Mathh.vec2center);
		}

	}
}

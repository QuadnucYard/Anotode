using System;
using System.Collections.Generic;
using System.IO;
using Quadnuc.Utils;
using Swifter.Json;
using Swifter.RW;

namespace Anotode.Utils.Locale {
	public static class Localization {

		private static Dictionary<string, LanguagePack> langs;

		public static string currentLanguage { get; private set; }

		private const string defaultLanguage = "en";

		public static Action onLanguageChanged;

		public static bool initialized { get; private set; } = false;

		public static void InitLoad() {
			langs = new();
			// Load core files
			var coreDir = (AssetPaths.dataPath / "core" / "locale").Directory;
			foreach (var di in coreDir.EnumerateDirectories()) {
				string k = di.Name;
				foreach (var fi in di.EnumerateFiles()) {
					if (fi.Name == "info.json") {
						langs.Add(di.Name, new() {
							key = k,
							info = JsonFormatter.DeserializeObject<LanguageInfo>(fi.OpenText()),
							items = new Properties(),
						});
					}
				}
			}
			// Load base files
			LoadLocaleDirectory((AssetPaths.dataPath / "core" / "locale").Directory);
			LoadLocaleDirectory((AssetPaths.dataPath / "base" / "locale").Directory);

			SetLanguage();

			initialized = true;
		}

		private static void LoadLocaleDirectory(DirectoryInfo dir) {
			if (!dir.Exists) return;
			foreach (var di in dir.EnumerateDirectories()) {
				string k = di.Name;
				if (!langs.ContainsKey(k)) continue;
				foreach (var fi in di.EnumerateFiles()) {
					if (fi.Extension == ".cfg" || fi.Extension == ".lang") {
						langs[di.Name].items.Load(fi.OpenText());
					}
				}
			}
		}

		public static void SetLanguage() {
			SwitchLanguage(defaultLanguage);
		}

		public static void SwitchLanguage(string lang) {
			if (!langs.ContainsKey(lang)) {
				throw new Exception($"Unknown language: {lang}");
			}
			currentLanguage = lang;
			onLanguageChanged?.Invoke();
		}

		public static string GetLocalText(string key) {
			if (!initialized) return key;
			if (langs.TryGetValue(currentLanguage, out var lang)) {
				return lang.items.GetValueOrDefault(key, key);
			}
			return key;
		}

		class LanguagePack {
			public string key;
			public LanguageInfo info;
			public Properties items;
		}

		struct LanguageInfo {
			[RWField("language-name")]
			public string languageName;
			public string[] fonts;
		}
	}
}

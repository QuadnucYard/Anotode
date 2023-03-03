using TMPro;
using UnityEngine;

namespace Anotode.Utils.Locale {

	[RequireComponent(typeof(TMP_Text))]
	public class LocaleText : MonoBehaviour {

		public TMP_Text tmp;
		public string key;
		[SerializeField]
		private string rawText;
		public object[] args;

		public string text { get => tmp.text; set => tmp.text = value; }

		private void Awake() {
			if (!tmp) tmp = GetComponent<TMP_Text>();
		}

		public void SetKey(string key) {
			this.key = key;
			rawText = Localization.GetLocalText(key);
			text = rawText; // Need get text
		}

		public void Format(params object[] args) {
			this.args = args;
			text = string.Format(rawText, args);
		}

		private void onLanguageChanged() {
			rawText = Localization.GetLocalText(key);
			text = string.Format(rawText, args);
		}

		private void OnDestroy() {
			Localization.onLanguageChanged -= onLanguageChanged;
		}
	}
}

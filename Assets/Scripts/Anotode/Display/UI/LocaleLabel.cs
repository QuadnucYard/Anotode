using System.Collections.Generic;
using Anotode.Utils.Locale;
using Quadnuc.Utils;
using UnityEngine.UIElements;

namespace Anotode.Display.UI {
	public class LocaleLabel : Label {

		public new class UxmlFactory : UxmlFactory<LocaleLabel, UxmlTraits> { }

		public new class UxmlTraits : TextElement.UxmlTraits {
			private UxmlStringAttributeDescription m_Key = new() {
				name = "key",
				defaultValue = "",
			};

			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription {
				get {
					yield break;
				}
			}

			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc) {
				base.Init(ve, bag, cc);
				LocaleLabel textElement = (LocaleLabel)ve;
				textElement.key = m_Key.GetValueFromBag(bag, cc);
			}
		}

		private string m_Key;
		private string m_RawText;
		private object[] m_Args;

		public LocaleLabel() {
			Localization.onLanguageChanged += onLanguageChanged;
			onLanguageChanged();
		}

		~LocaleLabel() {
			Localization.onLanguageChanged -= onLanguageChanged;
		}

		public string key {
			get => m_Key;
			set {
				m_Key = value;
				m_Args = null;
				SetLocaleText();
			}
		}

		public void Format(params object[] args) {
			m_Args = args;
			text = string.Format(m_RawText, args);
		}

		public void FormatKey(string key, params object[] args) {
			m_Key = key;
			m_Args = args;
			SetLocaleText();
		}

		private void onLanguageChanged() {
			SetLocaleText();
		}

		private void SetLocaleText() {
			m_RawText = key?.Length > 0 ? Localization.GetLocalText(key) : key;
			text = m_Args == null ? m_RawText : string.Format(m_RawText, m_Args);
		}

		/**
		 * 分几种情况
		 * 1. 设置key。get文本，不format。需要废弃args
		 * 2. format。修改text
		 * 3. 语言更改。get文本，用之前的args来format
		 */

	}
}

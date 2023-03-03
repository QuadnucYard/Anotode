using System;
using Anotode.Models.Map;
using UnityEngine.AddressableAssets;
using UnityEngine.UIElements;

namespace Anotode.Display.UI.Main.LevelSelect {
	public class LevelSelectItem : VisualElement {

		public new class UxmlFactory : UxmlFactory<LevelSelectItem> { }

		readonly TemplateContainer template;

		public event Action<string> onLevelSelect;

		public LevelSelectItem() {
			var op = Addressables.LoadAssetAsync<VisualTreeAsset>("LevelSelectItem");
			template = op.WaitForCompletion().Instantiate();
			template.style.flexGrow = 1;
			hierarchy.Add(template);
		}

		public LevelSelectItem(LevelModel levelModel) : this() {
			userData = levelModel;
			template.Q<LocaleLabel>("MapNameValue").key = $"{levelModel.map.id}-name";
			template.Q<LocaleLabel>("LevelNameValue").key = $"{levelModel.id}-name";
			template.Q<Button>().clicked += () => onLevelSelect?.Invoke(levelModel.id);
		}

	}
}

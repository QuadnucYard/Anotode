using UnityEngine;
using UnityEngine.UI;

namespace Quadnuc.Utils {

	public class DebugUILine : MonoBehaviour {

		private static readonly Vector3[] fourCorners = new Vector3[4];

		[ExecuteInEditMode]
		void OnDrawGizmos() {
			foreach (var g in FindObjectsOfType<MaskableGraphic>()) {
				if (g.raycastTarget) {
					RectTransform rectTransform = g.transform as RectTransform;
					rectTransform.GetWorldCorners(fourCorners);
					Gizmos.color = Color.blue;
					for (int i = 0; i < 4; i++)
						Gizmos.DrawLine(fourCorners[i], fourCorners[(i + 1) % 4]);
				}
			}
		}

	}
}


﻿using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

namespace Quadnuc.Editor {

	public class UITools {

		/*
		/// <summary>
		/// 自动取消RatcastTarget
		/// </summary>
		[MenuItem("GameObject/UI/Image")]
		static void CreatImage() {
			if (Selection.activeTransform) {
				if (Selection.activeTransform.GetComponentInParent<Canvas>()) {
					GameObject go = new GameObject("image", typeof(Image));
					go.GetComponent<Image>().raycastTarget = false;
					go.transform.SetParent(Selection.activeTransform);
				}
			}
		}
		//重写Create->UI->Text事件  
		[MenuItem("GameObject/UI/Text")]
		static void CreatText() {
			if (Selection.activeTransform) {
				//如果选中的是列表里的Canvas  
				if (Selection.activeTransform.GetComponentInParent<Canvas>()) {
					//新建Text对象  
					GameObject go = new GameObject("text", typeof(Text));
					//将raycastTarget置为false  
					go.GetComponent<Text>().raycastTarget = false;
					//设置其父物体  
					go.transform.SetParent(Selection.activeTransform);
				}
			}
		}

		//重写Create->UI->Text事件  
		[MenuItem("GameObject/UI/Raw Image")]
		static void CreatRawImage() {
			if (Selection.activeTransform) {
				//如果选中的是列表里的Canvas  
				if (Selection.activeTransform.GetComponentInParent<Canvas>()) {
					//新建Text对象  
					GameObject go = new GameObject("RawImage", typeof(RawImage));
					//将raycastTarget置为false  
					go.GetComponent<RawImage>().raycastTarget = false;
					//设置其父物体  
					go.transform.SetParent(Selection.activeTransform);
				}
			}
		}*/

		[MenuItem("Tools/Disable all RaycastTarget (Except buttons)")]
		static void DisableAllRaycastTarget() {
			foreach (var g in Object.FindObjectsOfType<MaskableGraphic>()) {
				if (!g.gameObject.GetComponent<Button>())
					g.raycastTarget = false;
			}
		}

		[MenuItem("Tools/Disable all RaycastTarget in selection")]
		static void DisableAllRaycastTargetSelection() {
			foreach (var go in Selection.gameObjects) {
				foreach (var g in go.GetComponents<MaskableGraphic>())
					g.raycastTarget = false;
			}

		}

	}
}

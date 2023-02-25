using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace Quadnuc.Utils {
	public static class GameObjectExtensions {

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddChild(this GameObject parent, GameObject child) {
			if (child != null)
				child.transform.SetParent(parent.transform, false);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddChild(this GameObject parent, Component child) {
			if (child != null)
				child.transform.SetParent(parent.transform, false);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddChild(this Component parent, GameObject child) {
			if (child != null)
				child.transform.SetParent(parent.transform, false);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddChild(this Component parent, Component child) {
			if (child != null)
				child.transform.SetParent(parent.transform, false);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Activate(this Component component) {
			component.gameObject.SetActive(true);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Deactivate(this Component component) {
			component.gameObject.SetActive(false);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetPosition(this GameObject gameObject, Vector3 position) {
			gameObject.transform.localPosition = position;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetPosition(this Component component, Vector3 position) {
			component.transform.localPosition = position;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetPosition(this GameObject gameObject, float x, float y, float z = 0) {
			gameObject.transform.localPosition = new(x, y, z);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetPosition(this Component component, float x, float y, float z = 0) {
			component.transform.localPosition = new(x, y, z);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetScale(this GameObject gameObject, float x, float y, float z = 1) {
			gameObject.transform.localScale = new(x, y, z);
		}

		public static void DestroyChildren(this Transform transform) {
			for (int i = 0; i < transform.childCount; i++) {
				GameObject.Destroy(transform.GetChild(0).gameObject);
			}
		}

		public static void SetX(this Transform transform, float x) {
			Vector3 _pos = transform.localPosition;
			transform.localPosition = new Vector3(x, _pos.y, _pos.z);
		}

		public static void SetY(this Transform transform, float y) {
			Vector3 _pos = transform.localPosition;
			transform.localPosition = new Vector3(_pos.x, y, _pos.z);
		}

		public static void SetZ(this Transform transform, float z) {
			Vector3 _pos = transform.localPosition;
			transform.localPosition = new Vector3(_pos.x, _pos.y, z);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetLocalScale(this Transform transform, float s) {
			transform.localScale = new Vector3(s, s, s);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetRotationZ(this Transform transform, float r) {
			transform.rotation = Quaternion.Euler(0, 0, r);
		}


		/// <summary>
		/// 查找子物体（递归查找）  
		/// </summary> 
		/// <param name="trans">父物体</param>
		/// <param name="goName">子物体的名称</param>
		/// <returns>找到的相应子物体</returns>
		public static Transform FindChildRecursively(this Transform trans, string goName) {
			Transform child = trans.Find(goName);
			if (child != null) return child;
			for (int i = 0; i < trans.childCount; i++) {
				child = trans.GetChild(i);
				Transform go = FindChildRecursively(child, goName);
				if (go != null) return go;
			}
			return null;
		}
	}
}
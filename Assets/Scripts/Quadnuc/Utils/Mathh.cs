using System.Runtime.CompilerServices;
using UnityEngine;

namespace Quadnuc.Utils {

	/// <summary>
	/// 扩展数学库
	/// </summary>
	public static class Mathh {

		public static readonly Vector2 vec2center = new(0.5f, 0.5f);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Polar(float length, float angle) {
			return new(length * Mathf.Cos(angle), length * Mathf.Sin(angle));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 PolarXY(float length, float angle) {
			return new(length * Mathf.Cos(angle), length * Mathf.Sin(angle));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="center">中心</param>
		/// <param name="span">跨度</param>
		/// <param name="count">数量</param>
		/// <param name="index">第几个，从0开始</param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Intersperse(float center, float span, int count, int index) {
			if (count <= 1) return center;
			return center - span / 2 + span / (count - 1) * index;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Arrange(float center, float space, int count, int index) {
			return center + (index * 2 - count + 1) * space / 2;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Range(float start, float end, int count, int index) {
			if (count <= 1) return start;
			return start + (end - start) / (count - 1) * index;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float LerpExp(float a, float b, float t) {
			return a + (b - a) * (1 - Mathf.Exp(-t));
		}

	}
}

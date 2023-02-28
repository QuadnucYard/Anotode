using System.Runtime.CompilerServices;
using UnityEngine;

namespace Quadnuc.Utils {

	/// <summary>
	/// UnityEngine.Vector extensions.
	/// </summary>
	public static class VectorExtensions {

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int Offset(this Vector2Int vec2i, int dx, int dy)
			=> new(vec2i.x + dx, vec2i.y + dy);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int Left(this Vector2Int vec2i)
			=> new(vec2i.x - 1, vec2i.y);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int Right(this Vector2Int vec2i)
			=> new(vec2i.x + 1, vec2i.y);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int Up(this Vector2Int vec2i)
			=> new(vec2i.x, vec2i.y + 1);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int Down(this Vector2Int vec2i)
			=> new(vec2i.x, vec2i.y - 1);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int RoundToInt(this Vector2 vec2)
			=> new(Mathf.RoundToInt(vec2.x), Mathf.RoundToInt(vec2.y));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3Int RoundToInt(this Vector3 vec3)
			=> new(Mathf.RoundToInt(vec3.x), Mathf.RoundToInt(vec3.y), Mathf.RoundToInt(vec3.z));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int FloorToInt(this Vector2 vec2)
			=> new(Mathf.FloorToInt(vec2.x), Mathf.FloorToInt(vec2.y));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3Int FloorToInt(this Vector3 vec3)
			=> new(Mathf.FloorToInt(vec3.x), Mathf.FloorToInt(vec3.y), Mathf.FloorToInt(vec3.z));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Offset(this Vector2 vec2, float dx, float dy)
			=> new(vec2.x + dx, vec2.y + dy);

	}
}

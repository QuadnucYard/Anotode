using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Anotode.Models;
using Quadnuc.Utils;
using Mathf = UnityEngine.Mathf;
using Vector2 = UnityEngine.Vector2;
using Vector2Int = UnityEngine.Vector2Int;

#nullable enable
namespace Anotode.Simul.Level {

	public enum PathMode {
		Floor, Air, Water, Check,
	}

	[DebuggerDisplay("ArealVector2(p={pos,nq}, area={area.id,nq})")]
	public struct ArealVector2 {
		public Vector2 pos;
		public TiledArea area;
	}

	public class Pathfinding {

		private static readonly Vector2Int[] directions = new Vector2Int[] { Vector2Int.right, Vector2Int.up, Vector2Int.left, Vector2Int.down, Vector2Int.zero };
		private const int maxTry = 1000;

		public Path? Find(Enemy enemy, GameMap.PositionInArea[] pExits) {
			Path? bestPath = null;
			var w = new Stopwatch();
			w.Start();
			foreach (var pa in pExits) {
				var path = Find(enemy.enemyModel, enemy.areaIn, enemy.localPos, pa.area, pa.pos);
				if (bestPath == null || path?.cost < bestPath.cost) bestPath = path;
			}
			w.Stop();
			UnityEngine.Debug.Log($"find {w.ElapsedMilliseconds}");
			return bestPath;
		}

		public Path? Find(EnemyModel enemy, TiledArea startArea, Vector2 startPos, TiledArea endArea, Vector2 endPos) {
			return new Closure(enemy, startArea, startArea.LocalToCell(startPos), endArea, endArea.LocalToCell(endPos)).Find();
		}

		public readonly struct Closure {
			readonly EnemyModel enemy;
			readonly TiledArea startArea;
			readonly Vector2Int startPos;
			readonly TiledArea endArea;
			readonly Vector2Int endPos;

			static readonly PriorityQueue<Node> openSet = new();
			static readonly HashSet<Node> closeSet = new();

			public Closure(EnemyModel enemy, TiledArea startArea, Vector2Int startPos, TiledArea endArea, Vector2Int endPos) {
				this.enemy = enemy;
				this.startArea = startArea;
				this.startPos = startPos;
				this.endArea = endArea;
				this.endPos = endPos;
				openSet.Clear();
				closeSet.Clear();
			}

			public Path? Find() {
				if (startPos == endPos) {
					return new Path(new() { startArea.CellToLocal(startPos) }, 0, startArea);
				}

				Node startNode = new(startPos, startArea);
				Node targetNode = new(endPos, endArea);
				openSet.Enqueue(startNode);
				closeSet.Add(startNode);

				for (int tries = maxTry; !openSet.Empty && tries > 0; tries--) {
					Node cur = openSet.Dequeue();

					if (cur.p == endPos && cur.areaIn == endArea) {
						return cur.GetPath();
					}

					foreach (Node n in GetArounds(cur)) {
						if (closeSet.Contains(n))
							continue;
						// 这里需要注意一下，如果当前格是传送，找邻居的时候会改变当前点，这里应该不额外加上距离
						// 考虑在tile实现distance策略
						n.fCost = cur.fCost +
							n.areaIn.frame.costMap[n.p.x, n.p.y] +
							n.areaIn.frame.charmMap[n.p.x, n.p.y] * enemy.charmSelf +
							Node.Distance(n, cur);
						n.gCost = Node.Distance(n, targetNode);
						openSet.Enqueue(n);
						closeSet.Add(n);
					}
				}
				return null;
			}

			private IEnumerable<Node> GetArounds(Node n) {
				// 这里根据坐标找区域
				var mapPos = n.areaIn.CellToMap(n.p);
				for (int k = 0; k < 4; k++) {
					var point = mapPos + (Vector2)directions[k] * 0.501f;
					var a = startArea.map.GetAreaAtPoint(point);
					if (a == null)
						continue;
					Vector2Int pos = a.MapToCell(point);
					if (checkPos(a, pos)) {
						yield return new(pos, a) { parent = n };
					}
				}
			}

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private bool checkPos(TiledArea area, Vector2Int pos) {
				return area.areaModel.ContainsPoint(pos) && (area.frame.passMap[pos.x, pos.y] & enemy.passFlag).Any();
			}
		}

		[DebuggerDisplay("Node(p={p}, f={fCost}, h={hCost})")]
		public class Node : IEquatable<Node>, IComparable<Node> {
			public readonly Vector2Int p; // 局部坐标
			public readonly TiledArea areaIn;

			public int fCost; //起始点到当前点的距离
			public int gCost; //当前点到终点的距离
			public Node? parent;

			public Node(Vector2Int p, TiledArea areaIn) {
				this.p = p;
				this.areaIn = areaIn;
			}

			public int hCost => fCost + gCost;

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public int CompareTo(Node other) {
				return fCost.CompareTo(other.fCost);
			}

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool Equals(Node? other) {
				return other != null && p == other.p && areaIn == other.areaIn;
			}

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public override int GetHashCode() {
				return HashCode.Combine(p.GetHashCode(), areaIn.GetHashCode());
			}

			//public static bool operator <(Node lhs, Node rhs) => lhs.CompareTo(rhs) < 0;
			//public static bool operator >(Node lhs, Node rhs) => lhs.CompareTo(rhs) > 0;
			//public static bool operator ==(Node lhs, Node rhs) => lhs.Equals(rhs);
			//public static bool operator !=(Node lhs, Node rhs) => !lhs.Equals(rhs);

			//public static implicit operator Vector2(Node node) => node.p;

			//public static implicit operator Vector2Int(Node node) => node.p;

			public static int Distance(Node a, Node b) {
				float distance;
				if (a.areaIn == b.areaIn) {
					distance = Vector2.Distance(a.p, b.p);
				} else {
					var aa = a.areaIn.LocalToMap(a.p);
					var bb = b.areaIn.LocalToMap(b.p);
					distance = Vector2.Distance(aa, bb);
				}
				return (int)(distance * 1000);
			}

			public Path GetPath() {
				List<Vector2> arr = new();
				TiledArea area = areaIn;
				for (Node cur = this; cur!.parent != null; cur = cur.parent!) {
					if (cur.areaIn != area) {
						area = cur.areaIn;
						arr.Clear();
					}
					arr.Add(new(cur.p.x + 0.5f, cur.p.y + 0.5f));
				}
				return new(arr, hCost, area, area == areaIn);
			}

		}

	}
}

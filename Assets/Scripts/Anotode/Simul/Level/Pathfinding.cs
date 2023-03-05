using System;
using System.Collections.Generic;
using System.Diagnostics;
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

	public struct ArealVector2 {
		public Vector2 pos;
		public TiledArea area;
	}

	public class Pathfinding {

		private static readonly Vector2Int[] directions = new Vector2Int[] { Vector2Int.right, Vector2Int.up, Vector2Int.left, Vector2Int.down, Vector2Int.zero };
		private const int maxTry = 10000;

		public Path? Find(Enemy enemy, GameMap.PositionInArea[] pExits) {
			Path? bestPath = null;
			foreach (var pa in pExits) {
				var path = Find(enemy.enemyModel, enemy.areaIn, enemy.localPos, pa.area, pa.pos);
				if (bestPath == null || path?.cost < bestPath.cost) bestPath = path;
			}
			return bestPath;
		}

		public Path? Find(EnemyModel enemy, TiledArea startArea, Vector2 startPos, TiledArea endArea, Vector2 endPos) {
			return new Closure(enemy, startArea, startArea.LocalToCell(startPos), endArea, endArea.LocalToCell(endPos)).Find();
		}

		public class Closure {
			readonly EnemyModel enemy;
			readonly TiledArea startArea;
			readonly Vector2Int startPos;
			readonly TiledArea endArea;
			readonly Vector2Int endPos;

			readonly PriorityQueue<Node> openSet;
			readonly HashSet<Node> closeSet;

			public Closure(EnemyModel enemy, TiledArea startArea, Vector2Int startPos, TiledArea endArea, Vector2Int endPos) {
				this.enemy = enemy;
				this.startArea = startArea;
				this.startPos = startPos;
				this.endArea = endArea;
				this.endPos = endPos;
				openSet = new();
				closeSet = new();
			}

			public Path? Find() {
				if (startPos == endPos) {
					return new Path(new() { startArea.CellToLocal(startPos) }, 0, startArea);
				}

				Node startNode = new(startPos, startArea);
				Node targetNode = new(endPos, endArea);
				openSet.Enqueue(startNode);

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
						closeSet.Add(cur);
					}
				}
				return null;
			}

			private IEnumerable<Node> GetArounds(Node n) {
				// 区域内
				for (int k = 0; k < 4; k++) {
					Vector2Int pos = n.p + directions[k];
					if (checkPos(n.areaIn, pos)) {
						yield return new(pos, n.areaIn) { parent = n };
					}
				}
				// 下面判断其他区域是否存在点能衔接上
				var globalPos = n.areaIn.CellToGlobal(n.p);
				foreach (var a in n.areaIn.map.areas) {
					if (a == n.areaIn) continue;
					for (int k = 0; k < 4; k++) {
						var loc = a.GlobalToCell(globalPos + directions[k]);
						if (checkPos(a, loc)) {
							yield return new(loc, a) { parent = n };
						}
					}
				}
			}

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

			public int CompareTo(Node other) {
				return fCost.CompareTo(other.fCost);
			}

			public bool Equals(Node? other) {
				return other != null && p == other.p && areaIn == other.areaIn;
			}

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
					var aa = a.areaIn.LocalToGlobal(a.p);
					var bb = b.areaIn.LocalToGlobal(b.p);
					distance = Vector2.Distance(aa, bb);
				}
				return (int)(distance * 100);
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

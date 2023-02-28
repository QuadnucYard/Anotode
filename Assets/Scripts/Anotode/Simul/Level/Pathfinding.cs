using System;
using System.Collections.Generic;
using Anotode.Models.Map;
using Quadnuc.Utils;
using Mathf = UnityEngine.Mathf;
using Vector2 = UnityEngine.Vector2;
using Vector2Int = UnityEngine.Vector2Int;

namespace Anotode.Simul.Level {

	public enum PathMode {
		Floor, Air, Water, Check,
	}

	// TODO: area和坐标的关系
	public class Pathfinding {

		private static readonly Vector2Int[] directions = new Vector2Int[] { Vector2Int.right, Vector2Int.up, Vector2Int.left, Vector2Int.down };
		private const int maxTry = 10000;

		public Path Find(Vector2 pStart, GameMap.PositionInArea[] pExits, TilePassFlag passFlag) {
			Path bestPath = null;
			foreach (var pa in pExits) {
				var path = Find(pa.area.frame, pStart.FloorToInt(), pa.pos, passFlag);
				if (bestPath == null || path?.cost < bestPath.cost) bestPath = path;
			}
			return bestPath;
		}

		public Path Find(TiledAreaFrame area, Vector2 pStart, Vector2 pEnd, TilePassFlag passFlag) {
			return Find(area, pStart.FloorToInt(), pEnd.FloorToInt(), passFlag);
		}

		public Path Find(TiledAreaFrame area, Vector2Int pStart, Vector2Int pEnd, TilePassFlag passFlag) {
			if (pStart == pEnd) {
				return new Path(new() { pEnd }, 0);
			}

			PriorityQueue<Node> openSet = new();
			HashSet<Node> closeSet = new();

			// 每个网格都默认分配一个结点
			Node[,] nodeMap = new Node[area.xGrid, area.yGrid];
			nodeMap.Generate((i, j) => new(i, j, area.passMap[i, j]));

			Node startNode = nodeMap[pStart.x, pStart.y];
			Node targetNode = nodeMap[pEnd.x, pEnd.y];
			openSet.Enqueue(startNode);

			for (int tries = maxTry; !openSet.Empty && tries > 0; tries--) {
				Node currentNode = openSet.Dequeue();

				if (currentNode == pEnd) {
					return GetPath(startNode, currentNode);
				}

				foreach (Node n in GetArounds(area, nodeMap, currentNode)) {
					if ((n.flag & passFlag) == TilePassFlag.None || closeSet.Contains(n))
						continue;
					// 这里需要注意一下，如果当前格是传送，找邻居的时候会改变当前点，这里应该不额外加上距离
					// 考虑在tile实现distance策略
					// 需要重写一下？
					if (!closeSet.Contains(n)) {
						n.fCost = currentNode.fCost + area.costMap[currentNode.x, currentNode.y] + Node.Distance(n, currentNode);
						n.gCost = Node.Distance(n, targetNode);
						n.parent = currentNode;
						openSet.Enqueue(n);
						closeSet.Add(currentNode);
					}
				}
			}
			return null;
		}

		private IEnumerable<Node> GetArounds(TiledAreaFrame map, Node[,] nodeMap, Vector2Int p) {
			for (int k = 0; k < 4; k++) {
				Vector2Int _checkPos = p + directions[k];
				if (map.area.areaModel.ContainsPoint(_checkPos)) {
					yield return nodeMap[_checkPos.x, _checkPos.y];
				}
			}
		}

		private Path GetPath(Node startNode, Node cur) {
			List<Vector2> arr = new();
			int cost = cur.hCost;
			while (cur.x != startNode.x || cur.y != startNode.y) {
				arr.Add(new(cur.x + 0.5f, cur.y + 0.5f));
				cur = cur.parent;
			}
			return new(arr, cost);
		}


		public class Node : IComparable<Node> {
			public readonly TilePassFlag flag;
			public readonly int x;
			public readonly int y;
			public int fCost;//起始点到当前点的距离
			public int gCost;//当前点到终点的距离
			public Node parent;

			public Node(int x, int y, TilePassFlag flag) {
				this.flag = flag;
				this.x = x;
				this.y = y;
			}

			public int hCost => fCost + gCost;

			public int CompareTo(Node other) {
				return fCost.CompareTo(other.fCost);
			}

			public static bool operator <(Node lhs, Node rhs) => lhs.CompareTo(rhs) < 0;
			public static bool operator >(Node lhs, Node rhs) => lhs.CompareTo(rhs) > 0;

			public static implicit operator Vector2(Node node) => new(node.x, node.y);

			public static implicit operator Vector2Int(Node node) => new(node.x, node.y);

			public static int Distance(Node a, Node b) {
				return (int)(Mathf.Sqrt(Mathf.Pow(a.x - b.x, 2) + Mathf.Pow(a.y - b.y, 2)) * 100);
			}
		}

	}
}

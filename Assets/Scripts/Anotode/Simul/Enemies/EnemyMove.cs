using Anotode.Simul.Level;
using Vector2 = UnityEngine.Vector2;

namespace Anotode.Simul.Enemies {
	public class EnemyMove {
		/// <summary>
		/// 进家判定距离。
		/// TODO 移到gamemodel里去
		/// </summary>
		private const float InvasionDistance = 0.2f;
		private const int PathRefindInterval = GlobalData.framesPerSecond * 5;
		private const int PathRefindIntervalWhenNull = GlobalData.framesPerSecond;
		private const int InitialPathTime = -10000;
		private const float TransferThreshold = 1.001f;

		private Simulation sim;
		private readonly Enemy enemy;

		private Path _path;
		private float _innatePathOffset;
		private int _lastPathTime;
		private bool _isTransferring;

		public float distanceTraveled { get; private set; }

		public EnemyMove(Enemy enemy) {
			this.enemy = enemy;
		}

		public void Init() {
			this.sim = enemy.sim;
			distanceTraveled = 0;
			_lastPathTime = InitialPathTime;
			_isTransferring = false;
			_innatePathOffset = UnityEngine.Random.Range(-0.2f, 0.2f);
		}

		public void Process() {
			if ((_path == null || _path.Empty) && sim.timer.time - _lastPathTime > PathRefindIntervalWhenNull) FindPath();
			if (_path != null) StepMove(sim.timer.elapsed);
		}

		private void FindPath() {
			_path = sim.map.pathfinding.Find(enemy, sim.map.areaExits);
			if (_path != null) {
				_path.Offset(_innatePathOffset);
				if (_path.area != enemy.areaIn) {
					_isTransferring = true;
				}
			}
			_lastPathTime = sim.timer.time;
		}

		private void StepMove(int elapsed) {
			// 这里特判一下，如果足够进就进家
			if (_path.ended && Vector2.Distance(enemy.localPos, _path.Last) < InvasionDistance) {
				_path = null;
				enemy.Invade();
				return;
			}
			float stepLength = enemy.enemyModel.speed * elapsed * GlobalData.fixedUpdateTime;
			// Here compute speed.
			if (_isTransferring) {
				CheckTransfer(stepLength);
			} else {
				CheckMove(stepLength);
			}
			//controller.OnUpdate();
		}

		private void CheckTransfer(float stepLength) {
			if (_path == null || _path.Empty) return;
			var currentPoint = enemy.mapPos;
			var pathFirst = _path.area.LocalToMap(_path.First);
			var displacement = pathFirst - currentPoint;
			float distance = displacement.magnitude;
			if (distance > TransferThreshold) {
				_isTransferring = false;
				_path = null;
				return;
			}
			enemy.localRotation = Vector2.SignedAngle(Vector2.up, displacement);
			if (distance <= stepLength) { // Transfer complete
				enemy.mapPos = pathFirst;
				TransferArea(_path.area);
				//CheckMove(stepLength - distance);
			} else {
				enemy.mapPos += displacement.normalized * stepLength;
				if (_path.area.ContainsMapPoint(enemy.mapPos)) {
					TransferArea(_path.area);
				}
			}
		}

		private void CheckMove(float stepLength) {
			if (_path.area != enemy.areaIn) throw new System.Exception("xxxx");
			//if (!areaIn.ContainsMapPoint(mapPos)) throw new System.Exception("xxxxxx");
			if (_path.Empty) return;
			distanceTraveled += stepLength;
			var currentPoint = enemy.localPos;
			Vector2 displacement;
			float distance;
			do {
				displacement = _path.First - currentPoint;
				distance = displacement.magnitude;
				if (distance > stepLength) break;
				stepLength -= distance;
				currentPoint = _path.Pop();
				if (_path.Empty && _path.ended) {
					_path = null;
					enemy.Invade();
					return;
				}
				if (_path.Empty) {
					enemy.localPos = currentPoint;
					FindPath();
					//CheckTransfer(stepLength);
					return;
				}
				if (sim.timer.time - _lastPathTime > PathRefindInterval) {
					enemy.localPos = currentPoint;
					enemy.localRotation = Vector2.SignedAngle(Vector2.up, displacement);
					FindPath();
				}
				_lastPathTime = sim.timer.time;
			} while (!_path.Empty);
			enemy.localPos = currentPoint + displacement.normalized * stepLength;
			enemy.localRotation = Vector2.SignedAngle(Vector2.up, displacement);
		}

		private void TransferArea(TiledArea to) {
			// 还是没解决local pos 直接继承的问题
			if (enemy.areaIdIn == to.id) {
				throw new System.Exception("Fucking transfer!");
			}
			var mapPos = enemy.areaIn.LocalToMap(enemy.localPos);
			sim.enemyManager.EnemyAreaChanged(enemy, enemy.areaIn.id, to.id);
			enemy.displayNode.SetParent(to.subDisplayNode);
			//localPos = to.MapToLocal(mapPos);
			enemy.mapPos = mapPos;
			_isTransferring = false;
			if (enemy.areaIn != to) {
				throw new System.Exception("Fucking transfer!");
			}
		}
	}
}

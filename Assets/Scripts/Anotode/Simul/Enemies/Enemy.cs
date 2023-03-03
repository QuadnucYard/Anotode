using Anotode.Display.Bridge;
using Anotode.Models;
using Anotode.Simul.Level;
using Anotode.Simul.Physics;
using Vector2 = UnityEngine.Vector2;

namespace Anotode.Simul {
	public class Enemy : Collidable, IProcessable {

		/// <summary>
		/// 进家判定距离。
		/// TODO 移到gamemodel里去
		/// </summary>
		private const float InvasionDistance = 0.2f;
		private const int PathRefindInterval = GlobalData.framesPerSecond * 5;

		public readonly EnemyModel enemyModel;
		//private int health;
		private Path _path;
		private int _lastPathTime;
		public int spawnIndex;
		public int spawnTime;
		private float _innatePathOffset;

		public bool dead { get; private set; }

		public EnemyController controller;

		public float distanceTraveled { get; private set; }

		public TiledArea areaIn;

		public Vector2 localPos => enemyModel.pos;
		public Vector2 mapPos => enemyModel.pos;

		public Enemy(EnemyModel model) {
			enemyModel = model;
			// 目前还不知道projectile的坐标在哪
		}

		public void Init() {
			distanceTraveled = 0;
			spawnTime = sim.timer.time;
			_lastPathTime = -1000;
			_innatePathOffset = UnityEngine.Random.Range(-0.2f, 0.2f);

			// Create view
			controller = new() { };
			controller.Init(enemyModel);
			controller.OnUpdate();
		}

		public void Process() {
			if (dead) return;
			if (_path == null && sim.timer.time - _lastPathTime > PathRefindInterval) FindPath();
			if (_path != null) StepMove(sim.timer.elapsed);
		}

		private void FindPath() {
			_path = sim.map.pathfinding.Find(enemyModel.pos, sim.map.areaExits, enemyModel.passFlag);
			_path?.Offset(_innatePathOffset);
			_lastPathTime = sim.timer.time;
		}

		public void StepMove(int elapsed) {
			// 下面这个特判好像不需要？
			if (Vector2.Distance(enemyModel.pos, _path.Last) < InvasionDistance) {
				Invade();
				return;
			}
			float _stepLength = enemyModel.speed * elapsed * GlobalData.fixedUpdateTime;
			// Here compute speed.
			CheckMove(_stepLength);
			controller.OnUpdate();
		}

		private void CheckMove(float stepLength) {
			distanceTraveled += stepLength;
			Vector2 currentPoint = enemyModel.pos;
			Vector2 displacement = _path.First - currentPoint;
			float distance = displacement.magnitude;
			while (distance < stepLength) {
				stepLength -= distance;
				currentPoint = _path.Pop();
				if (sim.timer.time - _lastPathTime > PathRefindInterval) {
					FindPath();
				}
				_lastPathTime = sim.timer.time;
				if (_path.Empty) {
					// 这里应该判断是否真的进家了
					Invade();
					return;
				}
				displacement = _path.First - currentPoint;
				distance = displacement.magnitude;
			}
			enemyModel.pos = currentPoint + displacement.normalized * stepLength;
			enemyModel.rotation = Vector2.SignedAngle(Vector2.up, displacement);
		}

		private void Invade() {
			_path = null;
			dead = true;
			Destroy();
		}

		private void Destroy() {
			controller.OnDestroy();
		}

	}
}

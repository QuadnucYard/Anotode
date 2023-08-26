﻿using Anotode.Display.Bridge;
using Anotode.Models;
using Anotode.Simul.Level;
using Anotode.Simul.Physics;
using Vector2 = UnityEngine.Vector2;

namespace Anotode.Simul {
	public class Enemy : Collidable {

		/// <summary>
		/// 进家判定距离。
		/// TODO 移到gamemodel里去
		/// </summary>
		private const float InvasionDistance = 0.2f;
		private const int PathRefindInterval = GlobalData.framesPerSecond * 5;
		private const int PathRefindIntervalWhenNull = GlobalData.framesPerSecond;
		private const int InitialPathTime = -10000;
		private const float TransferThreshold = 1.001f;

		public readonly EnemyModel enemyModel;
		//private int health;
		private Path _path;
		private int _lastPathTime;
		private bool _isTransferring;

		public int spawnIndex;
		public int spawnTime;
		private float _innatePathOffset;

		public bool dead { get; private set; }

		public EnemyController controller { get; private set; }

		public float distanceTraveled { get; private set; }

		public ObjectId areaIdIn;
		public TiledArea areaIn {
			get => sim.map.GetAreaById(areaIdIn);
			set => areaIdIn = value.id;
		}

		public Vector2 localPos {
			get => displayNode.position;
			set => displayNode.position = value;
		}
		public Vector2 mapPos {
			get => areaIn.LocalToMap(localPos);
			set => localPos = areaIn.MapToLocal(value);
		}
		public float localRotation {
			get => displayNode.rotation;
			set => displayNode.rotation = value;
		}

		public Enemy(EnemyModel model) {
			enemyModel = model;
			// 目前还不知道projectile的坐标在哪
		}

		public void Init() {
			distanceTraveled = 0;
			spawnTime = sim.timer.time;
			_lastPathTime = InitialPathTime;
			_isTransferring = false;
			_innatePathOffset = UnityEngine.Random.Range(-0.2f, 0.2f);

			enemyModel.hp = enemyModel.hpMax;
			dead = false;

			// Create view
			controller = new();

			displayNode = new("Enemy");
			displayNode.SetParent(areaIn.subDisplayNode);
			displayNode.Create();
			displayNode.onCreated += n => {
				controller.Init(n, enemyModel);
			};
			// 这里暂时让controller托管资源的加载
			process += Process;
		}

		private void Process() {
			if (dead) return;
			if ((_path == null || _path.Empty) && sim.timer.time - _lastPathTime > PathRefindIntervalWhenNull) FindPath();
			if (_path != null) StepMove(sim.timer.elapsed);
			displayNode.Update();
		}

		private void FindPath() {
			_path = sim.map.pathfinding.Find(this, sim.map.areaExits);
			if (_path != null) {
				_path.Offset(_innatePathOffset);
				if (_path.area != areaIn) {
					_isTransferring = true;
				}
			}
			_lastPathTime = sim.timer.time;
		}

		private void StepMove(int elapsed) {
			// 这里特判一下，如果足够进就进家
			if (_path.ended && Vector2.Distance(localPos, _path.Last) < InvasionDistance) {
				Invade();
				return;
			}
			float stepLength = enemyModel.speed * elapsed * GlobalData.fixedUpdateTime;
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
			var currentPoint = mapPos;
			var pathFirst = _path.area.LocalToMap(_path.First);
			var displacement = pathFirst - currentPoint;
			float distance = displacement.magnitude;
			if (distance > TransferThreshold) {
				_isTransferring = false;
				_path = null;
				return;
			}
			localRotation = Vector2.SignedAngle(Vector2.up, displacement);
			if (distance <= stepLength) { // Transfer complete
				mapPos = pathFirst;
				TransferArea(_path.area);
				//CheckMove(stepLength - distance);
			} else {
				mapPos += displacement.normalized * stepLength;
				if (_path.area.ContainsMapPoint(mapPos)) {
					TransferArea(_path.area);
				}
			}
		}

		private void CheckMove(float stepLength) {
			if (_path.area != areaIn) throw new System.Exception("xxxx");
			//if (!areaIn.ContainsMapPoint(mapPos)) throw new System.Exception("xxxxxx");
			if (_path.Empty) return;
			distanceTraveled += stepLength;
			var currentPoint = localPos;
			Vector2 displacement;
			float distance;
			do {
				displacement = _path.First - currentPoint;
				distance = displacement.magnitude;
				if (distance > stepLength) break;
				stepLength -= distance;
				currentPoint = _path.Pop();
				if (_path.Empty && _path.ended) {
					Invade();
					return;
				}
				if (_path.Empty) {
					localPos = currentPoint;
					FindPath();
					//CheckTransfer(stepLength);
					return;
				}
				if (sim.timer.time - _lastPathTime > PathRefindInterval) {
					localPos = currentPoint;
					localRotation = Vector2.SignedAngle(Vector2.up, displacement);
					FindPath();
				}
				_lastPathTime = sim.timer.time;
			} while (!_path.Empty);
			localPos = currentPoint + displacement.normalized * stepLength;
			localRotation = Vector2.SignedAngle(Vector2.up, displacement);
		}

		private void TransferArea(TiledArea to) {
			// 还是没解决local pos 直接继承的问题
			if (areaIdIn == to.id) {
				throw new System.Exception("Fucking transfer!");
			}
			var mapPos = areaIn.LocalToMap(localPos);
			sim.enemyManager.EnemyAreaChanged(this, areaIn.id, to.id);
			displayNode.SetParent(to.subDisplayNode);
			//localPos = to.MapToLocal(mapPos);
			this.mapPos = mapPos;
			_isTransferring = false;
			if (areaIn != to) {
				throw new System.Exception("Fucking transfer!");
			}
		}

		private void Invade() {
			_path = null;
			Destroy();
		}

		private void Destroy() {
			//controller.OnDestroy();
			//this.sim.map.RemoveEnemy(this);
			dead = true;
			displayNode.Destroy();
		}

		public void Damage(DamagePayload payload) {
			enemyModel.hp -= payload.amount;
			if (enemyModel.hp <= 0) {
				Destroy();
			}
		}

	}
}

using Anotode.Display.Bridge;
using Anotode.Models;
using Anotode.Simul.Level;
using Anotode.Simul.Physics;
using Vector2 = UnityEngine.Vector2;

namespace Anotode.Simul.Enemies {
	public class Enemy : Collidable {

		public readonly EnemyModel enemyModel;
		//private int health;

		public readonly EnemyMove move;

		public int spawnIndex;
		public int spawnTime;

		public bool dead { get; private set; }

		public EnemyController controller { get; private set; }

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
			move = new EnemyMove(this);
		}

		public void Init() {
			spawnTime = sim.timer.time;
			move.Init();

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
			move.Process();
			displayNode.Update();
		}

		internal void Invade() {
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

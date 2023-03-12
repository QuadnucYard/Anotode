using System.Collections.Generic;
using System.Linq;
using Anotode.Display.Bridge;
using Anotode.Models.Towers;
using Anotode.Simul.Level;
using Anotode.Simul.Objects;
using Anotode.Simul.Towers.Behaviors.Attacks;
using Vector2 = UnityEngine.Vector2;
using Vector2Int = UnityEngine.Vector2Int;

namespace Anotode.Simul.Towers {
	public class Tower : Simulatable {

		public int createdAt;
		public int createdAtWaveTime;

		public TowerModel towerModel { get; set; }

		public List<TowerBehavior> towerBehaviors { get; private set; }

		public ObjectId areaIdPlacedOn;
		public TiledArea areaPlacedOn { 
			get => sim.map.GetAreaById(areaIdPlacedOn);
			set => areaIdPlacedOn = value.id;
		}

		public Vector2Int cellPos { 
			get => areaPlacedOn.LocalToCell(localPos); 
			set => localPos = areaPlacedOn.CellToLocal(value);
		}
		public Vector2 localPos {
			get => displayNode.position;
			set => displayNode.position = value;
		}
		public Vector2 mapPos {
			get => areaPlacedOn.LocalToMap(localPos);
			set => localPos = areaPlacedOn.MapToLocal(value);
		}

		public TowerController controller { get; private set; }

		public void Init(TowerModel towerModel) {
			this.towerModel = towerModel;
			towerBehaviors = BehaviorProxyFactory.CreateFromModels<TowerBehavior>(towerModel.behaviors, sim);
			towerBehaviors.AddRange(towerModel.attacks.Select(t => {
				var a = new Attack() { sim = sim, tower = this };
				a.Init(t);
				return a;
			}));
			towerBehaviors.ForEach(t => t.tower = this);

			controller = new();

			displayNode = new("Tower");
			displayNode.SetParent(areaPlacedOn.displayNode);
			displayNode.Create();
			displayNode.onCreated += n => {
				controller.Init(n, towerModel);
			};

			process += Process;
		}

		public void Process() {
			towerBehaviors.ForEach(t => t.onUpdate?.Invoke());
			displayNode.Update();
		}

	}
}

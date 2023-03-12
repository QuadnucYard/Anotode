namespace Anotode.Simul.Objects {
	public abstract class BaseObject {
		public ObjectId id { get; protected set; }

		public Simulation.ProcessDelegate process;

		public BaseObject() {
			id = ObjectId.Next();
		}

	}
}

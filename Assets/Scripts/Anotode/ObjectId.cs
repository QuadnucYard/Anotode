using System;
using Yitter.IdGenerator;

namespace Anotode {

	public struct ObjectId : IEquatable<ObjectId>, IComparable<ObjectId> {
		private long data;

		static ObjectId() {
			var options = new IdGeneratorOptions(6);
			YitIdHelper.SetIdGenerator(options);
		}

		public static ObjectId Next() => new() { data = YitIdHelper.NextId() };

		public static ObjectId FromData(long data) => new() { data = data };

		public int CompareTo(ObjectId other) => data.CompareTo(other.data);

		public bool Equals(ObjectId other) => data.Equals(other.data);

		public override int GetHashCode() => data.GetHashCode();

		public override bool Equals(object obj) => obj is ObjectId id && Equals(id);

		public override string ToString() => data.ToString();

		public static bool operator ==(ObjectId left, ObjectId right) => left.Equals(right);

		public static bool operator !=(ObjectId left, ObjectId right) => !left.Equals(right);

		public static bool operator <(ObjectId left, ObjectId right) => left.CompareTo(right) < 0;

		public static bool operator >(ObjectId left, ObjectId right) => left.CompareTo(right) > 0;
	}
}
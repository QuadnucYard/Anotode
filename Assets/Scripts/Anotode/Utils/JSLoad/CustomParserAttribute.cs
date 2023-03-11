using System;

namespace Anotode.Utils.JSLoad {

	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	public class CustomParserAttribute : Attribute {
		public Type type;
		public CustomParserAttribute(Type type) {
			this.type = type;
		}
	}
}

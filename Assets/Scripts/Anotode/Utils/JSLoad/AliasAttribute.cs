using System;

namespace Anotode.Utils.JSLoad {

	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	public class AliasAttribute : Attribute {
		public readonly string alias;
		public AliasAttribute(string alias) { 
			this.alias = alias; 
		}
	}
}

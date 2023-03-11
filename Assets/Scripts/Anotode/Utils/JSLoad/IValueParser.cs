using Puerts;

namespace Anotode.Utils.JSLoad {
	public interface IValueParser<T> : IValueParser {
	}

	public interface IValueParser {
		public object Parse(JSObject value);
	}
}

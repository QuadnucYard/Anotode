using System.IO;

namespace Quadnuc.Utils {
	public class Pathy {

		private readonly string path;

		public Pathy(string path1) {
			path = path1;
		}
		public Pathy(string path1, string path2) {
			path = Path.Combine(path1, path2);
		}
		public Pathy(params string[] paths) {
			path = Path.Combine(paths);
		}

		public FileInfo File => new(path);
		public DirectoryInfo Directory => new(path);

		public static Pathy operator /(Pathy p, string s) => new(p.path, s);

		public override string ToString() => path;
	}
}

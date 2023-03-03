using System.Collections.Generic;
using System.IO;

namespace Quadnuc.Utils {
	public class Properties : Dictionary<string, string> {

		public void Load(TextReader reader) {
			for (string line; (line = reader.ReadLine()) != null;) {
				line = line.Trim();
				if (line.StartsWith('#'))
					continue;
				int i = line.IndexOf('=');
				if (i > 0) {
					Add(line[0..i].Trim(), line[(i + 1)..].Trim());
				}
			}
		}

		public void Store(TextWriter writer) {
			foreach (var (k,v) in this) {
				writer.WriteLine(k + "=" + v);
			}
		}

	}
}

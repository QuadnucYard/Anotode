using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anotode.Simul {
	public interface IProcessable {
		public void Process(int elapsed);
	}
}

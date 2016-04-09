using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsmScript
{
    public class Script {
		private List<Function> _functions = new List<Function>();

		public Script()	{

		}

		public void RegistryFunction(NetFunction function) {
			_functions.Add(function);
		}

		public bool Load(string filename) {
			return false;
		}

		
    }
}

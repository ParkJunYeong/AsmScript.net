using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
	class Program
	{
		static AsmScript.Object Print(List<AsmScript.Object> parms) {
			Console.WriteLine(parms[0].ToStr());

			return null;
		}

		static void Main(string[] args)
		{
			AsmScript.Script script = new AsmScript.Script();
			script.Load("test.asm");

			script.RegistryFunction("Print", Print);
			script.Execute();
		}
	}
}

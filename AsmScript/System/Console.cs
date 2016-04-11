using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Console
{
	public static AsmScript.Object Write(List<AsmScript.Object> parms) {
		System.Console.Write(parms[0].ToStr());
		return null;
	}

	public static AsmScript.Object WriteLine(List<AsmScript.Object> parms) {
		System.Console.WriteLine(parms[0].ToStr());
		return null;
	}
}
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

	public static AsmScript.Object Read(List<AsmScript.Object> parms) {
		return new AsmScript.IntegerObject() { Value = System.Console.Read() };
	}

	public static AsmScript.Object ReadLine(List<AsmScript.Object> parms) {
		return new AsmScript.StringObject() { Value = System.Console.ReadLine() };
	}

	public static AsmScript.Object Clear(List<AsmScript.Object> parms) {
		System.Console.Clear();

		return null;
	}
}
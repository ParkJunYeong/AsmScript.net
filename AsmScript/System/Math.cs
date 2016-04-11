using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Math {
	public  static AsmScript.Object Exp(List<AsmScript.Object> parms) {
		return new AsmScript.RealObject() { Value = System.Math.Exp(parms[0].ToReal()) };
	}
}
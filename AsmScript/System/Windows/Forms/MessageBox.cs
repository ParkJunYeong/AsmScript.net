using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MessageBox {
	public static AsmScript.Object Show(List<AsmScript.Object> parms) {
		return new AsmScript.IntegerObject() { Value = (int)System.Windows.Forms.MessageBox.Show(parms[0].ToStr(), parms[1].ToStr()) };
	}
}
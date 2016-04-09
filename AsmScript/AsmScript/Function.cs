using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsmScript
{
	public enum FunctionType {
		FUNC_NET,
		FUNC_ASM
	}

	public class Function {
		public string Name { get; set; }

		public virtual FunctionType GetFuncType() { return FunctionType.FUNC_ASM; }
	}

	public class NetFunction : Function {
		public delegate string FuncDel(params string[] parms);

		public FuncDel Impl;

		public override FunctionType GetFuncType() { return FunctionType.FUNC_NET; }
	}

	public class AsmFunction : Function {

	}
}

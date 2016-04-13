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
		public delegate Object FuncDel(List<Object> parms);

		public FuncDel Impl;

		public override FunctionType GetFuncType() { return FunctionType.FUNC_NET; }
	}

	public class AsmFunction : Function {
		public List<Token> Code = new List<Token>();
		public Dictionary<string, int> Labels = new Dictionary<string, int>();

		public Class Parent = null;
	}
}

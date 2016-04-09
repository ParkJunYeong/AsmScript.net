using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsmScript {
	public enum Commands {
		PUSH,
		CALL,

		RET,

		MOV,
		ADD,
		SUB,
		MUL,
		DIV,

		FUNC,
		END,

		VARINT,
		VARREAL,
		VARSTRING
	}

	public struct Token {
		public Commands cmd;

		public List<Object> parms;
	}

	class Parser {
		static public List<Token> Tokenization(string[] code) {
			List<Token> Ret = new List<Token>();

			foreach(string line in code) {
				if (line == string.Empty) continue;

				string[] lines = line.Split(' ');
				string arg = string.Empty;

				if (lines.Length > 1)
					for (int i = 1; i < lines.Length; i++)
						arg += lines[i] + " ";
				arg = arg.Trim();

				Token token = new Token();
				token.parms = new List<Object>();

				switch(lines[0].ToLower()) {
					case "push": token.cmd = Commands.PUSH; break;
					case "call": token.cmd = Commands.CALL; break;

					case "ret": token.cmd = Commands.RET; break;

					case "mov": token.cmd = Commands.MOV; break;
					case "add": token.cmd = Commands.ADD; break;
					case "sub": token.cmd = Commands.SUB; break;
					case "mul": token.cmd = Commands.MUL; break;
					case "div": token.cmd = Commands.DIV; break;

					case "func": token.cmd = Commands.FUNC; break;
					case "end": token.cmd = Commands.END; break;

					case "varint": token.cmd = Commands.VARINT; break;
					case "varreal": token.cmd = Commands.VARREAL; break;
					case "varstring": token.cmd = Commands.VARSTRING; break;
				}

				if(arg != string.Empty) {
					foreach (var it in arg.Split(',')) {
						int IntValue;
						double RealValue;

						if ((it.Trim()).First() == '\"' && (it.Trim()).Last() == '\"') {
							token.parms.Add(new StringObject() { Value = (it.Trim()).Trim('\"') });
						}
						else if (int.TryParse(it, out IntValue)) {
							token.parms.Add(new IntegerObject() { Value = IntValue });
						}
						else if (double.TryParse(it, out RealValue)) {
							token.parms.Add(new RealObject() { Value = RealValue });
						}
						else {
							token.parms.Add(new Object() { Name = it });
						}
					}
				}

				Ret.Add(token);
			}

			return Ret;
		}
	}
}

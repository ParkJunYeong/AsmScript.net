using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsmScript {
	public enum Commands {
		NONE,

		PUSH,
		CALL,

		JMP,

		RET,

		MOV,
		ADD,
		SUB,
		MUL,
		DIV,
		MOD,

		FUNC,
		END,

		VARINT,
		VARREAL,
		VARSTRING,
		VARCLASS,

		IMPORT,
		NATIVE,

		CMP,
		JE,
		JB,
		JA,
		JNE,
		JNB,
		JNA,
		JBE,
		JAE,
		JNBE,
		JNAE,

		CLASS,
	}

	public struct Token {
		public Commands cmd;
		public string value;

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

					case "jmp": token.cmd = Commands.JMP; break;

					case "ret": token.cmd = Commands.RET; break;

					case "mov": token.cmd = Commands.MOV; break;
					case "add": token.cmd = Commands.ADD; break;
					case "sub": token.cmd = Commands.SUB; break;
					case "mul": token.cmd = Commands.MUL; break;
					case "div": token.cmd = Commands.DIV; break;
					case "mod": token.cmd = Commands.MOD; break;

					case "func": token.cmd = Commands.FUNC; break;
					case "end": token.cmd = Commands.END; break;

					case "varint": token.cmd = Commands.VARINT; break;
					case "varreal": token.cmd = Commands.VARREAL; break;
					case "varstring": token.cmd = Commands.VARSTRING; break;
					case "varclass": token.cmd = Commands.VARCLASS; break;

					case "import": token.cmd = Commands.IMPORT; break;
					case "native": token.cmd = Commands.NATIVE; break;

					case "cmp": token.cmd = Commands.CMP; break;
					case "je": token.cmd = Commands.JE; break;
					case "jb": token.cmd = Commands.JB; break;
					case "ja": token.cmd = Commands.JA; break;
					case "jne": token.cmd = Commands.JNE; break;
					case "jnb": token.cmd = Commands.JNB; break;
					case "jna": token.cmd = Commands.JNA; break;
					case "jbe": token.cmd = Commands.JBE; break;
					case "jae": token.cmd = Commands.JAE; break;
					case "jnbe": token.cmd = Commands.JNBE; break;
					case "jnae": token.cmd = Commands.JNAE; break;

					case "class": token.cmd = Commands.CLASS; break;
				}
				token.value = lines[0];

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
							token.parms.Add(new Object() { Name = it.Trim() });
						}
					}
				}

				Ret.Add(token);
			}

			return Ret;
		}
	}
}

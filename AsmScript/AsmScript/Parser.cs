﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsmScript {
	public enum Commands {
		PUSH,
		CALL,

		MOV,
		ADD,
		SUB,
		MUL,
		DIV,
	}

	public struct Token {
		public Commands cmd;

		public List<Object> parms;
	}

	class Parser {
		static List<Token> Tokenization(string[] code) {
			List<Token> Ret = new List<Token>();

			foreach(string line in code) {
				string[] lines = line.Split(' ');
				string arg = string.Empty;

				if (lines.Length > 1)
					for (int i = 1; i < lines.Length; i++)
						arg += lines[i] + " ";
				arg.Trim();

				Token token = new Token();

				switch(lines[0]) {
					case "push": token.cmd = Commands.PUSH; break;
					case "call": token.cmd = Commands.CALL; break;

					case "mov": token.cmd = Commands.MOV; break;
					case "add": token.cmd = Commands.ADD; break;
					case "sub": token.cmd = Commands.SUB; break;
					case "mul": token.cmd = Commands.MUL; break;
					case "div": token.cmd = Commands.DIV; break;
				}

				foreach(var it in arg.Split(',')) {
					int IntValue;
					double RealValue;

					if (arg.First() == '\"' && arg.Last() == '\"') {
						token.parms.Add(new StringObject() { Value = arg });
					}
					else if (int.TryParse(it, out IntValue)) {
						token.parms.Add(new IntegerObject() { Value = IntValue });
					}
					else if(double.TryParse(it, out RealValue)) {
						token.parms.Add(new RealObject() { Value = RealValue });
					}
					else {
						token.parms.Add(new Object() { Name = arg });
					}
				}

				Ret.Add(token);
			}

			return Ret;
		}
	}
}

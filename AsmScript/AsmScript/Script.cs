﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsmScript
{
	public class Script {
		private List<Function> _functions = new List<Function>();

		public Script() {

		}

		public void RegistryFunction(string name, NetFunction.FuncDel function) {
			_functions.Add(new NetFunction() { Name = name, Impl = function });
		}

		public void Load(string filename) {
			try {
				List<Token> tokens = Parser.Tokenization(File.ReadAllLines(filename));

				int State = 0; // 0 : NONE, 1 : FUNC
				AsmFunction temp = new AsmFunction();

				foreach (Token token in tokens) {
					if (token.cmd == Commands.FUNC) {
						if (State == 0) {
							State = 1;

							temp.Name = token.parms[0].Name;
							continue;
						}
					}
					else if (token.cmd == Commands.END) {
						if (State == 1) {
							_functions.Add(temp);
							temp = new AsmFunction();

							State = 0;
							continue;
						}
					}

					if (State == 1) {
						temp.Code.Add(token);
					}
				}
			}
			catch (Exception e) {
				Console.Write(e.Message);
			}
		}

		public void Execute() {
			var func = _functions.Find((x) => { return x.Name == "Main"; });

			if(func != null) {
				if(func is AsmFunction) {
					RunCode(func);
				}
			}
		}

		private void RunCode(Function function, List<Object> parms = null) {
			if(function is AsmFunction) {
				List<Object> args = new List<Object>();

				foreach(var token in (function as AsmFunction).Code) {
					if(token.cmd == Commands.ADD) {
						token.parms[0].Add(token.parms[1]);
					}
					else if (token.cmd == Commands.SUB) {
						token.parms[0].Sub(token.parms[1]);
					}
					else if (token.cmd == Commands.MUL) {
						token.parms[0].Mul(token.parms[1]);
					}
					else if (token.cmd == Commands.DIV) {
						token.parms[0].Div(token.parms[1]);
					}
					else if (token.cmd == Commands.MOV) {
						token.parms[0] = token.parms[1];
					}
					else if(token.cmd == Commands.PUSH) {
						args.Add(token.parms[0]);
					}
					else if(token.cmd == Commands.CALL) {
						var func = _functions.Find((x) => { return x.Name == token.parms[0].Name; });

						if (func != null) {
							RunCode(func, args);
						}

						args.Clear();
					}
				}
			}
			else if(function is NetFunction) {
				(function as NetFunction).Impl(parms);
			}
		}
	}
}

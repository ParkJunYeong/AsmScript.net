using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AsmScript
{
	public class Script {
		private List<Function> _functions = new List<Function>();

		private Object _efr = null;

		public Script() {

		}

		public void RegistryFunction(string name, NetFunction.FuncDel function) {
			_functions.Add(new NetFunction() { Name = name, Impl = function });
		}

		private void _Load(string filename) {
			List<Token> tokens = Parser.Tokenization(File.ReadAllLines(filename, Encoding.UTF8));

			int State = 0; // 0 : NONE, 1 : FUNC
			AsmFunction temp = new AsmFunction();
			int func_start = 0;

			for (int m = 0; m < tokens.Count; m++) {
				var token = tokens[m];

				if (token.cmd == Commands.FUNC) {
					if (State == 0) {
						State = 1;
						func_start = m;

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
				else if(token.value.Last() == ':') {
					if(State == 1) {
						temp.Labels.Add(token.value.Substring(0, token.value.Length - 1), m - func_start - temp.Labels.Count);

						continue;
					}
				}
				else if(token.cmd == Commands.IMPORT) {
					if(State == 0) {
						if (token.parms[0].ToStr().LastIndexOf('.') != -1)
							_Load(token.parms[0].ToStr());
						else
							_Load(token.parms[0].ToStr() + ".asm");
					}

					continue;
				}

				if (State == 1) {
					temp.Code.Add(token);
				}
			}
		}

		public void Load(string filename) {
			try {
				_functions.Clear();
				_efr = null;

				_Load(filename);
			}
			catch (Exception e) {
				Console.Write(e.Message);
			}
		}

		public Object Execute() {
			var func = _functions.Find((x) => { return x.Name == "Main"; });

			if(func != null) {
				if(func is AsmFunction) {
					return RunCode(func);
				}
			}

			return null;
		}

		public Object RunCode(Function function, List<Object> parms = null) {
			if(function is AsmFunction) {
				List<Object> args = new List<Object>();
				List<Object> vars = new List<Object>();

				Object cmp1 = null, cmp2 = null;
				
				for (int m = 0; m < (function as AsmFunction).Code.Count; m++) {
					var token = ((AsmFunction)function).Code[m];

					if(token.cmd == Commands.VARINT) {
						vars.Add(new IntegerObject() { Name = token.parms[0].Name });

						if (token.parms.Count > 1)
							vars.Last().Mov(parms[(int)token.parms[1].ToInt() - 1]);
					}
					else if(token.cmd == Commands.VARREAL) {
						vars.Add(new RealObject() { Name = token.parms[0].Name });

						if (token.parms.Count > 1)
							vars.Last().Mov(parms[(int)token.parms[1].ToInt() - 1]);
					}
					else if(token.cmd == Commands.VARSTRING) {
						vars.Add(new StringObject() { Name = token.parms[0].Name });

						if (token.parms.Count > 1)
							vars.Last().Mov(parms[(int)token.parms[1].ToInt() - 1]);
					}
					else {
						for(int i = 0; i < token.parms.Count; i++) {
							var v = vars.Find((x) => { return x.Name == token.parms[i].Name; });

							if (v != null) {
								token.parms[i] = v;
							}
							else if (token.parms[i].Name == "efr")
								token.parms[i] = _efr;
						}
					}

					if (token.cmd == Commands.JMP) {
						int k = 0;

						if ((function as AsmFunction).Labels.TryGetValue(token.parms[0].ToStr(), out k)) {
							m = k - 1;

							token = ((AsmFunction)function).Code[m];
						}
					}
					else if (token.cmd == Commands.JE) {
						if (cmp1.JE(cmp2)) {
							int k = 0;

							if ((function as AsmFunction).Labels.TryGetValue(token.parms[0].ToStr(), out k)) {
								m = k - 1;

								token = ((AsmFunction)function).Code[m];
							}
						}
					}
					else if (token.cmd == Commands.JB) {
						if (cmp1.JB(cmp2)) {
							int k = 0;

							if ((function as AsmFunction).Labels.TryGetValue(token.parms[0].ToStr(), out k)) {
								m = k - 1;

								token = ((AsmFunction)function).Code[m];
							}
						}
					}
					else if (token.cmd == Commands.JA) {
						if (cmp1.JA(cmp2)) {
							int k = 0;

							if ((function as AsmFunction).Labels.TryGetValue(token.parms[0].ToStr(), out k)) {
								m = k - 1;

								token = ((AsmFunction)function).Code[m];
							}
						}
					}
					else if (token.cmd == Commands.JNE) {
						if (cmp1.JNE(cmp2)) {
							int k = 0;

							if ((function as AsmFunction).Labels.TryGetValue(token.parms[0].ToStr(), out k)) {
								m = k - 1;

								token = ((AsmFunction)function).Code[m];
							}
						}
					}
					else if (token.cmd == Commands.JNB) {
						if (cmp1.JNB(cmp2)) {
							int k = 0;

							if ((function as AsmFunction).Labels.TryGetValue(token.parms[0].ToStr(), out k)) {
								m = k - 1;

								token = ((AsmFunction)function).Code[m];
							}
						}
					}
					else if (token.cmd == Commands.JNA) {
						if (cmp1.JNA(cmp2)) {
							int k = 0;

							if ((function as AsmFunction).Labels.TryGetValue(token.parms[0].ToStr(), out k)) {
								m = k - 1;

								token = ((AsmFunction)function).Code[m];
							}
						}
					}
					else if (token.cmd == Commands.JBE) {
						if (cmp1.JBE(cmp2)) {
							int k = 0;

							if ((function as AsmFunction).Labels.TryGetValue(token.parms[0].ToStr(), out k)) {
								m = k - 1;

								token = ((AsmFunction)function).Code[m];
							}
						}
					}
					else if (token.cmd == Commands.JAE) {
						if (cmp1.JAE(cmp2)) {
							int k = 0;

							if ((function as AsmFunction).Labels.TryGetValue(token.parms[0].ToStr(), out k)) {
								m = k - 1;

								token = ((AsmFunction)function).Code[m];
							}
						}
					}
					else if (token.cmd == Commands.JNBE) {
						if (cmp1.JNBE(cmp2)) {
							int k = 0;

							if ((function as AsmFunction).Labels.TryGetValue(token.parms[0].ToStr(), out k)) {
								m = k - 1;

								token = ((AsmFunction)function).Code[m];
							}
						}
					}
					else if (token.cmd == Commands.JNAE) {
						if (cmp1.JNAE(cmp2)) {
							int k = 0;

							if ((function as AsmFunction).Labels.TryGetValue(token.parms[0].ToStr(), out k)) {
								m = k - 1;

								token = ((AsmFunction)function).Code[m];
							}
						}
					}

					if (token.cmd == Commands.ADD) {
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
						token.parms[0].Mov(token.parms[1]);
					}
					else if(token.cmd == Commands.MOD) {
						token.parms[0].Mod(token.parms[1]);
					}
					else if(token.cmd == Commands.PUSH) {
						args.Add(token.parms[0]);
					}
					else if(token.cmd == Commands.CMP) {
						cmp1 = token.parms[0];
						cmp2 = token.parms[1];
					}
					else if(token.cmd == Commands.CALL) {
						var func = _functions.Find((x) => { return x.Name == token.parms[0].Name; });

						if (func != null) {
							_efr = RunCode(func, args);
						}

						args.Clear();
					}
					else if(token.cmd == Commands.NATIVE) {
						Assembly a = Assembly.LoadFile(System.Environment.CurrentDirectory + "\\" + token.parms[0].ToStr());

						foreach (var t in a.GetTypes()) {
							MethodInfo method = t.GetMethod(token.parms[1].ToStr());

							if (method == null) continue;

							var instance = Activator.CreateInstance(t);


							return (Object)method.Invoke(instance, new object[] { args });
						}
					}
					else if(token.cmd == Commands.RET) {
						if (token.parms.Count == 0) {
							return null;
						}
						else {
							return token.parms[0];
						}
					}
				}
			}
			else if(function is NetFunction) {
				return (function as NetFunction).Impl(parms);
			}

			return null;
		}
	}
}

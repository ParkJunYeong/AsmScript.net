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
		private List<Class> _classes = new List<Class>();

		private Object _efr = null;

		public Script() {

		}

		public void RegistryFunction(string name, NetFunction.FuncDel function) {
			_functions.Add(new NetFunction() { Name = name, Impl = function });
		}

		private void _Load(string filename) {
			List<Token> tokens = Parser.Tokenization(File.ReadAllLines(filename, Encoding.UTF8));

			int State = 0; // 0 : NONE, 1 : FUNC, 2 : CLASS, 3 : CLASS FUNC
			AsmFunction temp = new AsmFunction();
			Class cls = new Class() { _impl = this };
			int func_start = 0;

			for (int m = 0; m < tokens.Count; m++) {
				var token = tokens[m];

				if (token.cmd == Commands.FUNC) {
					if (State == 0 || State == 2) {
						State = (State == 0) ? 1 : 3;
						func_start = m;

						temp.Name = token.parms[0].Name;
						continue;
					}
				}
				else if(token.cmd == Commands.CLASS) {
					if(State == 0) {
						State = 2;

						cls.Type = token.parms[0].Name;
						continue;
					}
				}
				else if (token.cmd == Commands.END) {
					if (State == 1) {
						_functions.Add(temp);
						temp = new AsmFunction();

						State = (State == 1) ? 0 : 2;
						continue;
					}
					else if(State == 3) {
						temp.Parent = cls;
						cls.functions.Add(temp);
						temp = new AsmFunction();

						State = (State == 1) ? 0 : 2;
						continue;
					}
					else if(State == 2) {
						_classes.Add(cls);
						cls = new Class() { _impl = this };

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
						if (token.parms[0].ToStr().LastIndexOf('.') != -1 && System.IO.Path.GetExtension(token.parms[0].ToStr()) == ".asm")
							_Load(token.parms[0].ToStr());
						else
							_Load(token.parms[0].ToStr() + ".asm");
					}

					continue;
				}


				if (State == 1 || State == 3) {
					temp.Code.Add(token);
				}
				else if(State == 2) {
					if (token.cmd == Commands.VARINT) {
						cls.fields.Add(new IntegerObject() { Name = token.parms[0].Name });
					}
					else if (token.cmd == Commands.VARREAL) {
						cls.fields.Add(new RealObject() { Name = token.parms[0].Name });
					}
					else if (token.cmd == Commands.VARSTRING) {
						cls.fields.Add(new StringObject() { Name = token.parms[0].Name });
					}
					else if (token.cmd == Commands.VARCLASS) {
						var c = (Class)_classes.Find((x) => { return x.Type == token.parms[0].Name; }).Clone();

						c.Name = token.parms[1].Name;

						cls.fields.Add(c);
					}
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

				if ((function as AsmFunction).Parent != null)
					vars.AddRange((function as AsmFunction).Parent.fields);

				Object cmp1 = null, cmp2 = null;

				List<Token> code = new List<Token>();
				foreach (Token t in (function as AsmFunction).Code)
					code.Add((Token)t.Clone());
				
				for (int m = 0; m < code.Count; m++) {
					var token = code[m];

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
					else if(token.cmd == Commands.VARCLASS) {
						var c = (Class)_classes.Find((x) => { return x.Type == token.parms[0].Name; }).Clone();

						c.Name = token.parms[1].Name;

						vars.Add(c);
					}
					else {
						for(int i = 0; i < token.parms.Count; i++) {
							if ((token.parms[i] is IntegerObject) || (token.parms[i] is StringObject)) continue;

							string[] ts = token.parms[i].Name.Split(':');

							var v = (ts.Length == 1) ? vars.Find((x) => { return x.Name == ts[0]; }) : (vars.Find((x) => { return x.Name == ts[0]; }) as Class).fields.Find((x) => { return x.Name == ts[1]; });

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
							m = k - 2;

							token = code[m];

							continue;
						}
					}
					else if (token.cmd == Commands.JE) {
						if (cmp1.JE(cmp2)) {
							int k = 0;

							if ((function as AsmFunction).Labels.TryGetValue(token.parms[0].ToStr(), out k)) {
								m = k - 2;

								token = code[m];

								continue;
							}
						}
					}
					else if (token.cmd == Commands.JB) {
						if (cmp1.JB(cmp2)) {
							int k = 0;

							if ((function as AsmFunction).Labels.TryGetValue(token.parms[0].ToStr(), out k)) {
								m = k - 2;

								token = code[m];

								continue;
							}
						}
					}
					else if (token.cmd == Commands.JA) {
						if (cmp1.JA(cmp2)) {
							int k = 0;

							if ((function as AsmFunction).Labels.TryGetValue(token.parms[0].ToStr(), out k)) {
								m = k - 2;

								token = code[m];

								continue;
							}
						}
					}
					else if (token.cmd == Commands.JNE) {
						if (cmp1.JNE(cmp2)) {
							int k = 0;

							if ((function as AsmFunction).Labels.TryGetValue(token.parms[0].ToStr(), out k)) {
								m = k - 2;

								token = code[m];

								continue;
							}
						}
					}
					else if (token.cmd == Commands.JNB) {
						if (cmp1.JNB(cmp2)) {
							int k = 0;

							if ((function as AsmFunction).Labels.TryGetValue(token.parms[0].ToStr(), out k)) {
								m = k - 2;

								token = code[m];

								continue;
							}
						}
					}
					else if (token.cmd == Commands.JNA) {
						if (cmp1.JNA(cmp2)) {
							int k = 0;

							if ((function as AsmFunction).Labels.TryGetValue(token.parms[0].ToStr(), out k)) {
								m = k - 2;

								token = code[m];

								continue;
							}
						}
					}
					else if (token.cmd == Commands.JBE) {
						if (cmp1.JBE(cmp2)) {
							int k = 0;

							if ((function as AsmFunction).Labels.TryGetValue(token.parms[0].ToStr(), out k)) {
								m = k - 2;

								token = code[m];

								continue;
							}
						}
					}
					else if (token.cmd == Commands.JAE) {
						if (cmp1.JAE(cmp2)) {
							int k = 0;

							if ((function as AsmFunction).Labels.TryGetValue(token.parms[0].ToStr(), out k)) {
								m = k - 2;

								token = code[m];

								continue;
							}
						}
					}
					else if (token.cmd == Commands.JNBE) {
						if (cmp1.JNBE(cmp2)) {
							int k = 0;

							if ((function as AsmFunction).Labels.TryGetValue(token.parms[0].ToStr(), out k)) {
								m = k - 2;

								token = code[m];

								continue;
							}
						}
					}
					else if (token.cmd == Commands.JNAE) {
						if (cmp1.JNAE(cmp2)) {
							int k = 0;

							if ((function as AsmFunction).Labels.TryGetValue(token.parms[0].ToStr(), out k)) {
								m = k - 2;

								token = code[m];

								continue;
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
						string[] call_token = token.parms[0].ToStr().Split(':');

						if(call_token.Length == 2) {
							var c = (Class)vars.Find((x) => { return x is Class && x.Name == call_token[0]; });
							var f = c.functions.Find((x) => { return x.Name == call_token[1]; });

							if (f != null)
								_efr = RunCode(f, args);

							args.Clear();
						}
						else if(call_token.Length == 1) {
							var func = _functions.Find((x) => { return x.Name == call_token[0]; });

							if (func != null) {
								_efr = RunCode(func, args);
							}

							args.Clear();
						}
					}
					else if(token.cmd == Commands.NATIVE) {
						Assembly a = Assembly.LoadFile(System.Environment.CurrentDirectory + "\\" + token.parms[0].ToStr());

						string[] cls = token.parms[1].ToStr().Split('.');

						var t = a.GetType(cls[cls.Length - 2]);

						MethodInfo method = t.GetMethod(cls[cls.Length - 1]);

						if (method == null) continue;

						var instance = Activator.CreateInstance(t);


						return (Object)method.Invoke(instance, new object[] { args });
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

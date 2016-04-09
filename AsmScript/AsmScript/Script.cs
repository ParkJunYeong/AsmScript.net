using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsmScript
{
    public class Script {
		private List<Function> _functions = new List<Function>();

		public Script()	{

		}

		public void RegistryFunction(NetFunction function) {
			_functions.Add(function);
		}

		public void Load(string filename) {
			try {
				List<Token> tokens = Parser.Tokenization(File.ReadAllLines(filename));

				int State = 0; // 0 : NONE, 1 : FUNC
				AsmFunction temp = new AsmFunction();

				foreach(Token token in tokens) {
					if(token.cmd == Commands.FUNC) {
						if(State == 0) {
							State = 1;
							
							temp.Name = token.parms[0].Name;
							continue;
						}
					}
					else if(token.cmd == Commands.END) {
						if(State == 1) {
							_functions.Add(temp);
							temp = new AsmFunction();

							State = 0;
							continue;
						}
					}

					if(State == 1) {
						temp.Code.Add(token);
					}
				}
			}
			catch (Exception e) {
				Console.Write(e.Message);
			}
		}		
    }
}

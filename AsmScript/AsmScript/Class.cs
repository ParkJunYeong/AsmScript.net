using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsmScript {
	public class Class : Object, ICloneable {
		internal Script _impl;
		
		public string Type {
			get; set;
		}

		public List<Object> fields {
			get;
			private set;
		} = new List<Object>();

		public List<Function> functions {
			get;
			private set;
		} = new List<Function>();

		public object Clone() {
			return this.MemberwiseClone();
		}

		public override void Mov(Object Other) {
			var f = functions.Find((x) => { return x.Name == "__MOV__"; });

			if (f != null)
				_impl.RunCode(f, new List<Object>() { Other });
		}
		public override void Add(Object Other) {
			var f = functions.Find((x) => { return x.Name == "__ADD__"; });

			if (f != null)
				_impl.RunCode(f, new List<Object>() { Other });
		}
		public override void Sub(Object Other) {
			var f = functions.Find((x) => { return x.Name == "__SUB__"; });

			if (f != null)
				_impl.RunCode(f, new List<Object>() { Other });
		}
		public override void Mul(Object Other) {
			var f = functions.Find((x) => { return x.Name == "__MUL__"; });

			if (f != null)
				_impl.RunCode(f, new List<Object>() { Other });
		}
		public override void Div(Object Other) {
			var f = functions.Find((x) => { return x.Name == "__DIV__"; });

			if (f != null)
				_impl.RunCode(f, new List<Object>() { Other });
		}
		public override void Mod(Object Other) {
			var f = functions.Find((x) => { return x.Name == "__MOD__"; });

			if (f != null)
				_impl.RunCode(f, new List<Object>() { Other });
		}

		public override bool JE(Object Other) {
			var f = functions.Find((x) => { return x.Name == "__JE__"; });

			if (f != null)
				return _impl.RunCode(f, new List<Object>() { Other }).ToInt() == 0;

			return false;
		}
		public override bool JB(Object Other) {
			var f = functions.Find((x) => { return x.Name == "__JB__"; });

			if (f != null)
				return _impl.RunCode(f, new List<Object>() { Other }).ToInt() == 0;

			return false;
		}
		public override bool JA(Object Other) {
			var f = functions.Find((x) => { return x.Name == "__JA__"; });

			if (f != null)
				return _impl.RunCode(f, new List<Object>() { Other }).ToInt() == 0;

			return false;
		}
		public override bool JNE(Object Other) {
			var f = functions.Find((x) => { return x.Name == "__JNE__"; });

			if (f != null)
				return _impl.RunCode(f, new List<Object>() { Other }).ToInt() == 0;

			return false;
		}
		public override bool JNB(Object Other) {
			var f = functions.Find((x) => { return x.Name == "__JNB__"; });

			if (f != null)
				return _impl.RunCode(f, new List<Object>() { Other }).ToInt() == 0;

			return false;
		}
		public override bool JNA(Object Other) {
			var f = functions.Find((x) => { return x.Name == "__JNA__"; });

			if (f != null)
				return _impl.RunCode(f, new List<Object>() { Other }).ToInt() == 0;

			return false;
		}
		public override bool JBE(Object Other) {
			var f = functions.Find((x) => { return x.Name == "__JBE__"; });

			if (f != null)
				return _impl.RunCode(f, new List<Object>() { Other }).ToInt() == 0;

			return false;
		}
		public override bool JAE(Object Other) {
			var f = functions.Find((x) => { return x.Name == "__JAE__"; });

			if (f != null)
				return _impl.RunCode(f, new List<Object>() { Other }).ToInt() == 0;

			return false;
		}
		public override bool JNBE(Object Other) {
			var f = functions.Find((x) => { return x.Name == "__JNBE__"; });

			if (f != null)
				return _impl.RunCode(f, new List<Object>() { Other }).ToInt() == 0;

			return false;
		}
		public override bool JNAE(Object Other) {
			var f = functions.Find((x) => { return x.Name == "__JNAE__"; });

			if (f != null)
				return _impl.RunCode(f, new List<Object>() { Other }).ToInt() == 0;

			return false;
		}
	}
}

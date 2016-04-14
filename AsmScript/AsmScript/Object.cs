using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsmScript {
	public class Object {
		public string Name { get; set; }
		
		public virtual void Mov(Object Other) {
			throw new NotImplementedException();
		}
		public virtual void Add(Object Other) {
			throw new NotImplementedException();
		}
		public virtual void Sub(Object Other) {
			throw new NotImplementedException();
		}
		public virtual void Mul(Object Other) {
			throw new NotImplementedException();
		}
		public virtual void Div(Object Other) {
			throw new NotImplementedException();
		}
		public virtual void Mod(Object Other) {
			throw new NotImplementedException();
		}

		public virtual bool JE(Object Other) {
			throw new NotImplementedException();
		}
		public virtual bool JB(Object Other) {
			throw new NotImplementedException();
		}
		public virtual bool JA(Object Other) {
			throw new NotImplementedException();
		}
		public virtual bool JNE(Object Other) {
			throw new NotImplementedException();
		}
		public virtual bool JNB(Object Other) {
			throw new NotImplementedException();
		}
		public virtual bool JNA(Object Other) {
			throw new NotImplementedException();
		}
		public virtual bool JBE(Object Other) {
			throw new NotImplementedException();
		}
		public virtual bool JAE(Object Other) {
			throw new NotImplementedException();
		}
		public virtual bool JNBE(Object Other) {
			throw new NotImplementedException();
		}
		public virtual bool JNAE(Object Other) {
			throw new NotImplementedException();
		}

		public virtual string ToStr() {
			return Name;
		}

		public virtual long ToInt() {
			throw new NotImplementedException();
		}

		public virtual double ToReal() {
			throw new NotImplementedException();
		}
	}
	
	public class IntegerObject : Object{
		public long Value;

		public override void Mov(Object Other) {
				Value = Other.ToInt();
		}
		public override void Add(Object Other) {
				Value += Other.ToInt();
		}
		public override void Sub(Object Other) {
				Value -= Other.ToInt();
		}
		public override void Mul(Object Other) {
				Value *= Other.ToInt();
		}
		public override void Div(Object Other) {
				Value /= Other.ToInt();
		}
		public override void Mod(Object Other) {
				Value %= Other.ToInt();
		}

		public override bool JE(Object Other) {
			return (Value == Other.ToInt());
		}
		public override bool JB(Object Other) {
			return (Value < Other.ToInt());
		}
		public override bool JA(Object Other) {
			return (Value > Other.ToInt());
		}
		public override bool JNE(Object Other) {
			return (Value != Other.ToInt());
		}
		public override bool JNB(Object Other) {
			return !(Value < Other.ToInt());
		}
		public override bool JNA(Object Other) {
			return !(Value > Other.ToInt());
		}
		public override bool JBE(Object Other) {
			return (Value <= Other.ToInt());
		}
		public override bool JAE(Object Other) {
			return (Value >= Other.ToInt());
		}
		public override bool JNBE(Object Other) {
			return !(Value <= Other.ToInt());
		}
		public override bool JNAE(Object Other) {
			return !(Value >= Other.ToInt());
		}

		public override string ToStr() {
			return Value.ToString();
		}

		public override long ToInt() {
			return Value;
		}

		public override double ToReal() {
			return Value;
		}
	}

	public class RealObject : Object {
		public double Value;

		public override void Mov(Object Other) {
				Value = Other.ToReal();
		}
		public override void Add(Object Other) {
				Value += Other.ToReal();
		}
		public override void Sub(Object Other) {
				Value -= Other.ToReal();
		}
		public override void Mul(Object Other) {
				Value *= Other.ToReal();
		}
		public override void Div(Object Other) {
				Value /= Other.ToReal();
		}
		public override void Mod(Object Other) {
				Value %= Other.ToReal();
		}

		public override bool JE(Object Other) {
			return (Value == Other.ToReal());
		}
		public override bool JB(Object Other) {
			return (Value < Other.ToReal());
		}
		public override bool JA(Object Other) {
			return (Value > Other.ToReal());
		}
		public override bool JNE(Object Other) {
			return (Value != Other.ToReal());
		}
		public override bool JNB(Object Other) {
			return !(Value < Other.ToReal());
		}
		public override bool JNA(Object Other) {
			return !(Value > Other.ToReal());
		}
		public override bool JBE(Object Other) {
			return (Value <= Other.ToReal());
		}
		public override bool JAE(Object Other) {
			return (Value >= Other.ToReal());
		}
		public override bool JNBE(Object Other) {
			return !(Value <= Other.ToReal());
		}
		public override bool JNAE(Object Other) {
			return !(Value >= Other.ToReal());
		}

		public override string ToStr() {
			return Value.ToString();
		}

		public override long ToInt() {
			return (long)Value;
		}

		public override double ToReal() {
			return Value;
		}
	}

	public class StringObject : Object {
		public string Value;

		public override void Mov(Object Other) {
			Value = Other.ToStr();
		}
		public override void Add(Object Other) {
			Value += Other.ToStr();
		}

		public override bool JE(Object Other) {
			return (Value == Other.ToStr());
		}

		public override bool JNE(Object Other) {
			return (Value != Other.ToStr());
		}

		public override string ToStr() {
			return Value;
		}

		public override long ToInt() {
			return Convert.ToInt64(Value);
		}

		public override double ToReal() {
			return Convert.ToDouble(Value);
		}
	}

	public class NetObject : Object
	{
		public System.Object Value;

		public override void Mov(Object Other)
		{
			Value = (Other as NetObject).Value;
		}

		public override bool JE(Object Other)
		{
			return (Value == (Other as NetObject).Value);
		}

		public override bool JNE(Object Other)
		{
			return (Value != (Other as NetObject).Value);
		}

		public override string ToStr()
		{
			return Value.ToString();
		}

		public override long ToInt()
		{
			return Convert.ToInt64(Value);
		}

		public override double ToReal()
		{
			return Convert.ToDouble(Value);
		}
	}
}

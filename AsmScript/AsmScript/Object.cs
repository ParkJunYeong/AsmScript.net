﻿using System;
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
			if (Other is IntegerObject || Other is RealObject)
				Value = Other.ToInt();
		}
		public override void Add(Object Other) {
			if (Other is IntegerObject || Other is RealObject)
				Value += Other.ToInt();
		}
		public override void Sub(Object Other) {
			if (Other is IntegerObject || Other is RealObject)
				Value -= Other.ToInt();
		}
		public override void Mul(Object Other) {
			if (Other is IntegerObject || Other is RealObject)
				Value *= Other.ToInt();
		}
		public override void Div(Object Other) {
			if (Other is IntegerObject || Other is RealObject)
				Value /= Other.ToInt();
		}
		public override void Mod(Object Other) {
			if (Other is IntegerObject || Other is RealObject)
				Value %= Other.ToInt();
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
			if (Other is IntegerObject || Other is RealObject)
				Value = Other.ToInt();
		}
		public override void Add(Object Other) {
			if (Other is IntegerObject || Other is RealObject)
				Value += Other.ToInt();
		}
		public override void Sub(Object Other) {
			if (Other is IntegerObject || Other is RealObject)
				Value -= Other.ToInt();
		}
		public override void Mul(Object Other) {
			if (Other is IntegerObject || Other is RealObject)
				Value *= Other.ToInt();
		}
		public override void Div(Object Other) {
			if (Other is IntegerObject || Other is RealObject)
				Value /= Other.ToInt();
		}
		public override void Mod(Object Other) {
			if (Other is IntegerObject || Other is RealObject)
				Value %= Other.ToInt();
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
}

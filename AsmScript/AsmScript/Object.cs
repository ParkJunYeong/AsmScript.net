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
	}
	
	public class IntegerObject : Object{
		public long Value;

		public override void Mov(Object Other) {
			if (Other is IntegerObject)
				Value = (Other as IntegerObject).Value;
			else if (Other is RealObject)
				Value = (long)(Other as RealObject).Value;
		}
		public override void Add(Object Other) {
			if (Other is IntegerObject)
				Value += (Other as IntegerObject).Value;
			else if (Other is RealObject)
				Value += (long)(Other as RealObject).Value;
		}
		public override void Sub(Object Other) {
			if (Other is IntegerObject)
				Value -= (Other as IntegerObject).Value;
			else if (Other is RealObject)
				Value -= (long)(Other as RealObject).Value;
		}
		public override void Mul(Object Other) {
			if (Other is IntegerObject)
				Value *= (Other as IntegerObject).Value;
			else if (Other is RealObject)
				Value *= (long)(Other as RealObject).Value;
		}
		public override void Div(Object Other) {
			if (Other is IntegerObject)
				Value /= (Other as IntegerObject).Value;
			else if (Other is RealObject)
				Value /= (long)(Other as RealObject).Value;
		}
	}

	public class RealObject : Object {
		public double Value;

		public override void Mov(Object Other) {
			if (Other is IntegerObject)
				Value = (Other as IntegerObject).Value;
			else if (Other is RealObject)
				Value = (Other as RealObject).Value;
		}
		public override void Add(Object Other) {
			if (Other is IntegerObject)
				Value += (Other as IntegerObject).Value;
			else if (Other is RealObject)
				Value += (Other as RealObject).Value;
		}
		public override void Sub(Object Other) {
			if (Other is IntegerObject)
				Value -= (Other as IntegerObject).Value;
			else if (Other is RealObject)
				Value -= (Other as RealObject).Value;
		}
		public override void Mul(Object Other) {
			if (Other is IntegerObject)
				Value *= (Other as IntegerObject).Value;
			else if (Other is RealObject)
				Value *= (Other as RealObject).Value;
		}
		public override void Div(Object Other) {
			if (Other is IntegerObject)
				Value /= (Other as IntegerObject).Value;
			else if (Other is RealObject)
				Value /= (Other as RealObject).Value;
		}
	}

	public class StringObject : Object {
		public string Value;

		public override void Mov(Object Other) {
			if (Other is IntegerObject)
				Value = (Other as IntegerObject).Value.ToString();
			else if (Other is RealObject)
				Value = (Other as RealObject).Value.ToString();
			else if(Other is StringObject)
				Value = (Other as StringObject).Value;
		}
		public override void Add(Object Other) {
			if (Other is IntegerObject)
				Value += (Other as IntegerObject).Value.ToString();
			else if (Other is RealObject)
				Value += (Other as RealObject).Value.ToString();
			else if (Other is StringObject)
				Value += (Other as StringObject).Value;
		}
	}
}

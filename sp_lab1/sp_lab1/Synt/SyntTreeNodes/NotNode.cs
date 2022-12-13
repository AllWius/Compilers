using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sp_lab1.Synt.SyntTreeNodes
{
	public class NotNode : LogicalNode
	{
		public NotNode() { }
		public NotNode(string token, ExpressionNode expression1)
			: base(token, expression1, expression1)
		{
		}

        public override string ToAlgLang()
        {
            return $" != ";
        }

        public override string ToString()
		{
			return $"NotNode: {operation}";
		}
	}
}

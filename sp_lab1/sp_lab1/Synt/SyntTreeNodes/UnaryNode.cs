using sp_lab1.Synt.SyntTreeNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace lab2.Models.SyntaxNodes
{
	public class UnaryNode : ExpressionNode
	{
		public ExpressionNode Expression;

		public UnaryNode() { }
		public UnaryNode(string op, ExpressionNode expression) : base(op)
		{
			Expression = expression;
		}

        public override string ToAlgLang()
        {
            return $"{operation} {Expression}";
        }

        public override string ToString()
		{
			return $"UnaryNode: {operation}";
		}
	}
}

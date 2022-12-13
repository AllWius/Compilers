using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sp_lab1.Synt.SyntTreeNodes
{
    public class ArithmeticalNode : ExpressionNode
    {
        public ExpressionNode expression_1, expression_2;

        public ArithmeticalNode() { }
        public ArithmeticalNode(string op, ExpressionNode expression_1, ExpressionNode expression_2) : base(op)
        {
            this.expression_1 = expression_1;
            this.expression_2 = expression_2;

            if (Match(expression_1, expression_2) == "") throw new Exception("Type error");
        }

        public override string Type()
        {
            if (expression_1.Type() == expression_2.Type()) return expression_1.Type();
            return "";
        }

        public override string Match(ExpressionNode e1, ExpressionNode e2)
        {
            if (e1.Type() == e2.Type()) return e1.Type();
            return "";
        }

        public override string ToAlgLang()
        {
            return $"{expression_1.ToAlgLang()} {operation} {expression_2.ToAlgLang()}";
        }

        public override string ToString()
        {
            return $"ArithmeticalNode: {operation} ";
        }
    }
}

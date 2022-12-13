using lab2.Models.SyntaxNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sp_lab1.Synt.SyntTreeNodes
{
    public class ExpressionNode : StatementNode
    {
        public string operation;

        public ExpressionNode() { }

        public ExpressionNode(string operation)
        {
            this.operation = operation;
        }

        public override string Type()
        {
            return "";
        }

        public virtual string Match(ExpressionNode e1, ExpressionNode e2)
        {
            if (e1.Type() == e2.Type()) return e1.Type();
            return "";
        }


        public override string ToString()
        {
            return $"ExpressionNode: {operation}";
        }
    }
}

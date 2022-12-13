using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sp_lab1.Synt.SyntTreeNodes
{
    public class LogicalNode : ExpressionNode
	{
		public ExpressionNode Expression1, Expression2;

		public LogicalNode() { }
		public LogicalNode(string token, ExpressionNode expression1, ExpressionNode expression2) : base(token)
		{
			Expression1 = expression1;
			Expression2 = expression2;
		}

        public override string Type()
        {
            return "bool";
        }



        public override string ToString()
		{
			return $"LogicalNode: {operation}";
		}
	}
}

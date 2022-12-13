using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sp_lab1.Synt.SyntTreeNodes
{
    public class StapleNode : ExpressionNode
    {
        public ExpressionNode node;

        public StapleNode(ExpressionNode exp)
        {
            node = exp;
        }

        public override string ToAlgLang()
        {
            return $"({node.ToAlgLang()})";
        }
    }
}

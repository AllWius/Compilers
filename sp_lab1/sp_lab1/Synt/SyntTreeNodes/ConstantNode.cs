using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sp_lab1.Synt.SyntTreeNodes
{
    public class ConstantNode : ExpressionNode
    {
        public string type;
        public ConstantNode() { }
        public ConstantNode(string token, string dataType) : base(token)
        {
            type = dataType;
        }

        public override string Type()
        {
            return type;
        }

        public override string ToString()
        {
            return $"ConstantNode: {operation}, {type}";
        }

        public override string ToAlgLang()
        {
            return operation;
        }
    }
}

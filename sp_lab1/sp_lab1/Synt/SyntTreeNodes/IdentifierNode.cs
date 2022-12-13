using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sp_lab1.Synt.SyntTreeNodes
{
    public class IdentifierNode : ExpressionNode
    {
        public string type;

        public IdentifierNode() { }
        public IdentifierNode(string op, string type) : base(op) 
        {
            this.type = type;
        }

        public override string Type()
        {
            return type;
        }

        public override string ToAlgLang()
        {
            return operation;
        }

        public override string ToString()
        {
            return $"IdentifierNode: {operation}, {type}";
        }
    }
}

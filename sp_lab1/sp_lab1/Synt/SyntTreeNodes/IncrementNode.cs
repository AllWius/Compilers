using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sp_lab1.Synt.SyntTreeNodes
{
    public class IncrementNode : ExpressionNode
    {
        public string Identifier;
        public IncrementNode() { }
        public IncrementNode(string op, string id) : base(op)
        {
            Identifier = id;
        }

        public override string ToAlgLang()
        {
            return $"{Identifier} := {Identifier} + 1";
        }

        public override string ToString()
        {
            return $"IncrementNode: {operation}, {Identifier}";
        }
    }
}

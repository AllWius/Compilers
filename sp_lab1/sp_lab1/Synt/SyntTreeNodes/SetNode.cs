using sp_lab1.Synt.SyntTreeNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.Models.SyntaxNodes
{
    public class SetNode : StatementNode
    {
        public string Name;
        public ExpressionNode Expression;

        public SetNode() { }
        public SetNode(string name, ExpressionNode expression)
        {
            Name = name;
            Expression = expression;
        }

        public override string ToAlgLang()
        {
            return $"{Name} := {Expression}";
        }
        public override string ToString()
        {
            return $"SetNode: {Name}";
        }
    }
}

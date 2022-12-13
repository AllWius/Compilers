using lab2.Models.SyntaxNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sp_lab1.Synt.SyntTreeNodes
{
    public class ForNode : StatementNode
    {
        public StatementNode Id;
        public ExpressionNode condition;
        public StatementNode expression;
        public StatementNode body;

        public ForNode () { }
        public ForNode (StatementNode id, ExpressionNode condition, SequenceNode expression, StatementNode body)
        {
            Id = id;
            this.condition = condition;
            this.expression = expression;
            this.body = body;
        }

        public override string ToString()
        {
            return $"ForNode:";
        }

        public override string ToAlgLang()
        {
            return $"нц для {Id.ToAlgLang()} пока ({condition.ToAlgLang()}) {expression.ToAlgLang()} \n\t{body.ToAlgLang()}\nкц";
        }
    }
}

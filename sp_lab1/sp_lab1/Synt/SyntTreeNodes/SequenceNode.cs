using lab2.Models.SyntaxNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sp_lab1.Synt.SyntTreeNodes
{
    public class SequenceNode : StatementNode
    {
        public StatementNode Statement { get; set; }
        public StatementNode? Next { get; set; }

        public SequenceNode() { }
        public SequenceNode(StatementNode statement1, StatementNode? statement2 = null)
        {
            Statement = statement1;
            Next = statement2;
        }

        public override string ToAlgLang()
        {
            ForNode For = Next as ForNode;
            SetNode Set = Next as SetNode;
            ArithmeticalNode Arith = Next as ArithmeticalNode;

            //if (For != null)
            //{
            //    return $"{Statement.ToAlgLang}\n{Next.ToAlgLang()}";
            //    TreePrint(For.Id, tub + "\t");
            //    TreePrint(For.condition, tub + "\t");
            //    TreePrint(For.expression, tub + "\t");
            //    TreePrint(For.body, tub + "\t");
            //}
            //else if (Set != null)
            //{
            //    TreePrint(Set.Expression, tub + "│" + "\t");
            //}
            //else if (Arith != null)
            //{
            //    TreePrint(Arith.expression_1, tub + "│" + "\t");
            //    TreePrint(Arith.expression_2, tub + "│" + "\t");
            //}
            return $"{Statement.ToAlgLang}\n{Next.ToAlgLang()}";
        }

        public override string ToString()
        {
            return $"SequenceNode:";
        }
    }
}

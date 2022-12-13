using lab2.Models.SyntaxNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sp_lab1.Synt.SyntTreeNodes
{
    public class BlockNode : StatementNode
    {
        public SequenceNode node;

        public BlockNode() { }
        public BlockNode(SequenceNode node)
        {
            this.node = node;
        }

        public override string ToAlgLang()
        {
            return "нач\n\t" + node.ToAlgLang() + "\nкон";
        }
    }
}

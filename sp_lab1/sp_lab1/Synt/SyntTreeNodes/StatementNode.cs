using sp_lab1.Synt.SyntTreeNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace lab2.Models.SyntaxNodes
{
    public class StatementNode : BaseNode
    {
        public StatementNode()
        {
         
        }

        public virtual string Type()
        {
            return "";
        }
        public override string ToString()
        {
            return $"StatementNode:";
        }

        public override string ToAlgLang()
        {
            return "";
        }
    }
}

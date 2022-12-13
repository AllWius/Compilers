using lab2.Models.SyntaxNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sp_lab1.Synt.SyntTreeNodes
{
    public class DeclarationNode : StatementNode
    {
        public string Identificator;
        public string Type;

        public DeclarationNode() { }
        public DeclarationNode(string identificator, string type)
        {
            this.Identificator = identificator;
            this.Type = type;
        }

        public override string ToString()
        {
            return $"DeclarationNode: {Identificator}, {Type}";
        }

        public override string ToAlgLang()
        {
            string type = Type;
            if (type == "float") type = "вещ";
            else type = "цел";
            return $"{type} {Identificator}";
        }
    }
}

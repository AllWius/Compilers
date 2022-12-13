using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sp_lab1.Synt.SyntTreeNodes
{
    public class BaseNode
    {
        public BaseNode() { }

        public virtual string ToAlgLang()
        {
            return "";
        }
        
    }
}

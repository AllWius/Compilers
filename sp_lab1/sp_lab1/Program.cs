using lab2.Models.SyntaxNodes;
using sp_lab1.Lex;
using sp_lab1.Synt;
using sp_lab1.Synt.SyntTreeNodes;
using System;

namespace sp_lab1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LexAnlz lAnz = new LexAnlz();
            var result = lAnz.ParseFile(@"../../../Examples/ex1.txt");
            var temp = result;
            SyntAnlz parser = new SyntAnlz(temp.Item1 as List<Lexem>);
            var SyntTree = parser.GetTree();
            TreePrint(SyntTree, "");

            Console.WriteLine(SyntTree.ToAlgLang());
            //Console.WriteLine("Lexems: ");
            //Console.WriteLine("================================================");
            //foreach (var lexem in result.Item1)
            //{
            //    Console.WriteLine(lexem.getLexrem());
            //}
            //Console.WriteLine("================================================");
            //Console.WriteLine("Variables: ");
            //foreach (var variable in result.Item2)
            //{
            //    Console.WriteLine(variable.getLexrem());
            //}
            //Console.WriteLine("================================================");

            Console.Read();
        }

        public static void TreePrint(StatementNode root, string tub)
        {
            if (root == null) return;

            Console.WriteLine($"{tub}({root.ToString()})─┐");

            ForNode For = root as ForNode;
            SetNode Set = root as SetNode;
            ArithmeticalNode Arith = root as ArithmeticalNode;
            BlockNode Block = root as BlockNode;

            if (For != null)
            {
                TreePrint(For.Id, tub + "\t");
                TreePrint(For.condition, tub + "\t");
                TreePrint(For.expression, tub + "\t");
                TreePrint(For.body, tub + "\t");
            }
            else if(Set != null)
            {
                TreePrint(Set.Expression, tub + "│" + "\t");
            }
            else if(Arith != null)
            {
                TreePrint(Arith.expression_1, tub + "│" + "\t");
                TreePrint(Arith.expression_2, tub + "│" + "\t");
            }
            else if(Block != null)
            {
                TreePrint(Block.node, tub + "\t");
            }
            else {
                SequenceNode sn = root as SequenceNode;

                if (sn == null) return;

                TreePrint(sn.Statement, tub + "│" + "\t");
                TreePrint(sn.Next, tub);
            }
            
        }
    }

}
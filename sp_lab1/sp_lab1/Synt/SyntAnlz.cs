using lab2.Models.SyntaxNodes;
using sp_lab1.Lex;
using sp_lab1.Synt.SyntTreeNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sp_lab1.Synt
{
    public class SyntAnlz
    {
        List<Lexem> Lexems;
        Lexem _currLexem, _prevLexem;
        IdentifiersTable top;

        public SyntAnlz(List<Lexem> Lexems)
        {
            this.Lexems = Lexems;
            NextLexem();
        }

        private void NextLexem()
        {
            if (Lexems != null && Lexems.Count != 0)
            {
                _prevLexem = _currLexem;
                _currLexem = Lexems.First();
                Lexems.Remove(_currLexem);
            }
            else
            {
                top = new IdentifiersTable(null);
                Reset();
            }
        }

        private void Reset()
        {
            top = new IdentifiersTable(null);
            _currLexem = new Lexem();
        }

        public StatementNode GetTree()
        {
            StatementNode tree = Block();
            return tree;
        }

        private void Expect(string s)
        {
            if (_currLexem.getVal() == s)
            {
                NextLexem();
                return;
            }
            else Error(_currLexem.getId().ToString());
            
        }

        private void Error(string s)
        {
            throw new Exception($"Error: {s}");
        }

        private StatementNode Block()
        {
            Expect("{");
            IdentifiersTable table = top;
            top = new IdentifiersTable(top);
            SequenceNode node = GetStatementSequense();
            Expect("}");
            top = table;
            BlockNode bl = new BlockNode(node);
            return bl;
        }

        private StatementNode GetStatement()
        {
            if (_currLexem.getVal() == "}") return new NullNode();
            else
            {
                switch (_currLexem.getType())
                {
                    case LexType.ComFunc:
                        if (_currLexem.getVal() == "for")
                        {
                            NextLexem();
                            Expect("(");
                            StatementNode st = GetStatement();
                            StatementNode ass = Assign();
                            st = new SequenceNode(st, ass);
                            ExpressionNode exp1 = BoolOr();
                            Expect(";");
                            ExpressionNode exp2 = Expression();
                            SequenceNode sec = new SequenceNode(exp2, GetStatement());
                            Expect(")");
                            StatementNode body = Block();
                            return new ForNode(st, exp1, sec, body);
                        }
                        else if (_currLexem.getVal() == "if")
                        {
                            NextLexem();
                            Expect("(");
                            ExpressionNode condition = BoolOr();
                            Expect(")");
                            StatementNode then = Block();
                            StatementNode? elseStatement = null;
                            if (_currLexem.getVal() == "else")
                            {
                                NextLexem();
                                elseStatement = null;
                                if (_currLexem.getVal() == "{")
                                {
                                    elseStatement = Block();
                                }
                                else if (_currLexem.getVal() == "if")
                                {
                                    elseStatement = GetStatementSequense();
                                }
                            }
                            return new IfNode(condition, then, elseStatement);
                        }
                        break;
                    case LexType.DataType:
                        string Type = _currLexem.getVal();
                        NextLexem();
                        string name = _currLexem.getVal();
                        if (top.Get(_currLexem) != null)
                        {
                            throw new Exception($"Variable {name} already declared");
                        }
                        if (_currLexem.getType() != LexType.ID)
                        {
                            throw new Exception($"Expected Identificator");
                        }
                        top.Put(_currLexem, new IdentifierNode(name, Type));
                        DeclarationNode node = new DeclarationNode(name, Type);
                        NextLexem();
                        return node;
                        break;
                    case LexType.Op:
                        if (_currLexem.getVal() == "++" || _currLexem.getVal() == "--" && _prevLexem.getType() == LexType.ID)
                        {
                            string op = _currLexem.getVal();
                            string id = _prevLexem.getVal();
                            if (top.Get(_prevLexem) == null)
                            {
                                throw new Exception($"Variable {_prevLexem.getVal()} undefined");
                            }
                            NextLexem();
                            Expect(";");
                            return new IncrementNode(op, id);
                        }
                        if (_currLexem.getVal() == "=")
                        {
                            return Assign();
                        }
                        break;
                    case LexType.ID:
                        NextLexem();
                        return GetStatement();
                        break;
                    case LexType.Del:
                        if (_currLexem.getVal() == "{")
                        {
                            StatementNode st = Block();
                            return st;
                        }
                        break;
                }
            }
            return null;
        }
        private StatementNode Assign()
        {
            if (_prevLexem.getType() != LexType.ID) throw new Exception("Syntax error");
            string name = _prevLexem.getVal();
            IdentifierNode identifier = top.Get(_prevLexem);
            if (identifier == null)
            {
                throw new Exception($"undefined variable {name}");
            }
            NextLexem();
            SetNode statement = new SetNode(name, BoolOr());
            Expect(";");
            return statement;
        }
        private SequenceNode GetStatementSequense()
        {
            Lexem lex = new Lexem();
            if (_currLexem.getVal() == "}" || _currLexem.getVal() == lex.getVal()) return null; //new NullNode();
            while (_currLexem.getVal() == ";") NextLexem();
            return new SequenceNode(GetStatement(), GetStatementSequense());
        }


        private ExpressionNode BoolOr()
        {
            ExpressionNode expression = BoolAnd();
            while (_currLexem.getVal() == "||")
            {
                string op = _currLexem.getVal();
                NextLexem();
                expression = new ArithmeticalNode(op, expression, BoolAnd());
            }
            return expression;
        }
        private ExpressionNode BoolAnd()
        {
            ExpressionNode expression = BoolMatch();
            while (_currLexem.getVal() == "&&")
            {
                string op = _currLexem.getVal();
                NextLexem();
                expression = new ArithmeticalNode(op, expression, BoolMatch());
            }
            return expression;
        }

        private ExpressionNode BoolMatch()
        {
            ExpressionNode expression = Expression();
            while (_currLexem.getVal() == "==" || _currLexem.getVal() == ">" || _currLexem.getVal() == "<" || _currLexem.getVal() == "<=" || _currLexem.getVal() == ">=" || _currLexem.getVal() == "!=")
            {
                string op = _currLexem.getVal();
                NextLexem();
                expression = new ArithmeticalNode(op, expression, Expression());
            }
            return expression;
        }
        private ExpressionNode Expression()
        {
            ExpressionNode expression = Term();
            while (_currLexem.getVal() == "+" ||
                _currLexem.getVal() == "-")
            {
                string op = _currLexem.getVal();
                NextLexem();
                expression = new ArithmeticalNode(op, expression, Term());
            }
            return expression;
        }

        private ExpressionNode Term()
        {
            ExpressionNode expression = Unary();
            while (_currLexem.getVal() == "*" ||
                _currLexem.getVal() == "/")
            {
                string op = _currLexem.getVal();
                NextLexem();
                expression = new ArithmeticalNode(op, expression, Unary());
            }
            return expression;
        }

        private ExpressionNode Unary()
        {
            if (_currLexem.getVal() == "-")
            {
                NextLexem();
                return new UnaryNode("-", Unary());
            }
            else if (_currLexem.getVal() == "!")
            {
                string op = _currLexem.getVal();
                NextLexem();
                return new NotNode(op, Unary());
            }
            else
            {
                return Factor();
            }
        }
        private ExpressionNode Factor()
        {
            ExpressionNode expressionNode = null;

            if (_currLexem.getType() == LexType.Num)
            {
                expressionNode = new ConstantNode(_currLexem.getVal(), _currLexem.getData());
                NextLexem();
                return expressionNode;
            }
            if (_currLexem.getType() == LexType.ID)
            {
                IdentifierNode identifier = top.Get(_currLexem);
                if (identifier == null)
                {
                    throw new Exception($"undefined variable {_currLexem.getVal()}");
                }
                NextLexem();
                return identifier;
            }
            if (_currLexem.getVal() == "(")
            {
                NextLexem();
                expressionNode = BoolOr();
                Expect(")");
                return expressionNode;
            }
            throw new Exception("Syntax error");
        }
    }

}


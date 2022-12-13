using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sp_lab1.Lex
{
    public class LexAnlz
    {
        public LexAnlz() { }

        private List<Lexem> _lexs = new List<Lexem>();
        private List<Lexem> _vars = new List<Lexem>();
        private StringReader? _reader;
        private string _temp = "";
        private char[] _nextChar = new char[1];
        private int _charInd = 1, _lineInd = 1;
        private int _state = 0;
        private bool fl = false;

        public Tuple<IList<Lexem>, IList<Lexem>> ParseFile(string fileName)
        {
            _nextChar[0] = ' ';
            using (_reader = new StringReader(File.ReadAllText(fileName)))
            {
                while (_state != -1)
                {
                    switch (_state)
                    {
                        case 0:
                            {
                                if (_reader.Peek() == -1 && _nextChar[0] == '\0')
                                {
                                    _state = -2;
                                    break;
                                }
                                if (IsSpaceOrNewLine(_nextChar[0])) GetNextChar();
                                else if (char.IsDigit(_nextChar[0]))
                                {
                                    _temp = "";
                                    _temp += _nextChar[0];

                                    _state = 1;
                                    GetNextChar();
                                }
                                else if (char.IsLetter(_nextChar[0]))
                                {
                                    _temp = "";
                                    _temp += _nextChar[0];

                                    _state = 2;
                                    GetNextChar();
                                }
                                else
                                {
                                    _temp += _nextChar[0];

                                    _state = 3;
                                    GetNextChar();
                                }
                                break;
                            }
                        case 1:
                            {
                                if (char.IsDigit(_nextChar[0]) || (_nextChar[0] == '.'))
                                {
                                    if (_nextChar[0] == '.') fl = true;
                                    _temp += _nextChar[0];
                                    GetNextChar();
                                }
                                else
                                {
                                    if (fl)
                                    {
                                        _lexs.Add(new Lexem(LexType.Num, _temp, $"{_temp}", "float"));
                                        fl = false;
                                    }
                                    else _lexs.Add(new Lexem(LexType.Num, _temp, $"{_temp}", "int"));
                                    _temp = "";
                                    _state = 0;
                                }
                                break;
                            }
                        case 2:
                            if (char.IsLetterOrDigit(_nextChar[0]))
                            {
                                _temp += _nextChar[0];
                                GetNextChar();
                            }
                            else
                            {
                                var idType = FindType();
                                var keyWord = FindKeyWord();

                                if(keyWord.Item1 != -1)
                                {
                                    _lexs.Add(new Lexem(LexType.ComFunc, $"{idType.Item1}", idType.Item2));
                                    _temp = "";
                                    _state = 0;
                                }
                                else if(idType.Item1 != -1)
                                {
                                    _lexs.Add(new Lexem(LexType.DataType, $"{idType.Item1}", idType.Item2));
                                    _temp = "";
                                    _state = 0;
                                }
                                else
                                {
                                    bool f = false;
                                    foreach(var v in _vars)
                                    {
                                        if (v.getVal().Equals(_temp))
                                        {
                                            f = true;
                                            break;
                                        }
                                    }
                                    if (!f)
                                    {
                                        _vars.Add(new Lexem(LexType.ID, $"{_vars.Count}", _temp));
                                        _lexs.Add(new Lexem(LexType.ID, $"{_vars.Count - 1}", $"{_temp}"));
                                    }
                                    else
                                    {
                                        _lexs.Add(new Lexem(LexType.ID, $"{_vars.FindIndex(c => c.getVal() == _temp)}", $"{_temp}"));
                                    }
                                    _temp = "";
                                    _state = 0;
                                }

                            }
                            break;
                        case 3:

                            var op = FindOperator();
                            var del = FindKeySymbol();

                            if(op.Item1 != -1)
                            {
                                _lexs.Add(new Lexem(LexType.Op, $"{op.Item1}", op.Item2));
                                _temp = "";
                                _state = 0;
                            }
                            else if(del.Item1 != -1)
                            {
                                _lexs.Add(new Lexem(LexType.Del, $"{del.Item1}", del.Item2)); ;
                                _temp = "";
                                _state = 0;
                            }
                            else
                            {
                                _lexs.Add(new Lexem(LexType.Err, $"{-1}", $"Parsing error at {_lineInd} line {_charInd} char"));
                                _temp = "";
                                _state = -2;
                            }
                            break;
                        case -2:
                            return new Tuple<IList<Lexem>, IList<Lexem>>(_lexs, _vars);
                            _state = -1;
                            break;
                    }
                }
            }
            return new Tuple<IList<Lexem>, IList<Lexem>>(_lexs, _vars);
        }

        private void GetNextChar()
        {
            if (_reader.Peek() == -1) _nextChar[0] = '\0';
            _reader.Read(_nextChar, 0, 1);
            _charInd++;
        }

        private (int, string) FindType() {
            var res = Constants.Types.ContainsKey(_temp);

            if(res)
            {
                return (Constants.Types[_temp].Item1, _temp);
            }
            return (-1, _temp);
        }

        private (int, string) FindOperator()
        {
            var res = Constants.Operators.ContainsKey(_temp);
            if (Constants.Operators.ContainsKey(_temp + _nextChar[0]))
            {
                _temp += _nextChar[0];
                GetNextChar();
                res = Constants.Operators.ContainsKey(_temp);
            }

            if (res)
            {
                return (Constants.Operators[_temp].Item1, _temp);
            }
            return (-1, _temp);
        }

        private (int, string) FindKeyWord()
        {
            var res = Array.FindIndex(Constants.Keywords, a => a.Equals(_temp));

            if (res != -1)
            {
                return (res, _temp);
            }
            return (-1, _temp);
        }

        private (int, string) FindKeySymbol()
        {
            var res = Array.FindIndex(Constants.KeySymbols, a => a.Equals(_temp));

            if (res != -1)
            {
                return (res,_temp);
            }
            return (-1, _temp);
        }

        private bool IsSpaceOrNewLine(char ch)
        {
            if (ch == '\n')
            {
                _lineInd++;
                _charInd = 0;
                return true;
            }
            return ch == ' ' || ch == '\t' || ch == '\0' || ch == '\r';
        }
    }

    public enum LexType { Err = -1, ComFunc = 0, Del = 1, DataType = 2, ID = 3, Op = 4, Num = 5,};

    public struct Lexem
    {
        public LexType _type;
        public string _id;
        public string _val;
        public string _dataType;

        public Lexem(LexType _type, string _id, string _val, string _dataType = "")
        {
            this._type = _type;
            this._id = _id;
            this._val = _val;
            this._dataType = _dataType;
        }

        public string getLexrem()
        {
            return $"| Type:   {_type}\t| ID:   {_id}\t| Value:   {_val}";
        }

        public LexType getType() { return _type; }

        public string getId() { return _id; }

        public string getVal() { return _val; }

        public string getData() { return _dataType; }

        public static bool operator == (Lexem l1, Lexem l2)
        {
            return l1.getLexrem() == l2.getLexrem();
        }
        public static bool operator !=(Lexem l1, Lexem l2)
        {
            return l1.getLexrem() != l2.getLexrem();
        }
    }

    public class Constants
    {
        public static readonly Dictionary<string, (int, string)> Types = new Dictionary<string, (int, string)>() {
            { "int", (0, "32-bit integer") },
            { "uint", (1, "32-bit unsigned integer") },
            { "long", (2, "64-bit integer") },
            { "ulong", (3, "64-bit unsigned integer") },
            { "float", (4, "32-bit float") },
            { "string", (5, "string of chars")},
        };

        public static readonly Dictionary<string, (int, string)> Operators = new Dictionary<string, (int, string)>() {
            {"=", (0, "assign_operation")},
            {"+", (1, "sum_operation")},
            {"-", (2, "subtract_operation")},
            {"*", (3, "multiply_operation")},
            {"/", (4, "divide_operation")},
            {"+=", (5, "add_amount_operation")},
            {"-=", (6, "subtract_amount_operation")},
            {"==", (7, "are_equal_operation")},
            {">", (8, "more_operation")},
            {"<", (9, "less_operation")},
            {"<=", (10, "less_or_equal_operation")},
            {">=", (11, "more_or_equal_operation")},
            {"++", (12, "increment")},
            {"--", (13, "decrement")}
        };
        public static readonly string[] Keywords = { "class", "public", "private", "for", "return", "if", "else", "while" };

        public static readonly string[] KeySymbols = { ".", ";", ",", "(", ")", "[", "]", "{", "}" };
    }
}

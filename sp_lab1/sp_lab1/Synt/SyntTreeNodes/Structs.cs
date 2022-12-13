using sp_lab1.Lex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sp_lab1.Synt.SyntTreeNodes {
	public class IdentifiersTable
	{
		private Dictionary<string, IdentifierNode> _table;
		protected IdentifiersTable _previous;

		public IdentifiersTable(IdentifiersTable prev)
		{
			_table = new Dictionary<string, IdentifierNode>();
			_previous = prev;
		}

		public void Put(Lexem token, IdentifierNode identifier)
		{
			_table.Add(token.getVal(), identifier);
		}

		public IdentifierNode? Get(Lexem token)
		{
			for (IdentifiersTable t = this; t != null; t = t._previous)
			{
				if (t._table.ContainsKey(token.getVal()))
				{
					return t._table[token.getVal()];
				}
			}

			return null;
		}
	}

}

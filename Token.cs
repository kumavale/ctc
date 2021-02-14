using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ctc
{
    public class Token
    {
        public TokenKind kind { get; }
        public string? str    { get; }

        public Token(TokenKind kind)
        {
            this.kind = kind;
        }

        public Token(TokenKind kind, string str)
        {
            this.kind = kind;
            this.str  = str;
        }
    }

    public enum TokenKind
    {
        String,
        Sequence,
        Year,
        Month,
        Day,
        Hour,
        Minute,
        Second,
        MilliSecond,
    }
}

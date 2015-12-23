using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Tokens
{
    public enum TokenType
    {
        Tilde,
        BackTick,
        SingleQuote,
        DoubleQuote,
        Exclaimation,
        At,
        Hash,
        Dollar,
        Percent,
        Hat,
        Ampersand,
        Star,
        LeftRoundBracket,
        RightRoundBracket,
        Dash,
        Equals,
        Underscore,
        Plus,
        LeftCurlyBracket,
        LeftSquareBracket,
        RightCurlyBracket,
        RightSquareBracket,
        Vertical,
        SemiColon,
        Colon,
        Comma,
        LeftAngleBracket,
        Period,
        RightAngleBracket,
        Question,

        Number,
        Identifier,
        String
    };

    public class Token
    {
        public TokenType type { get; protected set; }
        public String value { get; protected set; }
        public bool valid { get; protected set; }
    }
}

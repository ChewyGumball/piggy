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
        Slash,
        Backslash,

        Number,
        Name,
        Boolean,
        Error,
        End
    };

    public class Token
    {
        public static Dictionary<TokenType, int> precedenceTable = new Dictionary<TokenType, int>
        {
            { TokenType.End, -1 },
            { TokenType.SemiColon, -1 },
            { TokenType.Comma, -1 },
            { TokenType.Plus, 40},
            { TokenType.Dash, 40},
            { TokenType.Star, 50},
            { TokenType.Slash, 50},
            { TokenType.Equals, 60 },
            { TokenType.LeftRoundBracket, 100 }
        };

        public static readonly Dictionary<String, TokenType> SymbolTable = new Dictionary<String, TokenType>
        {
            { "~", TokenType.Tilde },
            { "`", TokenType.BackTick },
            { "'", TokenType.SingleQuote },
            { "\"", TokenType.DoubleQuote },
            { "!", TokenType.Exclaimation },
            { "@", TokenType.At },
            { "#", TokenType.Hash },
            { "$", TokenType.Dollar },
            { "%", TokenType.Percent },
            { "^", TokenType.Hat },
            { "&", TokenType.Ampersand },
            { "*", TokenType.Star },
            { "(", TokenType.LeftRoundBracket },
            { ")", TokenType.RightRoundBracket },
            { "-", TokenType.Dash },
            { "=", TokenType.Equals },
            { "_", TokenType.Underscore },
            { "+", TokenType.Plus },
            { "{", TokenType.LeftCurlyBracket },
            { "[", TokenType.LeftSquareBracket },
            { "}", TokenType.RightCurlyBracket },
            { "]", TokenType.RightSquareBracket },
            { "|", TokenType.Vertical },
            { ";", TokenType.SemiColon },
            { ":", TokenType.Colon },
            { ",", TokenType.Comma },
            { "<", TokenType.LeftAngleBracket },
            { ".", TokenType.Period },
            { ">", TokenType.RightAngleBracket },
            { "?", TokenType.Question },
            { "/", TokenType.Slash },
            { "\\", TokenType.Backslash }
        };

        static Token()
        {
            //Fill in non explicit precedences to 0 (always stop)
            foreach (TokenType t in Enum.GetValues(typeof(TokenType)))
            {
                if (!precedenceTable.ContainsKey(t))
                {
                    precedenceTable[t] = 0;
                }
            }
        }

        public TokenType type { get; protected set; }
        public String value { get; protected set; }
        public bool valid { get; protected set; }


        public int precedence
        {
            get { return precedenceTable[type]; }
        }

        public Token(TokenType type, String value)
        {
            this.type = type;
            this.value = value;
            this.valid = true;
        }
    }
}

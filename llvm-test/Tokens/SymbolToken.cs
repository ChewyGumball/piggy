using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Tokens
{
    public class SymbolToken : Token<SymbolToken.Symbol>
    {
        public enum Symbol {
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
            Question
        };

        public static Dictionary<String, Symbol> SymbolTable = new Dictionary<string, Symbol>
        {
            { "~", Symbol.Tilde },
            { "`", Symbol.BackTick },
            { "'", Symbol.SingleQuote },
            { "\"", Symbol.DoubleQuote },
            { "!", Symbol.Exclaimation },
            { "@", Symbol.At },
            { "#", Symbol.Hash },
            { "$", Symbol.Dollar },
            { "%", Symbol.Percent },
            { "^", Symbol.Hat },
            { "&", Symbol.Ampersand },
            { "*", Symbol.Star },
            { "(", Symbol.LeftRoundBracket },
            { ")", Symbol.RightRoundBracket },
            { "-", Symbol.Dash },
            { "=", Symbol.Equals },
            { "_", Symbol.Underscore },
            { "+", Symbol.Plus },
            { "{", Symbol.LeftCurlyBracket },
            { "[", Symbol.LeftSquareBracket },
            { "}", Symbol.RightCurlyBracket },
            { "]", Symbol.RightSquareBracket },
            { "|", Symbol.Vertical },
            { ";", Symbol.SemiColon },
            { ":", Symbol.Colon },
            { ",", Symbol.Comma },
            { "<", Symbol.LeftAngleBracket },
            { ".", Symbol.Period },
            { ">", Symbol.RightAngleBracket },
            { "?", Symbol.Question }
        };

        public SymbolToken(String value)
        {
            Symbol symbolValue;
            valid = SymbolTable.TryGetValue(value, out symbolValue);
            this.value = symbolValue;
        }
    }
}

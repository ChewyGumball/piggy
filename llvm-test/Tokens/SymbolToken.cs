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



        public SymbolToken(String value)
        {
            Symbol symbolValue;
            valid = SymbolTable.TryGetValue(value, out symbolValue);
            this.value = symbolValue;
        }
    }
}

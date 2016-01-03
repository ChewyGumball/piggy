using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using llvm_test.Tokens;
using llvm_test.Parsing.Expressions;
using llvm_test.Parsing.Expressions.Literal;

namespace llvm_test.Parsing.Parslets
{
    class LiteralParslets
    {
        private static Dictionary<String, String> escapedCharacters = new Dictionary<string, string>
        {
            { "n", "\n" },
        };

        public static Expression numberLiteral(Parser p, Token t)
        {
            return new IntegralLiteralExpression(Convert.ToInt64(t.value));
        }
        public static Expression stringLiteral(Parser p, Token t)
        {
            StringBuilder stringValue = new StringBuilder();
            Token nextToken = p.consume();
            while(nextToken.type != TokenType.DoubleQuote)
            {
                if (nextToken.type == TokenType.Backslash)
                {
                    nextToken = p.consume();
                    if(escapedCharacters.ContainsKey(nextToken.value))
                    {
                        stringValue.Append(escapedCharacters[nextToken.value]);
                    }
                    else
                    {
                        stringValue.Append(nextToken.value);
                    }
                }
                else
                {
                    stringValue.Append(nextToken.value);
                }

                nextToken = p.consume();

                if(nextToken.type == TokenType.End)
                {
                    throw new Exception("Missing end of string marker!");
                }
            }
            return new StringLiteralExpression(stringValue.ToString());
        }
        public static Expression booleanLiteral(Parser p, Token t)
        {
            return new BooleanLiteralExpression(t.value == "true");
        }
    }
}

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
        public static Expression numberLiteral(Parser p, Token t)
        {
            return new IntegralLiteralExpression(Convert.ToInt64(t.value));
        }
        public static Expression stringLiteral(Parser p, Token t)
        {
            return new StringLiteralExpression(t.value);
        }
        public static Expression booleanLiteral(Parser p, Token t)
        {
            return new BooleanLiteralExpression(t.value == "true");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using llvm_test.Tokens;
using llvm_test.Parsing.Expressions;
using llvm_test.Parsing.Expressions.Logical;

namespace llvm_test.Parsing.Parslets
{
    class LogicalParslets
    {
        public static Expression logicalNot(Parser p, Token t)
        {
            return new LogicalNotExpression(p.parseExpression(t.precedence));
        }

        public static Expression greaterThan(Parser p, Expression left, Token t)
        {
            return new GreaterThanExpression(left, p.parseExpression(t.precedence));
        }
    }
}

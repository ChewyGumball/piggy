using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using llvm_test.Tokens;
using llvm_test.Parsing.Expressions;
using llvm_test.Parsing.Expressions.Bitwise;

namespace llvm_test.Parsing.Parslets
{
    class BitwiseParslets
    {
        public static Expression bitwiseNot(Parser p, Token t)
        {
            return new BitwiseNotExpression(p.parseExpression(t.precedence));
        }
    }
}

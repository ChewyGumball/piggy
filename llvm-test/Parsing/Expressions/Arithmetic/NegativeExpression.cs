using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using llvm_test.Tokens;

namespace llvm_test.Parsing.Expressions.Arithmetic
{
    class NegativeExpression : PrefixExpression
    {
        public NegativeExpression(Expression value) : base(value) { }
    }
}

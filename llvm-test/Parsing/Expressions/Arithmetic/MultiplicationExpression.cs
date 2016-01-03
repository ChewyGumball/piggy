using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing.Expressions.Arithmetic
{
    class MultiplicationExpression : BinaryInfixExpression
    {
        public MultiplicationExpression(Expression left, Expression right) : base(left, right) { }
    }
}

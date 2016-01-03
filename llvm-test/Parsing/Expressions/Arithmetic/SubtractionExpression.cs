using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing.Expressions.Arithmetic
{
    class SubtractionExpression : BinaryInfixExpression
    {
        public SubtractionExpression(Expression left, Expression right) : base(left, right) { }
    }
}

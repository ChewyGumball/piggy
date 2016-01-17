using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing.Expressions.Logical
{
    public class GreaterThanExpression : BinaryInfixExpression
    {
        public GreaterThanExpression(Expression left, Expression right) : base(left, right) { }
    }
}

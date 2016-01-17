using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing.Expressions.Logical
{
    public class LessThanExpression : BinaryInfixExpression
    {
        public LessThanExpression(Expression left, Expression right) : base(left, right) { }
    }
}

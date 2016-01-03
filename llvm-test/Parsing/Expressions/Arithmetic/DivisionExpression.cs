using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing.Expressions.Arithmetic
{
    public class DivisionExpression : BinaryInfixExpression
    {
        public DivisionExpression(Expression left, Expression right) : base(left, right) { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing.Expressions
{
    abstract class BinaryInfixExpression : Expression
    {
        public Expression left { get; protected set; }
        public Expression right { get; protected set; }
        public BinaryInfixExpression(Expression left, Expression right)
        {
            this.left = left;
            this.right = right;
        }
    }
}

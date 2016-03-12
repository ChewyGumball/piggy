using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing.Expressions.Arithmetic
{
    public class MultiplicationExpression : BinaryInfixExpression
    {
        public MultiplicationExpression(Expression left, Expression right) : base(left, right) { }

        public override String print(int indentation = 0)
        {
            StringBuilder b = new StringBuilder();
            b.Append('\t', indentation);
            b.AppendLine("(Multiplication Expression)");
            b.Append(base.print(indentation));

            return b.ToString();
        }
    }
}

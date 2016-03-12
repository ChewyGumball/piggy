using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing.Expressions
{
    public abstract class BinaryInfixExpression : Expression
    {
        public Expression left { get; protected set; }
        public Expression right { get; protected set; }
        public BinaryInfixExpression(Expression left, Expression right)
        {
            this.left = left;
            this.right = right;
        }

        public override string print(int indentation = 0)
        {
            StringBuilder b = new StringBuilder();
            b.Append('\t', indentation);
            b.AppendLine("[Left]");
            b.Append(left.print(indentation + 1));
            b.Append('\t', indentation);
            b.AppendLine("[Right]");
            b.Append(right.print(indentation + 1));

            return b.ToString();
        }
    }
}

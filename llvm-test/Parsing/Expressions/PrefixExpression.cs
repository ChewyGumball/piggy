using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing.Expressions
{
    abstract class PrefixExpression : Expression
    {
        public Expression value { get; protected set; }
        public PrefixExpression(Expression value)
        {
            this.value = value;
        }

        public override string print(int indentation = 0)
        {
            StringBuilder b = new StringBuilder();
            b.Append('\t', indentation);
            b.AppendLine("[Operand]");
            b.Append(value.print(indentation + 1));

            return b.ToString();
        }
    }
}

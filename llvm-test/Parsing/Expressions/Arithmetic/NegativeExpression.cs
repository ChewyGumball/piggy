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

        public override string print(int indentation = 0)
        {
            StringBuilder b = new StringBuilder();
            b.Append('\t', indentation);
            b.AppendLine("(Arithmetic Negation Expression)");
            b.Append(base.print(indentation));

            return b.ToString();
        }
    }
}

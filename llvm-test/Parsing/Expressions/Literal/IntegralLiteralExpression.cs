using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing.Expressions.Literal
{
    public class IntegralLiteralExpression : Expression
    {
        public long value { get; private set; }
        public IntegralLiteralExpression(long value)
        {
            this.value = value;
        }
    }
}

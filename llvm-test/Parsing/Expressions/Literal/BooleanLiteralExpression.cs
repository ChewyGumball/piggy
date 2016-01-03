using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing.Expressions.Literal
{
    class BooleanLiteralExpression : Expression
    {
        public bool value { get; private set; }
        public BooleanLiteralExpression(bool value)
        {
            this.value = value;
        }
    }
}

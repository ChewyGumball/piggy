using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing.Expressions.Literal
{
    class StringLiteralExpression : Expression
    {
        public String value { get; private set; }
        public StringLiteralExpression(String value)
        {
            this.value = value;
        }
    }
}

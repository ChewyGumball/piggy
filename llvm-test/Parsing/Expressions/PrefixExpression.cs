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
    }
}

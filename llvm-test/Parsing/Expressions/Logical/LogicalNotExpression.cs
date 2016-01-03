using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing.Expressions.Logical
{
    class LogicalNotExpression : PrefixExpression
    {
        public LogicalNotExpression(Expression value) : base(value) { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing.Expressions
{
    class BlockExpression : Expression
    {
        List<Expression> innerExpressions;
        public BlockExpression(List<Expression> expressions)
        {
            innerExpressions = expressions;
        }
    }
}

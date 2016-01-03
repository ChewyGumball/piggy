using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing.Expressions
{
    public class BlockExpression : Expression
    {
        public List<Expression> innerExpressions { get; protected set; }
        public BlockExpression(List<Expression> expressions)
        {
            innerExpressions = expressions;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing.Expressions.Names
{
    class WhileLoopExpression : Expression
    {
        Expression condition;
        BlockExpression loop;

        public WhileLoopExpression(Expression condition, BlockExpression loop)
        {
            this.condition = condition;
            this.loop = loop;
        }
    }
}

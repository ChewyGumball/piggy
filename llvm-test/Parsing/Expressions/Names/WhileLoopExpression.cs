using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing.Expressions.Names
{
    public class WhileLoopExpression : Expression
    {
        public Expression condition { get; protected set; }
        public BlockExpression loop { get; protected set; }

        public WhileLoopExpression(Expression condition, BlockExpression loop)
        {
            this.condition = condition;
            this.loop = loop;
        }
    }
}

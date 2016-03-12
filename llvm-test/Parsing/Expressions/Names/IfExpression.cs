using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing.Expressions.Names
{
    class IfExpression : Expression
    {
        public Expression condition { get; protected set; }
        public BlockExpression body { get; protected set; }

        public IfExpression(Expression condition, BlockExpression body)
        {
            this.condition = condition;
            this.body = body;
        }

        public override string print(int indentation = 0)
        {
            StringBuilder b = new StringBuilder();
            b.Append('\t', indentation);
            b.AppendLine("(If Statement)");
            b.Append('\t', indentation);
            b.AppendLine("[Condition]");
            b.Append(condition.print(indentation + 1));
            b.Append('\t', indentation);
            b.AppendLine("[Body]");
            b.Append(body.print(indentation + 1));

            return b.ToString();
        }
    }
}

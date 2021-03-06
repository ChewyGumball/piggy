﻿using System;
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

        public override string print(int indentation = 0)
        {
            StringBuilder b = new StringBuilder();
            b.Append('\t', indentation);
            b.AppendLine("(While Loop)");
            b.Append('\t', indentation);
            b.AppendLine("[Condition]");
            b.Append(condition.print(indentation + 1));
            b.Append('\t', indentation);
            b.AppendLine("[Body]");
            b.Append(loop.print(indentation + 1));

            return b.ToString();
        }
    }
}

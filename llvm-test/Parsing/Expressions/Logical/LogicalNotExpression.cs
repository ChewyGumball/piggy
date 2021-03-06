﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing.Expressions.Logical
{
    class LogicalNotExpression : PrefixExpression
    {
        public LogicalNotExpression(Expression value) : base(value) { }

        public override string print(int indentation = 0)
        {
            StringBuilder b = new StringBuilder();
            b.Append('\t', indentation);
            b.AppendLine("(Logical Not Expression)");
            b.Append(base.print(indentation));

            return b.ToString();
        }
    }
}

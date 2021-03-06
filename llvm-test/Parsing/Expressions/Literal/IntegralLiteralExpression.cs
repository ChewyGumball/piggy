﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing.Expressions.Literal
{
    public class IntegralLiteralExpression : Expression
    {
        public long value { get; private set; }
        public IntegralLiteralExpression(long value)
        {
            this.value = value;
        }

        public override string print(int indentation = 0)
        {
            StringBuilder b = new StringBuilder();
            b.Append('\t', indentation);
            b.AppendFormat("(Integral Literal [value = {0}, hex value = 0x{0:x16}])", value).AppendLine();

            return b.ToString();
        }
    }
}

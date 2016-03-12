using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing.Expressions.Literal
{
    public class StringLiteralExpression : Expression
    {
        public String value { get; private set; }
        public StringLiteralExpression(String value)
        {
            this.value = value;
        }

        public override string print(int indentation = 0)
        {
            StringBuilder b = new StringBuilder();
            b.Append('\t', indentation);
            b.AppendFormat("(String Literal [value = \"{0}\"])", value).AppendLine();

            return b.ToString();
        }
    }
}

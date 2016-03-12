using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing.Expressions.Assignment
{
    public class VariableAssignmentExpression : Expression
    {
        public String name { get; protected set; }
        public Expression value { get; protected set; }

        public VariableAssignmentExpression(String name, Expression value)
        {
            this.name = name;
            this.value = value;
        }

        public override string print(int indentation = 0)
        {
            StringBuilder b = new StringBuilder();
            b.Append('\t', indentation);
            b.AppendFormat("(Variable Assignment [name = {0}])", name).AppendLine();
            b.Append('\t', indentation);
            b.AppendLine("[Value]");
            b.Append(value.print(indentation + 1));

            return b.ToString();
        }
    }
}

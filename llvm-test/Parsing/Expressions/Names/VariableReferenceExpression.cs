using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing.Expressions.Names
{
    public class VariableReferenceExpression : Expression
    {
        public String name { get; protected set; }
        public VariableReferenceExpression(String variableName)
        {
            name = variableName;
        }

        public override string print(int indentation = 0)
        {
            StringBuilder b = new StringBuilder();
            b.Append('\t', indentation);
            b.AppendFormat("(Variable Reference [name = {0}])", name).AppendLine();

            return b.ToString();
        }
    }
}

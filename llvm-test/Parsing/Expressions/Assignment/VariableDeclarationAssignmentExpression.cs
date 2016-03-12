using llvm_test.Parsing.Expressions.Names;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing.Expressions.Assignment
{
    public class VariableDeclarationAssignmentExpression : Expression
    {
        public VariableDeclarationExpression declaration { get; protected set; }
        public Expression value { get; protected set; }

        public VariableDeclarationAssignmentExpression(VariableDeclarationExpression declaration, Expression value)
        {
            this.declaration = declaration;
            this.value = value;
        }

        public override string print(int indentation = 0)
        {
            StringBuilder b = new StringBuilder();
            b.Append('\t', indentation);
            b.AppendLine("(Variable Declaration and Assignment Expression)");
            b.Append('\t', indentation);
            b.AppendLine("[Declaration]");
            b.Append(declaration.print(indentation + 1));
            b.Append('\t', indentation);
            b.AppendLine("[Value]");
            b.Append(value.print(indentation + 1));

            return b.ToString();
        }
    }
}

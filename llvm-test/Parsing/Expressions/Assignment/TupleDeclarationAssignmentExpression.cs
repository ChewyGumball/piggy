using llvm_test.Parsing.Expressions.Tuples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing.Expressions.Assignment
{
    public class TupleDeclarationAssignmentExpression : Expression
    {
        public TupleDeclarationExpression names { get; protected set; }
        public Expression values { get; protected set; }

        public TupleDeclarationAssignmentExpression(TupleDeclarationExpression names, Expression values)
        {
            this.names = names;
            this.values = values;
        }

        public override string print(int indentation = 0)
        {
            StringBuilder b = new StringBuilder();
            b.Append('\t', indentation);
            b.AppendLine("(Tuple Declaration and Assignment Expression)");
            b.Append('\t', indentation);
            b.AppendLine("[Declaration]");
            b.Append(names.print(indentation + 1));
            b.Append('\t', indentation);
            b.AppendLine("[Value]");
            b.Append(values.print(indentation + 1));

            return b.ToString();
        }
    }
}

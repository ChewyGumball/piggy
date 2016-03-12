using llvm_test.Parsing.Expressions.Names;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing.Expressions.Tuples
{
    public class TupleDeclarationExpression : Expression
    {
        public List<VariableDeclarationExpression> members { get; protected set; }
        public TupleDeclarationExpression(List<VariableDeclarationExpression> members)
        {
            this.members = members;
        }

        public override string print(int indentation = 0)
        {
            StringBuilder b = new StringBuilder();
            b.Append('\t', indentation);
            b.AppendFormat("(Tuple Declaration [length = {0}])", members.Count).AppendLine();
            b.Append('\t', indentation);
            b.AppendLine("[Members]");
            members.ForEach(member => b.Append(member.print(indentation + 1)));

            return b.ToString();
        }
    }
}

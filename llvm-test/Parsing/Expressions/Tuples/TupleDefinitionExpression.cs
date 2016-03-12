using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing.Expressions.Tuples
{
    public class TupleDefinitionExpression : Expression
    {
        public List<Expression> members { get; protected set; }
        public TupleDefinitionExpression(List<Expression> members)
        {
            this.members = members;
        }

        public override string print(int indentation = 0)
        {
            StringBuilder b = new StringBuilder();
            b.Append('\t', indentation);
            b.AppendFormat("(Tuple Definition Expression [length = {0}])", members.Count).AppendLine();
            b.Append('\t', indentation);
            b.AppendLine("[Members]");
            members.ForEach(member => b.Append(member.print(indentation + 1)));

            return b.ToString();
        }
    }
}

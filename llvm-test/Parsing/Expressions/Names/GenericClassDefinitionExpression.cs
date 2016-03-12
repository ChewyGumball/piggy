using llvm_test.Parsing.Expressions.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing.Expressions.Names
{
    public class GenericClassDefinitionExpression : ClassDefinitionExpression
    {
        public List<TypeName> genericTypes { get; protected set; }
        public GenericClassDefinitionExpression(GenericTypeName name, List<Expression> members, Visibility visibility = Visibility.None)
            :base(name.name, members, visibility)
        {
            genericTypes = name.genericTypes;
        }

        public override string print(int indentation = 0)
        {
            StringBuilder b = new StringBuilder();
            b.Append('\t', indentation);
            b.AppendFormat("(Generic Class Definition [name = {0}, visibility = {1}, types = {2}])", name, visibility, String.Join(",", genericTypes.Select(x => x.print()))).AppendLine();
            b.Append('\t', indentation);
            b.AppendLine("[Members]");

            foreach (Expression member in members)
            {
                b.Append(member.print(indentation + 1));
            }

            return b.ToString();
        }
    }
}

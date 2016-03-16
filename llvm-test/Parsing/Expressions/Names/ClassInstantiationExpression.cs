using llvm_test.Parsing.Expressions.Tuples;
using llvm_test.Parsing.Expressions.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing.Expressions.Names
{
    public class ClassInstantiationExpression : Expression
    {
        public TypeName type { get; protected set; }
        public TupleDefinitionExpression arguments { get; protected set; }

        public ClassInstantiationExpression(TypeName type, TupleDefinitionExpression arguments)
        {
            this.type = type;
            this.arguments = arguments;
        }

        public override string print(int indentation = 0)
        {
            StringBuilder b = new StringBuilder();
            b.Append('\t', indentation);
            b.AppendFormat("(Class Instantiation [type = {0}])", type.print());
            b.Append('\t', indentation);
            b.AppendLine("[Arguments]");
            b.Append(arguments.print(indentation + 1));

            return b.ToString();
        }
    }
}

using llvm_test.Parsing.Expressions.Tuples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing.Expressions.Names
{
    class FunctionCallExpression : Expression
    {
        public String name { get; protected set; }
        public TupleDefinitionExpression arguments { get; protected set; }
        public FunctionCallExpression(String name, TupleDefinitionExpression arguments)
        {
            this.name = name;
            this.arguments = arguments;
        }
        public override string print(int indentation = 0)
        {
            StringBuilder b = new StringBuilder();
            b.Append('\t', indentation);
            b.AppendFormat("(Function Call [name = {0}])", name).AppendLine();
            b.Append('\t', indentation);
            b.AppendLine("[Arguments]");
            b.Append(arguments.print(indentation + 1));

            return b.ToString();
        }
    }
}

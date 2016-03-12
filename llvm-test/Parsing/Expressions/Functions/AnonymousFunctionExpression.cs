using llvm_test.Parsing.Expressions.Tuples;
using llvm_test.Parsing.Expressions.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing.Expressions.Functions
{
    public class AnonymousFunctionExpression : Expression
    {
        public TupleDeclarationExpression arguments { get; protected set; }
        public TypeName returnType { get; protected set; }
        public BlockExpression body { get; protected set; }

        public AnonymousFunctionExpression(TupleDeclarationExpression arguments, TypeName returnType, BlockExpression body)
        {
            this.arguments = arguments;
            this.returnType = returnType;
            this.body = body;
        }

        public override string print(int indentation = 0)
        {
            StringBuilder b = new StringBuilder();
            b.Append('\t', indentation);
            b.AppendFormat("(Anonymous Function Definition [return type = {2}])", returnType.print()).AppendLine();
            b.Append('\t', indentation);
            b.AppendLine("[Parameters]");
            b.Append(arguments.print(indentation + 1));
            b.Append('\t', indentation);
            b.AppendLine("[Body]");
            b.Append(body.print(indentation + 1));

            return b.ToString();
        }
    }
}

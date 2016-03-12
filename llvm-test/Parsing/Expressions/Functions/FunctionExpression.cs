using llvm_test.Parsing.Expressions.Tuples;
using llvm_test.Parsing.Expressions.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing.Expressions.Functions
{
    public class FunctionExpression : Expression, IVisibilityExpression
    {
        public String name { get; protected set; }
        public TupleDeclarationExpression arguments { get; protected set; }
        public TypeName returnType { get; protected set; }
        public BlockExpression body { get; protected set; }
        public Visibility visibility { get; set; }

        public FunctionExpression(String name, TupleDeclarationExpression arguments, TypeName returnType, BlockExpression body, Visibility visibility = Visibility.None)
        {
            this.name = name;
            this.arguments = arguments;
            this.returnType = returnType;
            this.body = body;
            this.visibility = visibility;
        }

        public override string print(int indentation = 0)
        {
            StringBuilder b = new StringBuilder();
            b.Append('\t', indentation);
            b.AppendFormat("(Function Definition [name = {0}, visibility = {1}, return type = {2}])", name, visibility, returnType.print()).AppendLine();
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

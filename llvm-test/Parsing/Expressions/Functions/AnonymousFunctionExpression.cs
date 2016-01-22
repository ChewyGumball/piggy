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
    }
}

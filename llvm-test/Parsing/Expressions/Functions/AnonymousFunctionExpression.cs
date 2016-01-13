using llvm_test.Parsing.Expressions.Tuples;
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
        public String returnType { get; protected set; }
        public BlockExpression body { get; protected set; }

        public AnonymousFunctionExpression(TupleDeclarationExpression arguments, String returnType, BlockExpression body)
        {
            this.arguments = arguments;
            this.returnType = returnType;
            this.body = body;
        }
    }
}

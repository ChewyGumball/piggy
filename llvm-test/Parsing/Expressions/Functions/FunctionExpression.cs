using llvm_test.Parsing.Expressions.Tuples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing.Expressions.Functions
{
    public class FunctionExpression : Expression
    {
        public String name { get; protected set; }
        public TupleDeclarationExpression arguments { get; protected set; }
        public String returnType { get; protected set; }
        public BlockExpression body { get; protected set; }

        public FunctionExpression(String name, TupleDeclarationExpression arguments, String returnType, BlockExpression body)
        {
            this.name = name;
            this.arguments = arguments;
            this.returnType = returnType;
            this.body = body;
        }
    }
}

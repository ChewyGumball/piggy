using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing.Expressions.Tuples
{
    public class TupleDeclarationExpression : Expression
    {
        public List<Expression> members { get; protected set; }
        public TupleDeclarationExpression(List<Expression> members)
        {
            this.members = members;
        }
    }
}

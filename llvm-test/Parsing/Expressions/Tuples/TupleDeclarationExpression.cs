using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing.Expressions.Tuples
{
    public class TupleDeclarationExpression : Expression
    {
        List<Expression> members;
        public TupleDeclarationExpression(List<Expression> members)
        {
            this.members = members;
        }
    }
}

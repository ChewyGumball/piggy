using llvm_test.Parsing.Expressions.Tuples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing.Expressions.Assignment
{
    public class TupleAssignmentExpression : Expression
    {
        public TupleDeclarationExpression names { get; protected set; }
        public Expression values { get; protected set; }

        public TupleAssignmentExpression(TupleDeclarationExpression names, Expression values)
        {
            this.names = names;
            this.values = values;
        }
    }
}

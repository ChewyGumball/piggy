using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing.Expressions.Assignment
{
    public class VariableAssignmentExpression : Expression
    {
        public String name { get; protected set; }
        public Expression value { get; protected set; }

        public VariableAssignmentExpression(String name, Expression value)
        {
            this.name = name;
            this.value = value;
        }
    }
}

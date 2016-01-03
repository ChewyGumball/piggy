using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing.Expressions.Names
{
    public class VariableReferenceExpression : Expression
    {
        public String name { get; protected set; }
        public VariableReferenceExpression(String variableName)
        {
            name = variableName;
        }
    }
}

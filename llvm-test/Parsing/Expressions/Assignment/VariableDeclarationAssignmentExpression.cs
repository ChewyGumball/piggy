using llvm_test.Parsing.Expressions.Names;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing.Expressions.Assignment
{
    public class VariableDeclarationAssignmentExpression : Expression
    {
        public VariableDeclarationExpression declaration { get; protected set; }
        public Expression value { get; protected set; }
        public VariableDeclarationAssignmentExpression(VariableDeclarationExpression declaration, Expression value)
        {
            this.declaration = declaration;
            this.value = value;
        }
    }
}

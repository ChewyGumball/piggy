using llvm_test.Parsing.Expressions.Names;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing.Expressions.Assignment
{
    public class VariableDeclarationAssignmentExpression : Expression, IVisibilityExpression
    {
        public VariableDeclarationExpression declaration { get; protected set; }
        public Expression value { get; protected set; }
        public Visibility visibility { get; set; }

        public VariableDeclarationAssignmentExpression(VariableDeclarationExpression declaration, Expression value, Visibility visibility = Visibility.None)
        {
            this.declaration = declaration;
            this.value = value;
            this.visibility = visibility;
        }
    }
}

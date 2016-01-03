using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing.Expressions.Names
{
    public class VariableDeclarationExpression : Expression
    {
        Expression name, typeName;
        public VariableDeclarationExpression(Expression name, Expression type)
        {
            this.name = name;
            this.typeName = type;
        }
    }
}

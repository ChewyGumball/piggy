using llvm_test.Parsing.Expressions.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing.Expressions.Names
{
    public class VariableDeclarationExpression : Expression
    {
        public String name { get; protected set; }
        public TypeName typeName { get; protected set; }
        public VariableDeclarationExpression(String name, TypeName type)
        {
            this.name = name;
            this.typeName = type;
        }
    }
}

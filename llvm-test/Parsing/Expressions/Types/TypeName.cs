using llvm_test.Parsing.Expressions.Names;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing.Expressions.Types
{
    public class TypeName : Expression
    {
        public String name;
        public TypeName(String name) { this.name = name; }
        public TypeName(VariableReferenceExpression name) { this.name = name.name; }

        public override string print(int indentation = 0)
        {
            return name;
        }
    }
}

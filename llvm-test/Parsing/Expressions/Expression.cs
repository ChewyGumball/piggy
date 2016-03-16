using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing.Expressions
{
    public abstract class Expression : Visitable<Expression>
    {
        public void accept(Visitor<Expression> visitor)
        {
            visitor.visit((dynamic)this);
        }
        public abstract String print(int indentation = 0);
    }
}

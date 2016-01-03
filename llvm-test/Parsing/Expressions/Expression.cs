using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing.Expressions
{
    public abstract class Expression : Visitable<Expression>
    {
        public ExpressionType type { get; protected set; }
        public void accept(Visitor<Expression> visitor)
        {
            visitor.visit((dynamic)this);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing.Expressions.Names
{
    public class ClassDefinitionExpression : Expression, IVisibilityExpression
    {
        public Visibility visibility { get; set; }
        public String name { get; protected set; }
        public List<Expression> members { get; private set; }

        public ClassDefinitionExpression(String name, List<Expression> members, Visibility visibility = Visibility.None)
        {
            this.name = name;
            this.members = members;
            this.visibility = visibility;
        }
    }
}

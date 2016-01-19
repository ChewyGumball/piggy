using llvm_test.Parsing.Expressions.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing.Expressions.Names
{
    public class GenericClassDefinitionExpression : ClassDefinitionExpression
    {
        public List<TypeName> genericTypes { get; protected set; }
        public GenericClassDefinitionExpression(GenericTypeName name, List<Expression> members, Visibility visibility = Visibility.None)
            :base(name.name, members, visibility)
        {
            genericTypes = name.genericTypes;
        }
    }
}

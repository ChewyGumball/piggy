using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing.Expressions.Types
{
    public class GenericTypeName : TypeName
    {
        public List<TypeName> genericTypes { get; protected set; }
        public GenericTypeName(String name, List<TypeName> genericTypes) : base(name)
        {
            this.genericTypes = genericTypes;
        }
    }
}

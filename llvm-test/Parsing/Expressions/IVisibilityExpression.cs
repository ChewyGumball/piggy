using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing.Expressions
{
    public enum Visibility { Public, Private, Protected, None }
    interface IVisibilityExpression
    {
        Visibility visibility { get; set; }
    }
}

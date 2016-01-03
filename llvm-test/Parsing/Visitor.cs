using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing
{
    public interface Visitor<T>
    {
        bool visit(T thing);
    }
}

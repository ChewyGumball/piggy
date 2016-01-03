using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing
{
    public interface Visitable<T>
    {
        void accept(Visitor<T> visitor);
    }
}

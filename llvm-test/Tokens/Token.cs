using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Tokens
{
    public class Token<T> : TokenBase
    {
        public T value { get; protected set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Tokens
{
    public class NumberToken : Token<long>
    {
        public NumberToken(String value)
        {
            long numberValue;
            valid = long.TryParse(value, out numberValue);
            this.value = numberValue;
        }
    }
}

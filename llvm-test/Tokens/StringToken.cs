using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Tokens
{
    public class StringToken : Token<String>
    {

        public StringToken(String value)
        {
            valid = true;
            this.value = value;
        }
    }
}

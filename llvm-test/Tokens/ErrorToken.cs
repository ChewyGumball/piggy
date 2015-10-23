using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Tokens
{
    public class ErrorToken : Token<String>
    {
        public ErrorToken(String message)
        {
            valid = true;
            value = message;
        }
    }
}

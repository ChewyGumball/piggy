using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Tokens
{
    public class ErrorToken : Token
    {
        public ErrorToken(String message) : base(TokenType.Error, message)
        {
            valid = true;
        }
    }
}

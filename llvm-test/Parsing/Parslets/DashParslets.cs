﻿using llvm_test.Parsing.Expressions;
using llvm_test.Parsing.Expressions.Names;
using llvm_test.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing.Parslets
{
    public class DashParslets
    {
        public static Expression router(Parser p, Expression left, Token t)
        {
            if(p.peek(TokenType.RightAngleBracket))
            {
                return new VariableDeclarationExpression(left, p.parseExpression(0));
            }
            else
            {
                return ArithmeticParslets.subtraction(p, left, t);
            }
        }
    }
}

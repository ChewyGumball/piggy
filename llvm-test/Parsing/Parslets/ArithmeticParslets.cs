using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using llvm_test.Tokens;
using llvm_test.Parsing.Expressions;
using llvm_test.Parsing.Expressions.Arithmetic;

namespace llvm_test.Parsing.Parslets
{
    class ArithmeticParslets
    {
        public static Expression negative(Parser p, Token t)
        {            
            return new NegativeExpression(p.parseExpression(t.precedence));
        }
        public static Expression division(Parser p, Expression left, Token t)
        {
            return new DivisionExpression(left, p.parseExpression(t.precedence));
        }
        public static Expression multiplication(Parser p, Expression left, Token t)
        {
            return new MultiplicationExpression(left, p.parseExpression(t.precedence));
        }
        public static Expression addition(Parser p, Expression left, Token t)
        {
            return new AdditionExpression(left, p.parseExpression(t.precedence));
        }
        public static Expression subtraction(Parser p, Expression left, Token t)
        {
            return new SubtractionExpression(left, p.parseExpression(t.precedence));
        }
    }
}

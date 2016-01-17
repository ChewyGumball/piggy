using llvm_test.Parsing.Expressions;
using llvm_test.Parsing.Expressions.Functions;
using llvm_test.Parsing.Expressions.Names;
using llvm_test.Parsing.Expressions.Tuples;
using llvm_test.Parsing.Expressions.Types;
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
            if (p.peek(TokenType.RightAngleBracket))
            {
                if (left is VariableReferenceExpression)
                {
                    p.skip(TokenType.RightAngleBracket);
                    Expression type = p.parseExpression(Token.precedenceTable[TokenType.Equals] + 1);
                    if (type is VariableReferenceExpression)
                    {
                        if (p.peek(TokenType.LeftAngleBracket))
                        {
                            GenericTypeName typeName = BracketParslets.parseGenericTypeName(p, (type as VariableReferenceExpression).name);
                            return new VariableDeclarationExpression((left as VariableReferenceExpression).name, typeName);
                        }
                        else
                        {
                            return new VariableDeclarationExpression((left as VariableReferenceExpression).name, new TypeName((type as VariableReferenceExpression).name));
                        }
                    }
                    else
                    {
                        throw new Exception("Variable declaration has an invalid type!");
                    }
                }
                else if(left is TupleDeclarationExpression)
                {
                    p.skip(TokenType.RightAngleBracket);
                    String returnType = p.consume().value;
                    Expression body = p.parseExpression(0);
                    if (body is BlockExpression)
                    {
                        return new AnonymousFunctionExpression(left as TupleDeclarationExpression, returnType, body as BlockExpression);
                    }
                    else
                    {
                        throw new Exception("Function has invalid body!");
                    }
                }
                else
                {
                    throw new Exception("Can't tell what should happen after this dash!");
                }
            }
            else
            {
                return ArithmeticParslets.subtraction(p, left, t);
            }
        }
    }
}

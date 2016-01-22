using llvm_test.Parsing.Expressions;
using llvm_test.Parsing.Expressions.Functions;
using llvm_test.Parsing.Expressions.Logical;
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
    class BracketParslets
    {
        public static Expression roundBacketRouter(Parser p, Token t)
        {
            Expression innerExpression = p.parseExpression(0);
            if(p.peek(TokenType.Comma))
            {
                if(innerExpression is VariableDeclarationExpression)
                {
                    innerExpression = NameParslets.tupleDeclaration(p, innerExpression);
                }
                else
                {
                    innerExpression = tupleDefinition(p, innerExpression);
                }
            }
            p.skip(TokenType.RightRoundBracket);
            return innerExpression;
        }

        public static Expression angleBracketRouter(Parser p, Expression left, Token t)
        {
            Expression innerExpression = p.parseExpression(t.precedence);
            if (t.preceedingWhiteSpace == 0 && (p.peek(TokenType.Comma) || p.peek(TokenType.RightAngleBracket) || p.peek(TokenType.LeftAngleBracket)))
            {
                if (innerExpression is VariableReferenceExpression)
                {
                    return parseGenericTypeName(p, (left as VariableReferenceExpression).name, (innerExpression as VariableReferenceExpression).name);
                }
                else
                {
                    throw new Exception("Improper Generic Type expression!");
                }
            }
            else
            {
                return new LessThanExpression(left, innerExpression);
            }
        }

        private static TupleDefinitionExpression tupleDefinition(Parser p, Expression left)
        {
            List<Expression> members = new List<Expression> { left };
            while(!p.peek(TokenType.RightRoundBracket))
            {
                if (p.skip(TokenType.Comma))
                {
                    members.Add(p.parseExpression(0));
                }
                else
                {
                    throw new Exception("Invalid tuple definition!");
                }
            }

            return new TupleDefinitionExpression(members);
        }

        public static BlockExpression block(Parser p, Token t)
        {
            List<Expression> expressions = new List<Expression>();
            while(!p.peek(TokenType.RightCurlyBracket))
            {
                expressions.Add(p.parseStatement());
            }

            return new BlockExpression(expressions);
        }

        public static Expression functionDeclaration(Parser p, Expression left, Token t)
        {
            if (left is VariableReferenceExpression)
            {
                Expression arguments = roundBacketRouter(p, t);
                if (arguments is TupleDeclarationExpression)
                {
                    p.skip(TokenType.Dash);
                    p.skip(TokenType.RightAngleBracket);
                    TypeName returnType = DashParslets.getTypeName(p);
                    Expression body = p.parseExpression(0);
                    if (body is BlockExpression)
                    {
                        return new FunctionExpression((left as VariableReferenceExpression).name, 
                                                      arguments as TupleDeclarationExpression, 
                                                      returnType, 
                                                      body as BlockExpression);
                    }
                    else
                    {
                        throw new Exception("Function has invalid body!");
                    }
                }
                else
                {
                    throw new Exception("Function has invalid argument list!");
                }
            }
            else
            {
                throw new Exception("Function has invalid name!");
            }
        }

        public static GenericTypeName parseGenericTypeName(Parser p, String typeName, String firstGenericType = null)
        {
            List<TypeName> genericTypes = new List<TypeName>();
            if(firstGenericType != null)
            {
                if (p.peek(TokenType.LeftAngleBracket))
                {
                    if(p.peekWhitespace(0))
                    {
                        genericTypes.Add(parseGenericTypeName(p, firstGenericType));
                    }
                    else
                    {
                        throw new Exception("Improper Generic Type expression!");
                    }
                }
                else
                {
                    genericTypes.Add(new TypeName(firstGenericType));
                }
            }

            if(p.peek(TokenType.RightAngleBracket) && firstGenericType == null)
            {
                throw new Exception("Generic type declaration must have a non empty type list!");
            }

            do
            {
                p.skip(TokenType.LeftAngleBracket);
                Token innerToken = p.consume();
                String innerTypeName = innerToken.value;
                if (p.peek(TokenType.LeftAngleBracket))
                {
                    if (p.peekWhitespace(0))
                    {
                        genericTypes.Add(parseGenericTypeName(p, innerTypeName));
                    }
                    else
                    {
                        throw new Exception("Improper Generic Type expression! [Line: " + innerToken.lineNumber + ", Column: " + innerToken.columnNumber + "]");
                    }
                }
                else if (p.peek(TokenType.Comma) || p.peek(TokenType.RightAngleBracket))
                {
                    genericTypes.Add(new TypeName(innerTypeName));
                }
                else
                {
                    throw new Exception("Generic type is not a name!");
                }
            } while (p.peek(TokenType.Comma));

            if (p.skip(TokenType.RightAngleBracket))
            {
                return new GenericTypeName(typeName, genericTypes);
            }
            else
            {
                throw new Exception("Generic type must end in a '>'!");
            }
        }
    }
}

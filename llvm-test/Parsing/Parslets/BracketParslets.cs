﻿using llvm_test.Parsing.Expressions;
using llvm_test.Parsing.Expressions.Functions;
using llvm_test.Parsing.Expressions.Names;
using llvm_test.Parsing.Expressions.Tuples;
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
                    String returnType = p.consume().value;
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
    }
}

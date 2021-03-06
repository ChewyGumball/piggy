﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using llvm_test.Tokens;
using llvm_test.Parsing.Expressions;
using llvm_test.Parsing.Parslets;

namespace llvm_test.Parsing
{
    public class Parser
    {
        Lexer tokenStream;

        private static Dictionary<TokenType, Func<Parser, Token, Expression>> prefixExpressions = new Dictionary<TokenType, Func<Parser, Token, Expression>>
        {
            { TokenType.Dash, ArithmeticParslets.negative },
            { TokenType.Tilde, BitwiseParslets.bitwiseNot },
            { TokenType.Exclaimation, LogicalParslets.logicalNot },
            { TokenType.Number, LiteralParslets.numberLiteral },
            { TokenType.DoubleQuote, LiteralParslets.stringLiteral },
            { TokenType.Boolean, LiteralParslets.booleanLiteral },
            { TokenType.Name, NameParslets.router },
            { TokenType.LeftRoundBracket, BracketParslets.roundBacketRouter },
            { TokenType.LeftCurlyBracket, BracketParslets.block }
        };

        private static Dictionary<TokenType, Func<Parser, Expression, Token, Expression>> infixExpressions = new Dictionary<TokenType, Func<Parser, Expression, Token, Expression>>
        {
            { TokenType.Star, ArithmeticParslets.multiplication },
            { TokenType.Slash, ArithmeticParslets.division },
            { TokenType.Plus, ArithmeticParslets.addition },
            { TokenType.Dash, DashParslets.router },
            { TokenType.Equals, AssignmentParslets.assignment },
            { TokenType.LeftRoundBracket, BracketParslets.functionDeclaration },
            { TokenType.RightAngleBracket, LogicalParslets.greaterThan },
            { TokenType.LeftAngleBracket, BracketParslets.angleBracketRouter }
        };

        public Parser(Lexer lexer)
        {
            tokenStream = lexer;
        }

        public List<Expression> parse()
        {
            List<Expression> expressions = new List<Expression>();
            while(tokenStream.peek().type != TokenType.End)
            {
                expressions.Add(parseExpression(0));
                Token t = tokenStream.peek();
            }

            return expressions;
        }

        public Expression parseExpression(int currentPrecedence)
        {
            Token nextToken = tokenStream.consume();
            Expression left = prefixExpressions[nextToken.type](this, nextToken);

            Token lookAhead = tokenStream.peek();
            while(currentPrecedence < lookAhead.precedence)
            {
                nextToken = tokenStream.consume();
                left = infixExpressions[nextToken.type](this, left, nextToken);
                lookAhead = tokenStream.peek();
            }

            return left;
        }

        public Expression parseStatement()
        {
            Expression expression = parseExpression(0);
            skip(TokenType.SemiColon);
            return expression;
        }

        public bool peek(TokenType t)
        {
            return tokenStream.peek().type == t;
        }

        public bool peekWhitespace(int requiredWhiteSpace)
        {
            return tokenStream.peek().preceedingWhiteSpace == requiredWhiteSpace;
        }

        public bool skipIf(TokenType t)
        {
            bool skipped = peek(t);
            if(skipped)
            {
                tokenStream.consume();
            }

            return skipped;
        }

        public bool skip(TokenType t)
        {
            return tokenStream.consume().type == t;
        }

        public Token consume()
        {
            return tokenStream.consume();
        }
    }
}

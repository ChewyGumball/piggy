using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using llvm_test.Tokens;
using llvm_test.Parsing.Expressions;
using llvm_test.Parsing.Expressions.Names;
using llvm_test.Parsing.Expressions.Tuples;

namespace llvm_test.Parsing.Parslets
{
    class NameParslets
    {
        private static Dictionary<String, Func<Parser, Token, Expression>> keywordParslets = new Dictionary<string, Func<Parser, Token, Expression>>
        {
            { "while", whileLoop }
        };

        public static Expression router(Parser p, Token t)
        {
            if(keywordParslets.ContainsKey(t.value))
            {
                return keywordParslets[t.value](p, t);
            }
            else
            {
                return new VariableReferenceExpression(t.value);
            }
        }

        private static WhileLoopExpression whileLoop(Parser p, Token t)
        {
            Expression condition = p.parseExpression(0);
            Expression loop = p.parseExpression(0);

            if(loop is BlockExpression)
            {
                return new WhileLoopExpression(condition, loop as BlockExpression);
            }
            else
            {
                throw new Exception("No block for while loop");
            }
        }

        public static TupleDeclarationExpression tupleDeclaration(Parser p, Expression left)
        {
            List<Expression> members = new List<Expression> { left };
            while(!p.peek(TokenType.RightRoundBracket))
            {
                p.skip(TokenType.Comma);
                members.Add(p.parseExpression(0));
            }

            return new TupleDeclarationExpression(members);
        }
    }
}

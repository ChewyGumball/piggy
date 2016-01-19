using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using llvm_test.Tokens;
using llvm_test.Parsing.Expressions;
using llvm_test.Parsing.Expressions.Names;
using llvm_test.Parsing.Expressions.Tuples;
using llvm_test.Parsing.Expressions.Types;
using llvm_test.Parsing.Expressions.Assignment;

namespace llvm_test.Parsing.Parslets
{
    class NameParslets
    {
        private static Dictionary<String, Func<Parser, Token, Expression>> keywordParslets = new Dictionary<string, Func<Parser, Token, Expression>>
        {
            { "while", whileLoop },
            { "class",  classParser },
            { "public", visibilityParser },
            { "protected", visibilityParser },
            { "private", visibilityParser },
            { "new", objectInstantiation }
        };

        private static Expression objectInstantiation(Parser arg1, Token arg2)
        {
            throw new NotImplementedException();
        }

        private static Dictionary<String, Visibility> visibility = new Dictionary<string, Visibility>
        {
            {"public", Visibility.Public },
            {"protected", Visibility.Protected },
            {"private", Visibility.Private }
        };

        private static Expression visibilityParser(Parser p, Token t)
        {
            Expression affected = p.parseExpression(0);
            if (affected is IVisibilityExpression)
            {
                (affected as IVisibilityExpression).visibility = visibility[t.value];
                return affected;
            }
            else
            {
                throw new Exception("The visibility modifier '" + t.value + "' can not be applied to the next expression.");
            }
        }

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
            if (left is VariableDeclarationExpression)
            {
                List<VariableDeclarationExpression> members = new List<VariableDeclarationExpression> { left as VariableDeclarationExpression };
                while (!p.peek(TokenType.RightRoundBracket))
                {
                    p.skip(TokenType.Comma);
                    Expression nextExpression = p.parseExpression(0);
                    if (nextExpression is VariableDeclarationExpression)
                    {
                        members.Add(nextExpression as VariableDeclarationExpression);
                    }
                    else
                    {
                        throw new Exception("Tuple declarations can only have name -> type pairs in them.");
                    }
                }

                return new TupleDeclarationExpression(members);
            }
            else
            {
                throw new Exception("Tuple declarations can only have name -> type pairs in them.");
            }
        }

        private static ClassDefinitionExpression classParser(Parser p, Token t)
        {
            Expression name = p.parseExpression(0);
            if((name is VariableReferenceExpression || name is GenericTypeName))
            {
                if (p.skip(TokenType.LeftCurlyBracket))
                {
                    List<Expression> classMembers = new List<Expression>();
                    while(!p.peek(TokenType.RightCurlyBracket))
                    {
                        Expression member = p.parseExpression(0);
                        classMembers.Add(member);
                        if(p.peek(TokenType.SemiColon) && (member is VariableDeclarationExpression || member is VariableDeclarationAssignmentExpression))
                        {
                            p.skip(TokenType.SemiColon);
                        }
                    }

                    p.skip(TokenType.RightCurlyBracket);
                    if(name is GenericTypeName)
                    {
                        return new GenericClassDefinitionExpression(name as GenericTypeName, classMembers);
                    }
                    else
                    {
                        return new ClassDefinitionExpression((name as VariableReferenceExpression).name, classMembers);
                    }
                }
                throw new Exception("Invalid class definition.");
            }
            else
            {
                throw new Exception("Invalid class name.");
            }
        }
    }
}

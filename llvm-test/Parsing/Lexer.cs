using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

using llvm_test.Tokens;

namespace llvm_test
{
    public class Lexer
    {
        private static Regex digitChecker = new Regex(@"^\d+(\.\d+)?$");

        private StreamReader input;
        private Queue<Token> tokens = new Queue<Token>();


        public Lexer(Stream input)
        {
            this.input = new StreamReader(input, Encoding.UTF8);
        }

        public Token consume()
        {
            if (tokens.Count == 0)
            {
                processStream();
            }

            return tokens.Dequeue();
        }

        public Token peek(int lookAhead = 0)
        {
            while(tokens.Count <= lookAhead)
            {
                processStream();
            }

            return tokens.ElementAt(lookAhead);
        }

        private void processStream()
        {
            chewWhiteSpace();
            bool tokenFormed = false;
            StringBuilder builder = new StringBuilder();
            while (!tokenFormed)
            {
                int nextCharAsInt = input.Peek();
                if (nextCharAsInt == -1)
                {
                    tokenFormed = true;
                }
                else
                {
                    char nextChar = (char)nextCharAsInt;
                    if (Token.SymbolTable.ContainsKey(Char.ToString(nextChar)))
                    {
                        if (builder.Length == 0)
                        {
                            builder.Append(nextChar);
                            input.Read();
                        }
                        tokenFormed = true;
                    }
                    else if (Char.IsWhiteSpace(nextChar))
                    {
                        tokenFormed = true;
                    }
                    else
                    {
                        builder.Append(nextChar);
                        input.Read();
                    }
                }
            }
            createToken(builder);
        }
        
        private void chewWhiteSpace()
        {
            while(Char.IsWhiteSpace((char)input.Peek()))
            {
                input.Read();
            }
        }

        public void createToken(StringBuilder builder)
        {
            String tokenValue = builder.ToString();
            if (builder.Length > 0)
            {
                tokenValue = tokenValue.Trim();
                if (Token.SymbolTable.ContainsKey(tokenValue))
                {
                    tokens.Enqueue(new Token(Token.SymbolTable[tokenValue], tokenValue));
                }
                else if (digitChecker.IsMatch(tokenValue))
                {
                    tokens.Enqueue(new Token(TokenType.Number, tokenValue));
                }
                else if(tokenValue == "true" || tokenValue == "false")
                {
                    tokens.Enqueue(new Token(TokenType.Boolean, tokenValue));
                }
                else
                {
                    tokens.Enqueue(new Token(TokenType.Name, tokenValue));
                }
            }
            else
            {
                tokens.Enqueue(new Token(TokenType.End, ""));
            }
        }
    }
}

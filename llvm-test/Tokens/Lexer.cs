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
        int lineNumber = 1;
        int columnNumber = 1;


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
            int chewedWhiteSpace = chewWhiteSpace();
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
            createToken(builder, chewedWhiteSpace);
            columnNumber += builder.Length;
        }
        
        private int chewWhiteSpace()
        {
            int chewedWhiteSpace = 0;
            char nextCharacter = (char)input.Peek();
            while(Char.IsWhiteSpace(nextCharacter))
            {
                input.Read();
                chewedWhiteSpace++;
                columnNumber++;

                if (nextCharacter == '\n')
                {
                    lineNumber++;
                    columnNumber = 1;
                }

                nextCharacter = (char)input.Peek();
            }
            return chewedWhiteSpace;
        }

        public void createToken(StringBuilder builder, int preceedingWhiteSpace)
        {
            String tokenValue = builder.ToString();
            if (builder.Length > 0)
            {
                if (Token.SymbolTable.ContainsKey(tokenValue))
                {
                    tokens.Enqueue(new Token(Token.SymbolTable[tokenValue], tokenValue, lineNumber, columnNumber, preceedingWhiteSpace));
                }
                else if (digitChecker.IsMatch(tokenValue))
                {
                    tokens.Enqueue(new Token(TokenType.Number, tokenValue, lineNumber, columnNumber, preceedingWhiteSpace));
                }
                else if(tokenValue == "true" || tokenValue == "false")
                {
                    tokens.Enqueue(new Token(TokenType.Boolean, tokenValue, lineNumber, columnNumber, preceedingWhiteSpace));
                }
                else
                {
                    tokens.Enqueue(new Token(TokenType.Name, tokenValue, lineNumber, columnNumber, preceedingWhiteSpace));
                }
            }
            else
            {
                tokens.Enqueue(new Token(TokenType.End, "", lineNumber, columnNumber, preceedingWhiteSpace));
            }
        }
    }
}

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
        private bool inStringConstant = false;
        private Queue<TokenBase> tokens = new Queue<TokenBase>();


        public Lexer(Stream input)
        {
            this.input = new StreamReader(input, Encoding.UTF8);
        }

        public TokenBase next()
        {
            if (tokens.Count == 0)
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
                        if (nextChar == '"')
                        {
                            if (builder.Length == 0)
                            {
                                readStringTokens();
                            }
                            tokenFormed = true;
                        }
                        else
                        {
                            if (SymbolToken.SymbolTable.ContainsKey(Char.ToString(nextChar)))
                            {
                                if (builder.Length == 0)
                                {
                                    builder.Append(nextChar);
                                    input.Read();
                                }
                                tokenFormed = true;
                            }
                            else
                            {
                                builder.Append(nextChar);
                                input.Read();
                            }
                        }
                    }
                }
                createToken(builder);
            }

            return tokens.Dequeue();
        }

        private void readStringTokens()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append((char)input.Read());
            createToken(builder);
            builder.Clear();

            inStringConstant = true;
            int nextChar = input.Peek();
            while (nextChar != -1 && (char)nextChar != '"')
            {
                builder.Append((char)input.Read());
                nextChar = input.Peek();
            }
            createToken(builder);
            builder.Clear();
            inStringConstant = false;
            if (nextChar == -1)
            {
                tokens.Enqueue(new ErrorToken("Missing end quote for string."));
            }
            else
            {
                builder.Append((char)input.Read());
                createToken(builder);
            }
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
            if (inStringConstant)
            {
                inStringConstant = false;
                tokens.Enqueue(new StringToken(tokenValue));
            }
            else if (builder.Length > 0)
            {
                if (SymbolToken.SymbolTable.ContainsKey(tokenValue))
                {
                    tokens.Enqueue(new SymbolToken(tokenValue));
                }
                else if (digitChecker.IsMatch(tokenValue))
                {
                    tokens.Enqueue(new NumberToken(tokenValue));
                }
                else
                {
                    tokens.Enqueue(new ErrorToken("Uknown errors"));
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using llvm_test.Parsing;
using llvm_test.Tokens;
using llvm_test.Parsing.Expressions;

namespace llvm_test
{
    class Compiler
    {
        static void Main(string[] args)
        {
            String filename = args[0];
            File.OpenText(filename);
            String fileText = File.ReadAllText(filename);

            Parser parser = new Parser(new Lexer(new MemoryStream(Encoding.UTF8.GetBytes(fileText))));

            foreach(Expression e in parser.parse())
            {
                Console.WriteLine(e.print());
            }
        }
    }
}

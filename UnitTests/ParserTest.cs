using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using llvm_test;
using System.Text;
using llvm_test.Parsing;
using llvm_test.Parsing.Expressions;

namespace UnitTests
{
    [TestClass]
    public class ParserTest
    {
        private Lexer makeLexer(String input)
        {
            return new Lexer(new MemoryStream(Encoding.UTF8.GetBytes(input)));
        }

        private Parser makeParser(String input)
        {
            return new Parser(makeLexer(input));
        }

        [TestMethod]
        public void SimpleAddition()
        {
            Expression a = makeParser("a + b").parseExpression(0);
            Console.WriteLine(a.ToString());
        }

        [TestMethod]
        public void AdditionAndSubtractionWithBrackets()
        {
            Expression a = makeParser("a + b + (1 - g)").parseExpression(0);
            Console.WriteLine(a.ToString());
        }

        [TestMethod]
        public void BEDMASTest()
        {
            Expression a = makeParser("a + b * g / p - y").parseExpression(0);
            Console.WriteLine(a.ToString());
        }

        [TestMethod]
        public void WhileLoop()
        {
            Expression a = makeParser("while(true){a + b - c;}").parseExpression(0);
            Console.WriteLine(a.ToString());
        }

        [TestMethod]
        public void TupleDeclaration()
        {
            Expression a = makeParser("(a -> int, b -> FakeClass1, c-> FakeClass2, d -> String)").parseExpression(0);
            Console.WriteLine(a.ToString());
        }

        [TestMethod]
        public void TupleDefinition()
        {
            Expression a = makeParser("(a, b, 4+6,\"dsasdasd\", a - g + p * t)").parseExpression(0);
            Console.WriteLine(a.ToString());
        }
    }
}

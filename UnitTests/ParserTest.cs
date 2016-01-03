using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using llvm_test;
using System.Text;
using llvm_test.Parsing;
using llvm_test.Parsing.Expressions;
using llvm_test.Parsing.Expressions.Arithmetic;
using llvm_test.Parsing.Expressions.Names;
using llvm_test.Parsing.Expressions.Literal;

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

        private Expression parse(String input)
        {
            return makeParser(input).parseExpression(0);
        }

        private T assertTypeAndCast<T>(Expression a) where T : Expression
        {
            Assert.IsInstanceOfType(a, typeof(T));
            return a as T;
        }

        [TestMethod]
        public void SimpleAddition()
        {
            Expression expression = parse("a + b");
            AdditionExpression a = assertTypeAndCast<AdditionExpression>(expression);
            VariableReferenceExpression b = assertTypeAndCast<VariableReferenceExpression>(a.left);
            VariableReferenceExpression c = assertTypeAndCast<VariableReferenceExpression>(a.right);

            Assert.AreEqual(b.name, "a");
            Assert.AreEqual(c.name, "b");
        }

        [TestMethod]
        public void AdditionAndSubtractionWithBrackets()
        {
            Expression expression = parse("a + b + (1 - g)");
            AdditionExpression a = assertTypeAndCast<AdditionExpression>(expression);

            AdditionExpression b = assertTypeAndCast<AdditionExpression>(a.left);
            SubtractionExpression c = assertTypeAndCast<SubtractionExpression>(a.right);

            VariableReferenceExpression d = assertTypeAndCast<VariableReferenceExpression>(b.left);
            VariableReferenceExpression e = assertTypeAndCast<VariableReferenceExpression>(b.right);
            IntegralLiteralExpression f = assertTypeAndCast<IntegralLiteralExpression>(c.left);
            VariableReferenceExpression g = assertTypeAndCast<VariableReferenceExpression>(c.right);

            Assert.AreEqual(d.name, "a");
            Assert.AreEqual(e.name, "b");
            Assert.AreEqual(f.value, 1);
            Assert.AreEqual(g.name, "g");
        }

        [TestMethod]
        public void BEDMASTest()
        {
            Expression expression = parse("a + b * g / p - y");

            SubtractionExpression a = assertTypeAndCast<SubtractionExpression>(expression);
            AdditionExpression b = assertTypeAndCast<AdditionExpression>(a.left);
            VariableReferenceExpression c = assertTypeAndCast<VariableReferenceExpression>(a.right);
            VariableReferenceExpression d = assertTypeAndCast<VariableReferenceExpression>(b.left);
            DivisionExpression e = assertTypeAndCast<DivisionExpression>(b.right);
            MultiplicationExpression f = assertTypeAndCast<MultiplicationExpression>(e.left);
            VariableReferenceExpression g = assertTypeAndCast<VariableReferenceExpression>(e.right);
            VariableReferenceExpression h = assertTypeAndCast<VariableReferenceExpression>(f.left);
            VariableReferenceExpression i = assertTypeAndCast<VariableReferenceExpression>(f.right);

            Assert.AreEqual(c.name, "y");
            Assert.AreEqual(d.name, "a");
            Assert.AreEqual(g.name, "p");
            Assert.AreEqual(h.name, "b");
            Assert.AreEqual(i.name, "g");
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
            Expression a = makeParser("(a, b, 4+6,\"dsas\\\"dasd\", a - g + p * t)").parseExpression(0);
            Console.WriteLine(a.ToString());
        }
    }
}

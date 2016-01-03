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
using llvm_test.Parsing.Expressions.Tuples;

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

        private Expression parseExpression(String input)
        {
            return makeParser(input).parseExpression(0);
        }

        private Expression parseStatement(String input)
        {
            return makeParser(input).parseStatement();
        }

        private T assertTypeAndCast<T>(Expression a) where T : Expression
        {
            Assert.IsInstanceOfType(a, typeof(T));
            return a as T;
        }

        [TestMethod]
        public void SimpleAddition()
        {
            Expression expression = parseExpression("a + b");
            AdditionExpression a = assertTypeAndCast<AdditionExpression>(expression);
            VariableReferenceExpression b = assertTypeAndCast<VariableReferenceExpression>(a.left);
            VariableReferenceExpression c = assertTypeAndCast<VariableReferenceExpression>(a.right);

            Assert.AreEqual(b.name, "a");
            Assert.AreEqual(c.name, "b");
        }

        [TestMethod]
        public void AdditionAndSubtractionWithBrackets()
        {
            Expression expression = parseExpression("a + b + (1 - g)");
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
            Expression expression = parseExpression("a + b * g / p - y");

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
            Expression expression = parseExpression("while(true){a + b - c;}");
            WhileLoopExpression a = assertTypeAndCast<WhileLoopExpression>(expression);
            BooleanLiteralExpression condition = assertTypeAndCast<BooleanLiteralExpression>(a.condition);
            Assert.IsTrue(condition.value);

            Assert.AreEqual(1, a.loop.innerExpressions.Count);
            SubtractionExpression b = assertTypeAndCast<SubtractionExpression>(a.loop.innerExpressions[0]);
            AdditionExpression c = assertTypeAndCast<AdditionExpression>(b.left);
            VariableReferenceExpression d = assertTypeAndCast<VariableReferenceExpression>(b.right);
            VariableReferenceExpression e = assertTypeAndCast<VariableReferenceExpression>(c.left);
            VariableReferenceExpression f = assertTypeAndCast<VariableReferenceExpression>(c.right);

            Assert.AreEqual("a", e.name);
            Assert.AreEqual("b", f.name);
            Assert.AreEqual("c", d.name);
        }

        [TestMethod]
        public void TupleDeclaration()
        {
            Expression expression = parseExpression("(a -> int, b -> FakeClass1, c-> FakeClass2, d -> String)");
            TupleDeclarationExpression a = assertTypeAndCast<TupleDeclarationExpression>(expression);

            Assert.AreEqual(4, a.members.Count);

            VariableDeclarationExpression b = assertTypeAndCast<VariableDeclarationExpression>(a.members[0]);
            VariableDeclarationExpression c = assertTypeAndCast<VariableDeclarationExpression>(a.members[1]);
            VariableDeclarationExpression d = assertTypeAndCast<VariableDeclarationExpression>(a.members[2]);
            VariableDeclarationExpression e = assertTypeAndCast<VariableDeclarationExpression>(a.members[3]);

            Assert.AreEqual(b.name, "a");
            Assert.AreEqual(b.typeName, "int");

            Assert.AreEqual(c.name, "b");
            Assert.AreEqual(c.typeName, "FakeClass1");

            Assert.AreEqual(d.name, "c");
            Assert.AreEqual(d.typeName, "FakeClass2");

            Assert.AreEqual(e.name, "d");
            Assert.AreEqual(e.typeName, "String");
        }

        [TestMethod]
        public void VariableDeclaration()
        {
            Expression expression = parseStatement("a -> int;");

            VariableDeclarationExpression a = assertTypeAndCast<VariableDeclarationExpression>(expression);
            Assert.AreEqual(a.name, "a");
            Assert.AreEqual(a.typeName, "int");
        }

        [TestMethod]
        public void TupleDefinition()
        {
            Expression expression = parseExpression("(a, b, 4+6,\"dsas\\\"dasd\", a - g + p * t)");
            TupleDefinitionExpression a = assertTypeAndCast<TupleDefinitionExpression>(expression);

            Assert.AreEqual(a.members.Count, 5);

            VariableReferenceExpression b = assertTypeAndCast<VariableReferenceExpression>(a.members[0]);
            VariableReferenceExpression c = assertTypeAndCast<VariableReferenceExpression>(a.members[1]);
            AdditionExpression z = assertTypeAndCast<AdditionExpression>(a.members[2]);
            StringLiteralExpression d = assertTypeAndCast<StringLiteralExpression>(a.members[3]);
            AdditionExpression e = assertTypeAndCast<AdditionExpression>(a.members[4]);

            Assert.AreEqual(b.name, "a");
            Assert.AreEqual(c.name, "b");

            IntegralLiteralExpression y = assertTypeAndCast<IntegralLiteralExpression>(z.left);
            IntegralLiteralExpression x = assertTypeAndCast<IntegralLiteralExpression>(z.right);
            Assert.AreEqual(4, y.value);
            Assert.AreEqual(6, x.value);

            Assert.AreEqual(d.value, "dsas\"dasd");

            SubtractionExpression f = assertTypeAndCast<SubtractionExpression>(e.left);
            MultiplicationExpression g = assertTypeAndCast<MultiplicationExpression>(e.right);

            VariableReferenceExpression h = assertTypeAndCast<VariableReferenceExpression>(f.left);
            VariableReferenceExpression i = assertTypeAndCast<VariableReferenceExpression>(f.right);
            VariableReferenceExpression j = assertTypeAndCast<VariableReferenceExpression>(g.left);
            VariableReferenceExpression k = assertTypeAndCast<VariableReferenceExpression>(g.right);

            Assert.AreEqual(h.name, "a");
            Assert.AreEqual(i.name, "g");
            Assert.AreEqual(j.name, "p");
            Assert.AreEqual(k.name, "t");
        }
    }
}

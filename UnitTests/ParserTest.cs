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
using llvm_test.Parsing.Expressions.Assignment;
using llvm_test.Parsing.Expressions.Functions;
using llvm_test.Parsing.Expressions.Types;

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

            Assert.AreEqual("a", a.members[0].name);
            Assert.AreEqual("int", a.members[0].typeName.name);
            Assert.IsNotInstanceOfType(a.members[0].typeName, typeof(GenericTypeName));
            Assert.AreEqual("b", a.members[1].name);
            Assert.AreEqual("FakeClass1", a.members[1].typeName.name);
            Assert.IsNotInstanceOfType(a.members[1].typeName, typeof(GenericTypeName));
            Assert.AreEqual("c", a.members[2].name);
            Assert.AreEqual("FakeClass2", a.members[2].typeName.name);
            Assert.IsNotInstanceOfType(a.members[2].typeName, typeof(GenericTypeName));
            Assert.AreEqual("d", a.members[3].name);
            Assert.AreEqual("String", a.members[3].typeName.name);
            Assert.IsNotInstanceOfType(a.members[3].typeName, typeof(GenericTypeName));
        }

        [TestMethod]
        public void VariableDeclaration()
        {
            Expression expression = parseStatement("a -> int;");

            VariableDeclarationExpression a = assertTypeAndCast<VariableDeclarationExpression>(expression);
            Assert.AreEqual(a.name, "a");
            Assert.AreEqual(a.typeName.name, "int");
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

        [TestMethod]
        public void VariableAssignment()
        {
            Expression expression = parseStatement("a = 4;");
            VariableAssignmentExpression a = assertTypeAndCast<VariableAssignmentExpression>(expression);
            Assert.AreEqual("a", a.name);
            IntegralLiteralExpression b = assertTypeAndCast<IntegralLiteralExpression>(a.value);
            Assert.AreEqual(4, b.value);
        }

        [TestMethod]
        public void VariableDeclarationAssignment()
        {
            Expression expression = parseStatement("a -> int = 4 + p;");
            VariableDeclarationAssignmentExpression a = assertTypeAndCast<VariableDeclarationAssignmentExpression>(expression);
            Assert.AreEqual("a", a.declaration.name);
            Assert.AreEqual("int", a.declaration.typeName.name);

            AdditionExpression b = assertTypeAndCast<AdditionExpression>(a.value);
            IntegralLiteralExpression c = assertTypeAndCast<IntegralLiteralExpression>(b.left);
            VariableReferenceExpression d = assertTypeAndCast<VariableReferenceExpression>(b.right);
            Assert.AreEqual(4, c.value);
            Assert.AreEqual("p", d.name);
        }

        [TestMethod]
        public void TupleAssignment()
        {
            Expression expression = parseStatement("(a -> int, b -> FakeClass) = (4, p);");

            TupleAssignmentExpression a = assertTypeAndCast<TupleAssignmentExpression>(expression);
            Assert.AreEqual(2, a.names.members.Count);

            Assert.AreEqual("a", a.names.members[0].name);
            Assert.AreEqual("int", a.names.members[0].typeName.name);
            Assert.IsNotInstanceOfType(a.names.members[0].typeName, typeof(GenericTypeName));

            Assert.AreEqual("b", a.names.members[1].name);
            Assert.AreEqual("FakeClass", a.names.members[1].typeName.name);
            Assert.IsNotInstanceOfType(a.names.members[1].typeName, typeof(GenericTypeName));
            
            TupleDefinitionExpression d = assertTypeAndCast<TupleDefinitionExpression>(a.values);
            Assert.AreEqual(2, d.members.Count);

            IntegralLiteralExpression e = assertTypeAndCast<IntegralLiteralExpression>(d.members[0]);
            VariableReferenceExpression f = assertTypeAndCast<VariableReferenceExpression>(d.members[1]);
            Assert.AreEqual(4, e.value);
            Assert.AreEqual("p", f.name);
        }

        [TestMethod]
        public void FunctionDefinition()
        {
            Expression expression = parseExpression("foo(a -> int, b-> Banana) -> Platypus { a + 5; }");
            FunctionExpression a = assertTypeAndCast<FunctionExpression>(expression);

            Assert.AreEqual("foo", a.name);
            Assert.AreEqual("Platypus", a.returnType);
            Assert.AreEqual(2, a.arguments.members.Count);
            Assert.AreEqual(1, a.body.innerExpressions.Count);

            Assert.AreEqual("a", a.arguments.members[0].name);
            Assert.AreEqual("int", a.arguments.members[0].typeName.name);
            Assert.IsNotInstanceOfType(a.arguments.members[0].typeName, typeof(GenericTypeName));

            Assert.AreEqual("b", a.arguments.members[1].name);
            Assert.AreEqual("Banana", a.arguments.members[1].typeName.name);
            Assert.IsNotInstanceOfType(a.arguments.members[1].typeName, typeof(GenericTypeName));

            AdditionExpression d = assertTypeAndCast<AdditionExpression>(a.body.innerExpressions[0]);
            VariableReferenceExpression e = assertTypeAndCast<VariableReferenceExpression>(d.left);
            IntegralLiteralExpression f = assertTypeAndCast<IntegralLiteralExpression>(d.right);

            Assert.AreEqual("a", e.name);
            Assert.AreEqual(5, f.value);
        }

        [TestMethod]
        public void AnonymousFunctionDefinition()
        {
            Expression expression = parseExpression("(a -> int, b-> Banana) -> Platypus { a + 5; }");
            AnonymousFunctionExpression a = assertTypeAndCast<AnonymousFunctionExpression>(expression);
            
            Assert.AreEqual("Platypus", a.returnType);
            Assert.AreEqual(2, a.arguments.members.Count);
            Assert.AreEqual(1, a.body.innerExpressions.Count);

            Assert.AreEqual("a", a.arguments.members[0].name);
            Assert.AreEqual("int", a.arguments.members[0].typeName.name);
            Assert.IsNotInstanceOfType(a.arguments.members[0].typeName, typeof(GenericTypeName));

            Assert.AreEqual("b", a.arguments.members[1].name);
            Assert.AreEqual("Banana", a.arguments.members[1].typeName.name);
            Assert.IsNotInstanceOfType(a.arguments.members[1].typeName, typeof(GenericTypeName));

            AdditionExpression d = assertTypeAndCast<AdditionExpression>(a.body.innerExpressions[0]);
            VariableReferenceExpression e = assertTypeAndCast<VariableReferenceExpression>(d.left);
            IntegralLiteralExpression f = assertTypeAndCast<IntegralLiteralExpression>(d.right);

            Assert.AreEqual("a", e.name);
            Assert.AreEqual(5, f.value);
        }
        
        [TestMethod]
        public void AnonymousFunctionDefinitionAndAssignment()
        {
            Expression expression = parseExpression("anon -> Function<int,Banana, Platypus> = (a -> int, b -> Banana) -> Platypus { a + 5; }");
            VariableDeclarationAssignmentExpression z = assertTypeAndCast<VariableDeclarationAssignmentExpression>(expression);
            Assert.AreEqual("anon", z.declaration.name);
            Assert.AreEqual("Function", z.declaration.typeName.name);

            GenericTypeName y = assertTypeAndCast<GenericTypeName>(z.declaration.typeName);
            Assert.AreEqual(3, y.genericTypes.Count);
            Assert.AreEqual("int", y.genericTypes[0].name);
            Assert.AreEqual("Banana", y.genericTypes[1].name);
            Assert.AreEqual("Platypus", y.genericTypes[2].name);

            AnonymousFunctionExpression a = assertTypeAndCast<AnonymousFunctionExpression>(z.value);

            Assert.AreEqual("Platypus", a.returnType);
            Assert.AreEqual(2, a.arguments.members.Count);
            Assert.AreEqual(1, a.body.innerExpressions.Count);

            Assert.AreEqual("a", a.arguments.members[0].name);
            Assert.AreEqual("int", a.arguments.members[0].typeName.name);
            Assert.IsNotInstanceOfType(a.arguments.members[0].typeName, typeof(GenericTypeName));

            Assert.AreEqual("b", a.arguments.members[1].name);
            Assert.AreEqual("Banana", a.arguments.members[1].typeName.name);
            Assert.IsNotInstanceOfType(a.arguments.members[1].typeName, typeof(GenericTypeName));

            AdditionExpression d = assertTypeAndCast<AdditionExpression>(a.body.innerExpressions[0]);
            VariableReferenceExpression e = assertTypeAndCast<VariableReferenceExpression>(d.left);
            IntegralLiteralExpression f = assertTypeAndCast<IntegralLiteralExpression>(d.right);

            Assert.AreEqual("a", e.name);
            Assert.AreEqual(5, f.value);
        }

        [TestMethod]
        public void GenericTypeTest()
        {
            Expression expression = parseExpression("a -> Banana<t,p,Banana<t,d,g>>");
            VariableDeclarationExpression a = assertTypeAndCast<VariableDeclarationExpression>(expression);
            Assert.AreEqual("a", a.name);
            GenericTypeName b = assertTypeAndCast<GenericTypeName>(a.typeName);

            Assert.AreEqual(3, b.genericTypes.Count);
            Assert.AreEqual("t", b.genericTypes[0].name);
            Assert.AreEqual("p", b.genericTypes[1].name);
            Assert.AreEqual("Banana", b.genericTypes[2].name);

            GenericTypeName c = assertTypeAndCast<GenericTypeName>(b.genericTypes[2]);
            Assert.AreEqual(3, c.genericTypes.Count);
            Assert.AreEqual("t", c.genericTypes[0].name);
            Assert.AreEqual("d", c.genericTypes[1].name);
            Assert.AreEqual("g", c.genericTypes[2].name);
        }

        [TestMethod]
        public void ClassTest()
        {
            Expression expression = parseExpression(@"class Test {
                jelly -> int;
                public belly -> Banana<int,int>;
                p -> String = ""haha"";
                
                foopy(a -> int, b -> Banana) -> String {
                    a = 5;
                }
            ");
            ClassDefinitionExpression a = assertTypeAndCast<ClassDefinitionExpression>(expression);
            Assert.AreEqual("Test", a.name);
            Assert.AreEqual(4, a.members.Count);
            Assert.AreEqual(Visibility.None, a.visibility);

            VariableDeclarationExpression b = assertTypeAndCast<VariableDeclarationExpression>(a.members[0]);
            VariableDeclarationExpression c = assertTypeAndCast<VariableDeclarationExpression>(a.members[1]);
            VariableDeclarationAssignmentExpression d = assertTypeAndCast<VariableDeclarationAssignmentExpression>(a.members[2]);
            FunctionExpression e = assertTypeAndCast<FunctionExpression>(a.members[3]);

            Assert.AreEqual("jelly", b.name);
            Assert.AreEqual("int", b.typeName.name);
            Assert.IsNotInstanceOfType(b.typeName, typeof(GenericTypeName));
            Assert.AreEqual(Visibility.None, b.visibility);

            Assert.AreEqual("belly", c.name);
            Assert.AreEqual("Banana", c.typeName.name);
            Assert.AreEqual(Visibility.Public, c.visibility);
            GenericTypeName f = assertTypeAndCast<GenericTypeName>(c.typeName);
            Assert.AreEqual(2, f.genericTypes.Count);
            Assert.AreEqual("int", f.genericTypes[0].name);
            Assert.IsNotInstanceOfType(f.genericTypes[0], typeof(GenericTypeName));
            Assert.AreEqual("int", f.genericTypes[1].name);
            Assert.IsNotInstanceOfType(f.genericTypes[1], typeof(GenericTypeName));

            Assert.AreEqual("p", d.declaration.name);
            Assert.AreEqual("String", d.declaration.typeName.name);
            Assert.IsNotInstanceOfType(d.declaration.typeName, typeof(GenericTypeName));

            StringLiteralExpression g = assertTypeAndCast<StringLiteralExpression>(d.value);
            Assert.AreEqual("haha", g.value);

            Assert.AreEqual("foopy", e.name);
            Assert.AreEqual(Visibility.None, e.visibility);
            Assert.AreEqual("String", e.returnType);
            Assert.AreEqual(2, e.arguments.members.Count);
            Assert.AreEqual(1, e.body.innerExpressions.Count);

            Assert.AreEqual("a", e.arguments.members[0].name);
            Assert.AreEqual("int", e.arguments.members[0].typeName.name);
            Assert.IsNotInstanceOfType(e.arguments.members[0].typeName, typeof(GenericTypeName));

            Assert.AreEqual("b", e.arguments.members[1].name);
            Assert.AreEqual("Banana", e.arguments.members[1].typeName.name);
            Assert.IsNotInstanceOfType(e.arguments.members[1].typeName, typeof(GenericTypeName));

            VariableAssignmentExpression h = assertTypeAndCast<VariableAssignmentExpression>(e.body.innerExpressions[0]);
            Assert.AreEqual("a", h.name);
            IntegralLiteralExpression i = assertTypeAndCast<IntegralLiteralExpression>(h.value);
            Assert.AreEqual(5, i.value);
        }
    }
}

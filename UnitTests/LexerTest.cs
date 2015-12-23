using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

using llvm_test;
using llvm_test.Tokens;
using System.Text;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class LexerTest
    {
        private Lexer makeLexer(String input)
        {
            return new Lexer(new MemoryStream(Encoding.UTF8.GetBytes(input)));
        }

        private void testToken<T,U>(TokenBase t, U value) where T : Token<U>
        {
            Assert.IsTrue(t.valid);
            Assert.IsTrue(t is T);
            Assert.AreEqual((t as T).value, value);
        }

        [TestMethod]
        public void StringConstant()
        {
            Lexer l = makeLexer(@"""gjlskjdoijgnreojoij245%%  22446 *)()__ASDJlkj3lk1|\}{d  """);
            testToken<SymbolToken, SymbolToken.Symbol>(l.consume(), SymbolToken.Symbol.DoubleQuote);
            testToken<StringToken, String>(l.consume(), @"gjlskjdoijgnreojoij245%%  22446 *)()__ASDJlkj3lk1|\}{d  ");
            testToken<SymbolToken, SymbolToken.Symbol>(l.consume(), SymbolToken.Symbol.DoubleQuote);
        }

        [TestMethod]
        public void StringThenOther()
        {
            Lexer l = makeLexer(@"""jhdf""38209""*""");
            testToken<SymbolToken, SymbolToken.Symbol>(l.consume(), SymbolToken.Symbol.DoubleQuote);
            testToken<StringToken, String>(l.consume(), @"jhdf");
            testToken<SymbolToken, SymbolToken.Symbol>(l.consume(), SymbolToken.Symbol.DoubleQuote);
            testToken<NumberToken, long>(l.consume(), 38209L);
            testToken<SymbolToken, SymbolToken.Symbol>(l.consume(), SymbolToken.Symbol.DoubleQuote);
            testToken<StringToken, String>(l.consume(), @"*");
            testToken<SymbolToken, SymbolToken.Symbol>(l.consume(), SymbolToken.Symbol.DoubleQuote);
        }

        [TestMethod]
        public void NumberConstant()
        {
            Lexer l = makeLexer(@"7491847700");
            testToken<NumberToken, long>(l.consume(), 7491847700L);
        }

        [TestMethod]
        public void SymbolConstant()
        {
            //bad test, anything after " is considered a string
            Lexer l = makeLexer(String.Join("", SymbolToken.SymbolTable.Keys.Where(x => x != "\"")) + "\"");
            foreach(String symbol in SymbolToken.SymbolTable.Keys)
            {
                if (symbol == "\"") continue;
                testToken<SymbolToken, SymbolToken.Symbol>(l.consume(), SymbolToken.SymbolTable[symbol]);
            }

            testToken<SymbolToken, SymbolToken.Symbol>(l.consume(), SymbolToken.SymbolTable["\""]);
            testToken<StringToken, String>(l.consume(), @"");
            testToken<ErrorToken, String>(l.consume(), "Missing end quote for string.");
        }
    }
}

using NUnit.Framework;
using ProgrammingLanguage.LexicalAnalysis;

namespace ProgrammingLanguageTest.LexerTest
{
    [TestFixture]
    public class Lexer_InvalidInputForLexerTest
    {
        [TestCase("'")]
        [TestCase("\\")]
        [TestCase("?")]
        [TestCase("%")]
        [TestCase("$")]
        [TestCase("değişken x = .5;")]
        [TestCase("değişken x = 5.;")]
        [TestCase("değişken x = 05;")]
        [TestCase("değişken x = 05.4;")]
        [TestCase("değişken x = \"hello;")]
        public void InvalidInput_Lex_ThrowsException(string input)
        {
            Lexer lexer = new Lexer(input);
            Assert.Throws<LexerException>(() => lexer.Lex());
        }
    }
}

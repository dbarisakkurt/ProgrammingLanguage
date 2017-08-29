using NUnit.Framework;
using ProgrammingLanguage.LexicalAnalysis;

namespace ProgrammingLanguageTest.LexerTest
{
    [TestFixture]
    public class Lexer_EqualityOperatorTest
    {
        [TestCase("== =")]
        [TestCase("= ==")]
        [TestCase("+ ==")]
        [TestCase("+ =")]
        public void CreateLexer_AnalyzeInputLexically_3Tokens(string input)
        {
            Lexer lexer = new Lexer(input);
            lexer.Lex();

            Assert.AreEqual(3, lexer.TokenList.Count);
        }
    }
}

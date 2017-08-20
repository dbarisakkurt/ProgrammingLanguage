using NUnit.Framework;
using ProgrammingLanguage.Lexer;

namespace ProgrammingLanguageTest
{
    [TestFixture]
    public class LexerTest
    {
        [Test]
        public void Test1()
        {
            Lexer lexer = new Lexer("");
            lexer.Lex();
        }
    }
}

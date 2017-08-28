using NUnit.Framework;
using ProgrammingLanguage.LexicalAnalysis;

namespace ProgrammingLanguageTest.LexerTest
{
    [TestFixture]
    public class OperatorTest
    {
        [TestCase("+ - * /")]
        public void CreateLexer_AnalyzeInputLexically_Successful(string input)
        {
            Lexer lexer = new Lexer(input);
            lexer.Lex();

            Assert.AreEqual(5, lexer.TokenList.Count);
            Assert.AreEqual(TokenType.PLUS, lexer.TokenList[0].TokenType);
            Assert.AreEqual(TokenType.MINUS, lexer.TokenList[1].TokenType);
            Assert.AreEqual(TokenType.MULTIPLY, lexer.TokenList[2].TokenType);
            Assert.AreEqual(TokenType.DIVIDE, lexer.TokenList[3].TokenType);
            Assert.AreEqual(TokenType.EOF, lexer.TokenList[4].TokenType);
        }
    }
}

using NUnit.Framework;
using ProgrammingLanguage.LexicalAnalysis;

namespace ProgrammingLanguageTest.LexerTest
{
    [TestFixture]
    public class Lexer_CommentTest
    {
        //###################################################################################
        #region Tests

        [TestCase(@"+ # hello
            - # world *
            ==")]
        public void CreateLexer_AnalyzeInputWithCommentLexically_4Tokens(string input)
        {
            Lexer lexer = new Lexer(input);
            lexer.Lex();

            Assert.AreEqual(4, lexer.TokenList.Count);
            Assert.AreEqual(TokenType.PLUS, lexer.TokenList[0].TokenType);
            Assert.AreEqual(TokenType.MINUS, lexer.TokenList[1].TokenType);
            Assert.AreEqual(TokenType.EQUAL, lexer.TokenList[2].TokenType);
            Assert.AreEqual(TokenType.EOF, lexer.TokenList[3].TokenType);
        }

        #endregion
    }
}

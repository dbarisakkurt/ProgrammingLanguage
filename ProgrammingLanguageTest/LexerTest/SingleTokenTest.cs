using NUnit.Framework;
using ProgrammingLanguage.LexicalAnalysis;

namespace ProgrammingLanguageTest.LexerTest
{
    [TestFixture]
    public class SingleTokenTest
    {
        [TestCase("+", TokenType.PLUS)]
        [TestCase(" + ", TokenType.PLUS)]
        [TestCase("-", TokenType.MINUS)]
        [TestCase(" - ", TokenType.MINUS)]
        [TestCase("*", TokenType.MULTIPLY)]
        [TestCase(" * ", TokenType.MULTIPLY)]
        [TestCase("/", TokenType.DIVIDE)]
        [TestCase(" / ", TokenType.DIVIDE)]
        [TestCase("=", TokenType.ASSIGNMENT)]
        [TestCase(" = ", TokenType.ASSIGNMENT)]
        [TestCase("==", TokenType.EQUAL)]
        [TestCase(" == ", TokenType.EQUAL)]
        [TestCase("!", TokenType.NOT)]
        [TestCase(" ! ", TokenType.NOT)]
        [TestCase("!=", TokenType.NOT_EQUAL)]
        [TestCase(" != ", TokenType.NOT_EQUAL)]
        [TestCase("ve", TokenType.AND_KEYWORD)]
        [TestCase(" ve ", TokenType.AND_KEYWORD)]
        [TestCase("veya", TokenType.OR_KEYWORD)]
        [TestCase(" veya ", TokenType.OR_KEYWORD)]
        [TestCase("<", TokenType.LESS_THAN)]
        [TestCase(" < ", TokenType.LESS_THAN)]
        [TestCase("<=", TokenType.LESS_THAN_OR_EQUAL)]
        [TestCase(" <= ", TokenType.LESS_THAN_OR_EQUAL)]
        [TestCase(">", TokenType.GREATER_THAN)]
        [TestCase(" > ", TokenType.GREATER_THAN)]
        [TestCase(">=", TokenType.GREATER_THAN_OR_EQUAL)]
        [TestCase(" >= ", TokenType.GREATER_THAN_OR_EQUAL)]
        [TestCase(",", TokenType.COMMA)]
        [TestCase(" , ", TokenType.COMMA)]
        [TestCase("number", TokenType.VARIABLE)]
        [TestCase(" number ", TokenType.VARIABLE)]
        [TestCase("a23", TokenType.VARIABLE)]
        [TestCase(" a23 ", TokenType.VARIABLE)]
        [TestCase("number1", TokenType.VARIABLE)]
        [TestCase(" number1 ", TokenType.VARIABLE)]
        [TestCase("3", TokenType.NUMBER)]
        [TestCase(" 3 ", TokenType.NUMBER)]
        [TestCase("354", TokenType.NUMBER)]
        [TestCase(" 354 ", TokenType.NUMBER)]
        [TestCase("3.3", TokenType.NUMBER)]
        [TestCase(" 3.3 ", TokenType.NUMBER)]
        [TestCase("1010.43", TokenType.NUMBER)]
        [TestCase(" 1010.43 ", TokenType.NUMBER)]
        [TestCase("0", TokenType.NUMBER)]
        [TestCase("0.0", TokenType.NUMBER)]
        [TestCase("\"hello\"", TokenType.STRING)]
        [TestCase(" \"hello\" ", TokenType.STRING)]
        public void CreateLexer_LexSingleToken_Successful(string input, TokenType tokenType)
        {
            Lexer lexer = new Lexer(input);
            lexer.Lex();

            Assert.AreEqual(2, lexer.TokenList.Count);
            Assert.AreEqual(input.Trim().Trim('"'), lexer.TokenList[0].Value.ToString());
            Assert.AreEqual(tokenType, lexer.TokenList[0].TokenType);
        }
    }
}

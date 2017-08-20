using NUnit.Framework;
using ProgrammingLanguage.Lexer;

namespace ProgrammingLanguageTest
{
    [TestFixture]
    public class LexerTest
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
        [TestCase("ve", TokenType.AND)]
        [TestCase(" ve ", TokenType.AND)]
        [TestCase("veya", TokenType.OR)]
        [TestCase(" veya ", TokenType.OR)]
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

            Assert.AreEqual(1, lexer.TokenList.Count);
            Assert.AreEqual(input.Trim().Trim('"'), lexer.TokenList[0].Value.ToString());
            Assert.AreEqual(tokenType, lexer.TokenList[0].TokenType);
        }

        [TestCase("+ - * /")]
        public void CreateLexer_AnalyzeInputLexically_Successful(string input)
        {
            Lexer lexer = new Lexer(input);
            lexer.Lex();

            Assert.AreEqual(4, lexer.TokenList.Count);
            Assert.AreEqual(TokenType.PLUS, lexer.TokenList[0].TokenType);
            Assert.AreEqual(TokenType.MINUS, lexer.TokenList[1].TokenType);
            Assert.AreEqual(TokenType.MULTIPLY, lexer.TokenList[2].TokenType);
            Assert.AreEqual(TokenType.DIVIDE, lexer.TokenList[3].TokenType);
        }

        [TestCase("== =")]
        [TestCase("= ==")]
        [TestCase("+ ==")]
        [TestCase("+ =")]
        public void CreateLexer_AnalyzeInputLexically_2Tokens(string input)
        {
            Lexer lexer = new Lexer(input);
            lexer.Lex();

            Assert.AreEqual(2, lexer.TokenList.Count);
        }

        [TestCase(@"+ # hello
            - # world *
            ==")]
        public void CreateLexer_AnalyzeInputWithCommentLexically_3Tokens(string input)
        {
            Lexer lexer = new Lexer(input);
            lexer.Lex();

            Assert.AreEqual(3, lexer.TokenList.Count);
            Assert.AreEqual(TokenType.PLUS, lexer.TokenList[0].TokenType);
            Assert.AreEqual(TokenType.MINUS, lexer.TokenList[1].TokenType);
            Assert.AreEqual(TokenType.EQUAL, lexer.TokenList[2].TokenType);
        }

        //negative test cases
        //.5
        //5.
        //05
        //05.4
    }
}

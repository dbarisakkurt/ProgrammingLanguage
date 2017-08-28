using NUnit.Framework;
using ProgrammingLanguage.LexicalAnalysis;
using ProgrammingLanguage.SyntaxAnalysis;

namespace ProgrammingLanguageTest.ParserTest
{
    [TestFixture]
    public class IfStatementTest
    {
        [TestCase("eğer(x==2){yazdır 5;}değilse{yazdır 7;}")]
        [TestCase("eğer (x==2) { yazdır 5; } değilse { yazdır 7; }")]
        [TestCase("eğer (x==2) { yazdır 5 ; } değilse { yazdır 7 ; }")]
        [TestCase("eğer ( x == 2 ) { yazdır 5 ; } değilse { yazdır 7 ; }")]
        [TestCase("eğer ( x == 2 ) { yazdır 2 ; } değilse eğer (x ==3) {  yazdır 3 ; } değilse { yazdır 0;}")]
        public void IfStatement_Parse_NoException(string input)
        {
            Lexer lexer = new Lexer(input);
            lexer.Lex();

            Parser parser = new Parser(lexer.TokenList);
            parser.ParseProgram();

            //No Exception occurs
        }
    }
}

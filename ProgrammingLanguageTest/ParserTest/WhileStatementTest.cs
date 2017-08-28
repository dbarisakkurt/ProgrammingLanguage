using NUnit.Framework;
using ProgrammingLanguage.LexicalAnalysis;
using ProgrammingLanguage.SyntaxAnalysis;

namespace ProgrammingLanguageTest.ParserTest
{
    [TestFixture]
    public class WhileStatementTest
    {
        [TestCase("değişken x=2; oldukça(x<5){yazdır x; x=6;}")]
        [TestCase("değişken x = 2 ; oldukça (x<5) { yazdır x ; x = 6 ; }")]
        [TestCase("değişken x=2 ; oldukça (x < 5) { yazdır x ; x = 6 ; }")]
        [TestCase("değişken x = 2 ; oldukça (x < 5) { yazdır x ; x = 6 ; }")]
        [TestCase("oldukça (z < 5) { z = z + 1 ; }")]               //todo not halting
        [TestCase("değişken z = 2 ; oldukça (z < 5) { yazdır z ; z = z + 1 ; }")]  //real test not stopping
        [TestCase("değişken sayaç = 5 ; oldukça (sayaç>0) { yazdır sayaç; sayaç=sayaç - 1; }")]   //todo not halting
        public void WhileStatement_Parse_NoException(string input)
        {
            Lexer lexer = new Lexer(input);
            lexer.Lex();

            Parser parser = new Parser(lexer.TokenList);
            parser.ParseProgram();

            //No Exception occurs
        }
    }
}

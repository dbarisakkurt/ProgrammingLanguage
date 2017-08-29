using NUnit.Framework;
using ProgrammingLanguage.LexicalAnalysis;
using ProgrammingLanguage.SyntaxAnalysis;

namespace ProgrammingLanguageTest.ParserTest
{
    [TestFixture]
    public class Parser_PrintStatementTest
    {
        [TestCase("yazdır x;")]
        [TestCase("yazdır 55;")]
        [TestCase("yazdır sayı;")]
        [TestCase("yazdır 55+4;")]
        [TestCase("yazdır 55.43;")]
        [TestCase("yazdır 2.5 *2;")]
        [TestCase("yazdır -5;")]
        [TestCase("yazdır -x;")]
        public void PrintStatement_Parse_NoException(string input)
        {
            Lexer lexer = new Lexer(input);
            lexer.Lex();

            Parser parser = new Parser(lexer.TokenList);
            parser.ParseProgram();

            //No Exception occurs
        }
    }
}

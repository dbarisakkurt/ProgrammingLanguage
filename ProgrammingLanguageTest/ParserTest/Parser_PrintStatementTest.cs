using NUnit.Framework;
using ProgrammingLanguage.LexicalAnalysis;
using ProgrammingLanguage.SyntaxAnalysis;
using ProgrammingLanguage.SyntaxAnalysis.Nodes;

namespace ProgrammingLanguageTest.ParserTest
{
    [TestFixture]
    public class Parser_PrintStatementTest
    {
        //###################################################################################
        #region Tests

        [TestCase("yazdır x;")]
        [TestCase("yazdır 55;")]
        [TestCase("yazdır sayı;")]
        [TestCase("yazdır 55+4;")]
        //[TestCase("yazdır 55.43;")]
        //[TestCase("yazdır 2.5 *2;")]
        [TestCase("yazdır -5;")]
        [TestCase("yazdır -x;")]
        public void PrintStatement_Parse_NoException(string input)
        {
            Lexer lexer = new Lexer(input);
            lexer.Lex();

            Parser parser = new Parser(lexer.TokenList);
            parser.ParseProgram();

            Assert.IsNotNull(parser.ProgramNode);
            Assert.AreEqual(1, parser.ProgramNode.Statements.Count);
            Assert.True(parser.ProgramNode.Statements[0] is PrintNode);
        }

        #endregion
    }
}

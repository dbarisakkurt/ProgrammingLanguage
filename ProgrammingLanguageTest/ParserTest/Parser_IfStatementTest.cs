using NUnit.Framework;
using ProgrammingLanguage.LexicalAnalysis;
using ProgrammingLanguage.SyntaxAnalysis;
using ProgrammingLanguage.SyntaxAnalysis.Nodes;

namespace ProgrammingLanguageTest.ParserTest
{
    [TestFixture]
    public class Parser_IfStatementTest
    {
        //###################################################################################
        #region Tests

        [TestCase("eğer(x==2){yazdır 5;}değilse{yazdır 7;}", 1, 1)]
        [TestCase("eğer (x==2) { yazdır 5; } değilse { yazdır 7; }", 1, 1)]
        [TestCase("eğer (x==2) { yazdır 5 ; } değilse { yazdır 7 ; }", 1, 1)]
        [TestCase("eğer ( x == 2 ) { yazdır 5 ; } değilse { yazdır 7 ; }", 1, 1)]
        [TestCase("eğer ( x == 2 ) { yazdır 2 ; } değilse { eğer (x ==3) {  yazdır 3 ; }  değilse { yazdır 0;} }", 1, 1)]
        [TestCase("eğer(x==2){yazdır 5; yazdır 6; yazdır 7;}değilse{yazdır 8; yazdır 9;}", 3, 2)]
        [TestCase("eğer(x==2){yazdır 5; yazdır 6;} değilse { yazdır 7; yazdır 8; yazdır 9;}", 2, 3)]
        public void IfStatement_Parse_NoException(string input, int nodeCountInMainIfBranch, int nodeCountInMainElseBranch)
        {
            Lexer lexer = new Lexer(input);
            lexer.Lex();

            Parser parser = new Parser(lexer.TokenList);
            parser.ParseProgram();

            Assert.IsNotNull(parser.ProgramNode);
            Assert.AreEqual(1, parser.ProgramNode.Statements.Count);
            Assert.True(parser.ProgramNode.Statements[0] is IfNode);

            IfNode ifNode = (IfNode)parser.ProgramNode.Statements[0];
            Assert.AreEqual(nodeCountInMainIfBranch, ifNode.Statements.Count);
            Assert.AreEqual(nodeCountInMainElseBranch, ifNode.ElseBlock.Count);
        }

        #endregion
    }
}

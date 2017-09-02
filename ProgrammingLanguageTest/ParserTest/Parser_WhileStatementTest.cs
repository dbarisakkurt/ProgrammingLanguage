using NUnit.Framework;
using ProgrammingLanguage.LexicalAnalysis;
using ProgrammingLanguage.SyntaxAnalysis;
using ProgrammingLanguage.SyntaxAnalysis.Nodes;

namespace ProgrammingLanguageTest.ParserTest
{
    [TestFixture]
    public class Parser_WhileStatementTest
    {
        //###################################################################################
        #region Tests

        [TestCase("değişken x=2; oldukça(x<5){yazdır x; x=6;}", 2)]
        [TestCase("değişken x = 2 ; oldukça (x<5) { yazdır x ; x = 6 ; }", 2)]
        [TestCase("değişken x=2 ; oldukça (x < 5) { yazdır x ; x = 6 ; }", 2)]
        [TestCase("değişken x = 2 ; oldukça (x < 5) { yazdır x ; x = 6 ; }", 2)]
        [TestCase("oldukça (z < 5) { z = z + 1 ; }", 1)]
        [TestCase("değişken z = 2 ; oldukça (z < 5) { yazdır z ; z = z + 1 ; }", 2)]
        [TestCase("değişken sayaç = 5 ; oldukça (sayaç>0) { yazdır sayaç; sayaç=sayaç - 1; }", 2)]
        public void WhileStatement_Parse_NoException(string input, int nodeCount)
        {
            Lexer lexer = new Lexer(input);
            lexer.Lex();

            Parser parser = new Parser(lexer.TokenList);
            parser.ParseProgram();

            Assert.IsNotNull(parser.ProgramNode);
            Assert.AreEqual(nodeCount, parser.ProgramNode.Statements.Count);
            Assert.True(parser.ProgramNode.Statements[nodeCount - 1] is WhileNode);

            if(nodeCount == 2)
            {
                WhileNode whileNode = (WhileNode)parser.ProgramNode.Statements[nodeCount - 1];
                Assert.True(whileNode.Statements[0] is PrintNode);
                Assert.True(whileNode.Statements[1] is VariableDeclarationNode);
            }
        }

        #endregion
    }
}

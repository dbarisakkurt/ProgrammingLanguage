using NUnit.Framework;
using ProgrammingLanguage.LexicalAnalysis;
using ProgrammingLanguage.SyntaxAnalysis;
using ProgrammingLanguage.SyntaxAnalysis.Nodes;

namespace ProgrammingLanguageTest.ParserTest
{
    [TestFixture]
    public class Parser_FunctionDeclarationTest
    {
        //###################################################################################
        #region Tests

        [TestCase("fonk zdeğer(x,y){değişken z=5; dön z;}", 2)]
        [TestCase("fonk zdeğer (x , y){değişken z=5; dön z;}", 2)]
        [TestCase("fonk zdeğer(x,y) { değişken z = 5 ; dön z ; }", 2)]
        [TestCase("fonk zdeğer (x , y) { değişken z = 5 ; dön z ; }", 2)]
        [TestCase("fonk topla2sayı (x , y) { değişken z = x + y ; dön z ; }", 2)]
        [TestCase("fonk topla3sayı (a , b, c) { değişken z = a+b+c ; dön z; }", 2)]
        [TestCase("fonk çember_çevresi (yarıçap) { değişken sonuç = 2*3*yarıçap ; dön sonuç; }", 2)]
        [TestCase("fonk daireAlani (yarıçap) { değişken pi = 3; değişken sonuç = pi*yarıçap*yarıçap ; dön sonuç; }", 3)]
        public void FunctionDeclaration_Parse_NoException(string input, int nodeCount)
        {
            Lexer lexer = new Lexer(input);
            lexer.Lex();

            Parser parser = new Parser(lexer.TokenList);
            parser.ParseProgram();

            Assert.IsNotNull(parser.ProgramNode);
            Assert.AreEqual(1, parser.ProgramNode.Statements.Count);
            Assert.True(parser.ProgramNode.Statements[0] is FunctionDeclarationNode);

            FunctionDeclarationNode fnNode = (FunctionDeclarationNode)parser.ProgramNode.Statements[0];
            Assert.AreEqual(nodeCount, fnNode.Statements.Count);
        }

        #endregion
    }
}

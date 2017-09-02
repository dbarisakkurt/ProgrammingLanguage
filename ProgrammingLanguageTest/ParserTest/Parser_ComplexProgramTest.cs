using System.Collections.Generic;
using NUnit.Framework;
using ProgrammingLanguage.LexicalAnalysis;
using ProgrammingLanguage.SyntaxAnalysis;
using ProgrammingLanguage.SyntaxAnalysis.Nodes;

namespace ProgrammingLanguageTest.ParserTest
{
    [TestFixture]
    public class Parser_ComplexProgramTest
    {
        //###################################################################################
        #region Tests

        [TestCase(@"
fonk topla(x, y)
{
    değişken z = x+y;
	dön z;
}

# ana program
değişken a = 2;
değişken b = 3;

eğer (a > b)
{
    yazdır a;
}
değilse 
{
	değişken z = topla(a, b);
	değişken sayaç = 1;
	
    # döngü
	oldukça(sayaç < 3)
	{
		yazdır z;
		sayaç = sayaç + 1;
	}
}")]
        public void ComplexProgram_Parse_NoError(string input)
        {
            Lexer lexer = new Lexer(input);
            lexer.Lex();

            Parser parser = new Parser(lexer.TokenList);
            parser.ParseProgram();

            Assert.IsNotNull(parser.ProgramNode);
            Assert.AreEqual(4, parser.ProgramNode.Statements.Count);

            List<Node> statements = parser.ProgramNode.Statements;

            Assert.True(statements[0] is FunctionDeclarationNode);
            Assert.True(statements[1] is VariableDeclarationNode);
            Assert.True(statements[2] is VariableDeclarationNode);
            Assert.True(statements[3] is IfNode);

            IfNode ifNode = (IfNode)statements[3];
            Assert.AreEqual(1, ifNode.Statements.Count);
            Assert.AreEqual(3, ifNode.ElseBlock.Count);

            List<Node> elseBlock = ifNode.ElseBlock;
            Assert.True(elseBlock[0] is VariableDeclarationNode);
            Assert.True(elseBlock[1] is VariableDeclarationNode);
            Assert.True(elseBlock[2] is WhileNode);
        }

        #endregion

    }
}

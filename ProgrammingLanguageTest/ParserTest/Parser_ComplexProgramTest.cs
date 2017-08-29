using NUnit.Framework;
using ProgrammingLanguage.LexicalAnalysis;
using ProgrammingLanguage.SyntaxAnalysis;

namespace ProgrammingLanguageTest.ParserTest
{
    [TestFixture]
    public class Parser_ComplexProgramTest
    {
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
	değişken z = topla(a ,b);
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

            //No Exception occurs
        }

    }
}

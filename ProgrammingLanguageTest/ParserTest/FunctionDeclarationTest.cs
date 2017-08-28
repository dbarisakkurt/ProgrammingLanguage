using NUnit.Framework;
using ProgrammingLanguage.LexicalAnalysis;
using ProgrammingLanguage.SyntaxAnalysis;

namespace ProgrammingLanguageTest.ParserTest
{
    [TestFixture]
    public class FunctionDeclarationTest
    {
        [TestCase("fonk zdeğer(x,y){değişken z=5; dön z;}")]
        [TestCase("fonk zdeğer (x , y){değişken z=5; dön z;}")]
        [TestCase("fonk zdeğer(x,y) { değişken z = 5 ; dön z ; }")]
        [TestCase("fonk zdeğer (x , y) { değişken z = 5 ; dön z ; }")]
        [TestCase("fonk topla2sayı (x , y) { değişken z = x + y ; dön z ; }")]
        [TestCase("fonk topla3sayı (a , b) { değişken z = a+b+c ; dön z; }")]
        [TestCase("fonk çember_çevresi (yarıçap) { değişken sonuç = 2*3.14*yarıçap ; dön sonuç; }")]
        [TestCase("fonk daireAlani (yarıçap) { değişken pi = 3.14; değişken sonuç = pi*yarıçap*yarıçap ; dön sonuç; }")]
        public void FunctionDeclaration_Parse_NoException(string input)
        {
            Lexer lexer = new Lexer(input);
            lexer.Lex();

            Parser parser = new Parser(lexer.TokenList);
            parser.ParseProgram();

            //No Exception occurs
        }
    }
}

using NUnit.Framework;
using ProgrammingLanguage.LexicalAnalysis;
using ProgrammingLanguage.SyntaxAnalysis;

namespace ProgrammingLanguageTest.ParserTest
{
    [TestFixture]
    public class VariableDeclarationTest
    {
        [TestCase("değişken x = 2;")]
        [TestCase("değişken xx = 2;")]
        [TestCase("değişken x = 24;")]
        [TestCase("değişken a = doğru;")]
        [TestCase("değişken b = yanlış;")]
        [TestCase("değişken cc = 5 + 5;")]
        [TestCase("değişken ddd = doğru veya yanlış;")]
        [TestCase("değişken eeee = 5 == 5;")]
        [TestCase("değişken fffff = yanlış != doğru;")]
        [TestCase("değişken gggggg = 5 < 6 veya 7 > 5;")]
        [TestCase("değişken hhhhhhh = 5 <= 6 ve 7 >= 5;")]
        [TestCase("değişken eeee = yanlış ve doğru;")]
        [TestCase("değişken a=2;")]
        [TestCase("değişken aa=2;")]
        [TestCase("değişken a=234;")]
        [TestCase("değişken a =2;")]
        [TestCase("değişken a= 2;")]
        [TestCase("değişken a = ( 5 + 4 );")]
        [TestCase("değişken c = doğru veya yanlış == doğru;")]
        [TestCase("değişken a = (2);")]
        [TestCase("değişken a = ( 2 ) ;")]
        [TestCase("değişken a = ( 2 );")]
        [TestCase("değişken c = (doğru veya yanlış) == doğru;")]
        [TestCase("değişken c = doğru veya yanlış == (doğru);")]
        [TestCase("değişken k = z+1;")]
        public void DeclareVariable_Parse_NoException(string input)
        {
            Lexer lexer = new Lexer(input);
            lexer.Lex();

            Parser parser = new Parser(lexer.TokenList);
            parser.ParseProgram();

            //No Exception occurs
        }
    }
}

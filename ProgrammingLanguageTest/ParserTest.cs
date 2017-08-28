using NUnit.Framework;
using ProgrammingLanguage.LexicalAnalysis;
using ProgrammingLanguage.SyntaxAnalysis;

namespace ProgrammingLanguageTest
{
    [TestFixture]
    public class ParserTest
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
        [TestCase("değişken hhhhhhh = 5 <= 6 ve 7 >= 5;")]        //not stopping
        [TestCase("değişken eeee = yanlış ve doğru;")]            //not stopping
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
        public void DeclareVariable_Parse_NoException(string input)
        {
            Lexer lexer = new Lexer(input);
            lexer.Lex();

            Parser parser = new Parser(lexer.TokenList);
            parser.ParseProgram();

            //No Exception occurs
        }

        [TestCase("eğer(x==2){yazdır 5;}değilse{yazdır 7;}")]
        [TestCase("eğer (x==2) { yazdır 5; } değilse { yazdır 7; }")]
        [TestCase("eğer (x==2) { yazdır 5 ; } değilse { yazdır 7 ; }")]
        [TestCase("eğer ( x == 2 ) { yazdır 5 ; } değilse { yazdır 7 ; }")]
        public void IfStatement_Parse_NoException(string input)
        {
            Lexer lexer = new Lexer(input);
            lexer.Lex();

            Parser parser = new Parser(lexer.TokenList);
            parser.ParseProgram();

            //No Exception occurs
        }

        [TestCase("değişken x=2; oldukça(x<5){yazdır x; x=6;}")]
        [TestCase("değişken x = 2 ; oldukça (x < 5) { yazdır x; x=6; }")]
        [TestCase("değişken x = 2 ; oldukça (x<5) { yazdır x ; x = 6 ; }")]
        [TestCase("değişken x=2 ; oldukça (x < 5) { yazdır x ; x = 6 ; }")]
        [TestCase("değişken x = 2 ; oldukça (x < 5) { yazdır x ; x = 6 ; }")] //TODO: //x=x+1 error
        public void WhileStatement_Parse_NoException(string input)
        {
            Lexer lexer = new Lexer(input);
            lexer.Lex();

            Parser parser = new Parser(lexer.TokenList);
            parser.ParseProgram();

            //No Exception occurs
        }

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

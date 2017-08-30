using NUnit.Framework;
using ProgrammingLanguage.LexicalAnalysis;

namespace ProgrammingLanguageTest.LexerTest
{
    //###################################################################################
    #region Tests

    [TestFixture]
    public class Lexer_VariableDeclarationTest
    {
        [TestCase("değişken hhhhhhh = 5 <= 6 ve 7 >= 5;", 11)]
        [TestCase("değişken eeee = yanlış ve doğru;", 7)]
        [TestCase("değişken a=2;", 5)]
        [TestCase("değişken aa=2;", 5)]
        [TestCase("değişken a=234;", 5)]
        [TestCase("değişken a =2;", 5)]
        [TestCase("değişken a= 2;", 5)]
        [TestCase("değişken a = ( 5 + 4 );", 9)]
        [TestCase("değişken a = (2);", 7)]
        [TestCase("değişken a = ( 2 ) ;", 7)]
        [TestCase("değişken a = ( 2 );", 7)]
        [TestCase("değişken sayı1 = 2;", 5)]
        [TestCase("değişken sayı30 = 2;", 5)]
        [TestCase("değişken toplam_değeri = 70;", 5)]
        [TestCase("değişken _ = 70;", 5)]
        [TestCase("değişken _değer1 = 70;", 5)]
        [TestCase("değişken toplam_değeri1 = 70;", 5)]
        [TestCase("değişken toplam_değeri1 = -70;", 6)]
        [TestCase("yazdır -70;", 4)]
        public void CreateLexer_AnalyzeVariableDecleration_4Tokens(string input, int tokenCount)
        {
            Lexer lexer = new Lexer(input);
            lexer.Lex();

            Assert.AreEqual(tokenCount, lexer.TokenList.Count);
        }

        #endregion
    }
}

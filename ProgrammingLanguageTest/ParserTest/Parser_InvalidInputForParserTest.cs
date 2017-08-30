using NUnit.Framework;
using ProgrammingLanguage.LexicalAnalysis;
using ProgrammingLanguage.SyntaxAnalysis;

namespace ProgrammingLanguageTest.ParserTest
{
    [TestFixture]
    class Parser_InvalidInputForParserTest
    {
        //###################################################################################
        #region Tests

        [TestCase("eğer x=2;")]
        [TestCase("değilse { while {} yazdır 2;}")]                 
        [TestCase("eğer (x > 2) yazdır 3;")]                            
        [TestCase("eğer (x > 2) yazdır 3; değilse yazdır 4;")]
        [TestCase("eğer (x > 2)  {yazdır 3;} değilse yazdır 4;")]
        [TestCase("3 yazdır")]                                       
        [TestCase("değişken x += 3;")]                                                          
        public void InvalidInput_Parse_ThrowsException(string input)
        {
            Lexer lexer = new Lexer(input);
            lexer.Lex();

            Parser parser = new Parser(lexer.TokenList);
            
            Assert.Throws<ParseException>(() => parser.ParseProgram());
        }

        #endregion
    }
}

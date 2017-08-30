using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using ProgrammingLanguage.Interpreter;
using ProgrammingLanguage.LexicalAnalysis;
using ProgrammingLanguage.SyntaxAnalysis;

namespace ProgrammingLanguageTest.InterpreterTest
{
    [TestFixture]
    public class Interpreter_IfStatementTest
    {
        //###################################################################################
        #region Setup/TearDown

        [OneTimeTearDown]
        public void TearDown()
        {
            StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());
            sw.AutoFlush = true;
            Console.SetOut(sw);
        }

        #endregion

        //###################################################################################
        #region Tests

        [TestCase(@"değişken x = 4 + 5; değişken a = 1; değişken b = -1;
                    eğer(9 > 5) { yazdır a; } değilse  { yazdır b; }", "1")]
        [TestCase(@"değişken x = 4 + 5; değişken a = 6; değişken b = -6;
                    eğer(x > 5) { yazdır a; } değilse  { yazdır b; }", "6")]
        [TestCase(@"değişken x = 4 + 5; değişken a = 6; değişken b = -6;
                    eğer(x > 5) { yazdır a; yazdır 100; yazdır doğru; } değilse  { yazdır b; }", "6\r\n100\r\ndoğru")]
        [TestCase(@"değişken x = 4 + 5; değişken a = 6; değişken b = -6;
                    eğer(x > 5) { yazdır a; } değilse  { yazdır b; yazdır 50; yazdır yanlış; }", "6")]
        [TestCase(@"değişken x = 4 + 5; değişken a = 6; değişken b = -6;
                    eğer(x > 5) { yazdır a; yazdır 99; } değilse  { yazdır b; yazdır 101; }", "6\r\n99")]
        [TestCase(@"değişken x = 4 + 5; değişken a = 1; değişken b = -1;
                    eğer(9 < 5) { yazdır a; } değilse  { yazdır b; }", "-1")]
        [TestCase(@"değişken x = 4 + 5; değişken a = 6; değişken b = -6;
                    eğer(x < 5) { yazdır a; } değilse  { yazdır b; }", "-6")]
        [TestCase(@"değişken x = 4 + 5; değişken a = 6; değişken b = -6;
                    eğer(x < 5) { yazdır a; yazdır 100; yazdır doğru; } değilse  { yazdır b; }", "-6")]
        [TestCase(@"değişken x = 4 + 5; değişken a = 6; değişken b = -6;
                    eğer(x < 5) { yazdır a; } değilse  { yazdır b; yazdır 50; yazdır yanlış; }", "-6\r\n50\r\nyanlış")]
        [TestCase(@"değişken x = 4 + 5; değişken a = 6; değişken b = -6;
                    eğer(x < 5) { yazdır a; yazdır 99; } değilse  { yazdır b; yazdır 101; }", "-6\r\n101")]
        [TestCase(@"değişken x = 4 + 5; değişken a = 1; değişken b = -1;
                    eğer(9 > 5) { yazdır a; }", "1")]
        [TestCase(@"değişken x = 4 + 5; değişken a = 1; değişken b = -1;
                    eğer(9 < 5) { yazdır a; }", "")]
        [TestCase("değişken x = 3; eğer ( x == 2 ) { yazdır 2 ; } değilse { eğer (x == 3) {  yazdır 3; }  değilse { yazdır 101;} }", "3")]
        [TestCase("değişken x = 1; eğer ( x == 2 ) { yazdır 2 ; } değilse { eğer (x >3) {  yazdır 3 ; }  değilse { yazdır 1001;} }", "1001")]
        public void IfStatement_Interpret_Successfull(string input, string result)
        {
            Lexer lexer = new Lexer(input);
            lexer.Lex();

            Parser parser = new Parser(lexer.TokenList);
            parser.ParseProgram();
            var out1 = Console.Out;

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                Evaluator eval = new Evaluator();
                List<object> objRes = eval.Eval(parser.ProgramNode);

                Assert.AreEqual(result, sw.ToString().Trim());
            }
            Console.SetOut(out1);
        }

        #endregion
    }
}

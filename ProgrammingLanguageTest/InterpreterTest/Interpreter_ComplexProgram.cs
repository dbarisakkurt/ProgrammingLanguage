using System;
using System.IO;
using NUnit.Framework;
using ProgrammingLanguage.Interpreter;
using ProgrammingLanguage.LexicalAnalysis;
using ProgrammingLanguage.SyntaxAnalysis;

namespace ProgrammingLanguageTest.InterpreterTest
{
    [TestFixture]
    public class Interpreter_ComplexProgram
    {
        //###################################################################################
        #region Setup/TearDown

        [TearDown]
        public void TearDown()
        {
            StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());
            sw.AutoFlush = true;
            Console.SetOut(sw);
        }

        #endregion

        //###################################################################################
        #region Tests

        [TestCase(@"fonk ikiKatı(a, b) { eğer (b == 1) { yazdır 12;} değilse { yazdır 18;} }
    ikiKatı(6, 2);", "18")]
        [TestCase(@"fonk ikiKatı(a, b) { eğer (b == 1) {yazdır 12;} değilse { yazdır 18;} }
    ikiKatı(6, 1);", "12")]
        [TestCase(@"fonk ikiKatı(a, b) { eğer (b == 1) {yazdır 12;} değilse { yazdır 24;} }
    ikiKatı(6, 2);", "24")]
        public void IfInsideFunction_Interpret_Successfull(string input, string result)
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
                eval.Evaluate(parser.ProgramNode);

                Assert.AreEqual(result, sw.ToString().Trim());
            }
            Console.SetOut(out1);
        }

        [TestCase(@"fonk yazar(a) { değişken sayaç=1; oldukça (sayaç<a) { yazdır 1; sayaç=sayaç+1; } }
    yazar(3);", "1\r\n1")]
        [TestCase(@"fonk yazar(a) { değişken sayaç=1; oldukça (sayaç<a) { yazdır 2; sayaç=sayaç+1; } }
    yazar(2);", "2")]
        [TestCase(@"fonk yazar(a) { değişken sayaç=1; oldukça (sayaç<a) { yazdır 3; sayaç=sayaç+1; } }
    yazar(4);", "3\r\n3\r\n3")]
        public void WhileInsideFunction_Interpret_Successfull(string input, string result)
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
                eval.Evaluate(parser.ProgramNode);

                Assert.AreEqual(result, sw.ToString().Trim());
            }
            Console.SetOut(out1);
        }

        //[TestCase(@"değişken d= 4; fonk topla(v) {
        //eğer (v > 0) { dön v + topla(v - 1); } değilse { dön; } } yazdır topla(d);", "10")]
        //public void RecursiveFunction_Interpret_Successfull(string input, string result)
        //{
        //    Lexer lexer = new Lexer(input);
        //    lexer.Lex();

        //    Parser parser = new Parser(lexer.TokenList);
        //    parser.ParseProgram();
        //    var out1 = Console.Out;

        //    using (StringWriter sw = new StringWriter())
        //    {
        //        Console.SetOut(sw);

        //        Evaluator eval = new Evaluator();
        //        eval.Evaluate(parser.ProgramNode);

        //        Assert.AreEqual(result, sw.ToString().Trim());
        //    }
        //    Console.SetOut(out1);
        //}

        [TestCase("fonk e() { yazdır 2; } fonk d() { yazdır 1; e(); } d();", "1\r\n2")]
        public void FunctionCallInsideFunction_Interpret_Successfull(string input, string result)
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
                eval.Evaluate(parser.ProgramNode);

                Assert.AreEqual(result, sw.ToString().Trim());
            }
            Console.SetOut(out1);
        }

        [TestCase("değişken x = 3; eğer ( x == 2 ) { değişken sayaç = 1; oldukça (sayaç<5) { yazdır sayaç; sayaç = sayaç +1; } } değilse { yazdır 101; yazdır 102;}", "101\r\n102")]
        [TestCase("değişken x = 2; eğer ( x == 2 ) { değişken sayaç = 1; oldukça (sayaç<3) { yazdır sayaç; sayaç = sayaç +1; } } değilse { yazdır 101; yazdır 102;}", "1\r\n2\r\n3")]
        public void WhileInsideIf_Interpret_Successfull(string input, string result)
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
                eval.Evaluate(parser.ProgramNode);
            }
            Console.SetOut(out1);
        }

        [TestCase("değişken x = 3; değişken sayaç=1; oldukça (sayaç < 5) { eğer ( x == 2 ) { yazdır 2 ; } değilse { yazdır 101;}  sayaç = sayaç+1; }", "101\r\n2\r\n101\r\n101\r\n101")]
        [TestCase(@"değişken x = 3; değişken sayaç=1; oldukça (sayaç < 5) {  eğer ( x == 2 veya x==3 ) { yazdır 2 ; } sayaç = sayaç+1; }", "n2\r\n2")]
        public void IfInsideWhile_Interpret_Successfull(string input, string result)
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
                eval.Evaluate(parser.ProgramNode);
            }
            Console.SetOut(out1);
        }

        [TestCase(@"fonk ikiKatı(a, b) { dön a+b; } yazdır ikiKatı(6, 2, 1);")]
        [TestCase(@"fonk diger(a, b) { dön a+b; } yazdır diger(6);")]
        public void FunctionCallAndDeclaration_DifferentNumberOfArguments_ThrowsException(string input)
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

                Assert.Throws<InvalidOperationException>(() => eval.Evaluate(parser.ProgramNode));
            }
            Console.SetOut(out1);
        }

        [TestCase("değişken x = \"hello\"; eğer ( x == \"hello\" ) { yazdır 2 ; } değilse { yazdır 3; }", "2")]
        [TestCase("değişken x = \"hellohello\"; eğer ( x == \"hello\" ) { yazdır 2 ; } değilse { yazdır 3; }", "3")]
        public void StringEquality_Interpret_Successfull(string input, string result)
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
                eval.Evaluate(parser.ProgramNode);
            }
            Console.SetOut(out1);
        }

        #endregion
    }
}

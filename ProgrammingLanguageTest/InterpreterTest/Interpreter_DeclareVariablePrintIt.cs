using System.Collections.Generic;
using System.Globalization;
using NUnit.Framework;
using ProgrammingLanguage.Interpreter;
using ProgrammingLanguage.LexicalAnalysis;
using ProgrammingLanguage.SyntaxAnalysis;
using System;
using System.IO;

namespace ProgrammingLanguageTest.InterpreterTest
{
    [TestFixture]
    public class Interpreter_DeclareVariablePrintIt
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

        [TestCase("değişken x = 2 + 3 + 4; yazdır x;", "9")]
        [TestCase("değişken x = 2 + 3; yazdır x;", "5")]
        [TestCase("değişken x = 2; yazdır x;", "2")]
        [TestCase("değişken x = 3 * 2; yazdır x;", "6")]
        [TestCase("değişken x = 10 * 55; yazdır x;", "550")]
        [TestCase("değişken x = 5-3; yazdır x;", "2")]
        [TestCase("değişken x = 3-5; yazdır x;", "-2")]
        [TestCase("değişken x = 9/3; yazdır x;", "3")]
        [TestCase("değişken x = 2 + 4 * 5; yazdır x;", "22")]
        [TestCase("değişken x = (2 + 4) * 5; yazdır x;", "30")]
        public void DeclareIntegerNumber_PrintIt_InterpretsCorrectValue(string input, string result)
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


                Console.SetOut(out1);
            }
        }

        //[TestCase("değişken x = 2.2 + 3.3; yazdır x;", "5,5")]
        //[TestCase("değişken x = 2.6; yazdır x;", "2.6")]
        //[TestCase("değişken x = 3.1 * 2; yazdır x;", "6,2")]
        //[TestCase("değişken x = 10 * 55.1; yazdır x;", "551")]
        //[TestCase("değişken x = 2.2 + 3.3 + 1.1; yazdır x;", "6,6")]
        //[TestCase("değişken x = 2.2 - 3.3; yazdır x;", "-1,1")]
        //[TestCase("değişken x = 3.3 - 2.2; yazdır x;", "1,1")]
        //[TestCase("değişken x = 1.9 + 3.1 * 5; yazdır x;", "17.4")]
        //[TestCase("değişken x = (1.9 + 4.1) * 5; yazdır x;", "30")]
        //[TestCase("değişken x = 9.3/3.1; yazdır x;", "3")]
        //public void DeclareFloatNumber_PrintIt_InterpretsCorrectValue(string input, string result)
        //{
        //    Lexer lexer = new Lexer(input);
        //    lexer.Lex();

        //    Parser parser = new Parser(lexer.TokenList);
        //    parser.ParseProgram();

        //    Evaluator eval = new Evaluator();
        //    List<object> objRes = eval.Eval(parser.ProgramNode);

        //    Assert.AreEqual(1, objRes.Count);
        //    Assert.AreEqual(string.Format(CultureInfo.InvariantCulture, result), 
        //        string.Format(CultureInfo.InvariantCulture, (objRes[0]).ToString()));
        //}

        [TestCase("değişken x = doğru; yazdır x;", "doğru")]
        [TestCase("değişken x = yanlış; yazdır x;", "yanlış")]
        [TestCase("değişken x = doğru ve doğru; yazdır x;", "doğru")]
        [TestCase("değişken x = doğru ve yanlış; yazdır x;", "yanlış")]
        [TestCase("değişken x = yanlış ve doğru; yazdır x;", "yanlış")]
        [TestCase("değişken x = yanlış ve yanlış; yazdır x;", "yanlış")]
        [TestCase("değişken x = doğru veya doğru; yazdır x;", "doğru")]
        [TestCase("değişken x = doğru veya yanlış; yazdır x;", "doğru")]
        [TestCase("değişken x = yanlış veya doğru; yazdır x;", "doğru")]
        [TestCase("değişken x = yanlış veya yanlış; yazdır x;", "yanlış")]
        public void DeclareBooleanNumber_PrintIt_InterpretsCorrectValue(string input, string result)
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

                Console.SetOut(out1);
            }
        }

        [TestCase("değişken x = \"hello\" yazdır x;", "hello")]
        [TestCase("değişken x = \"hello world\"; yazdır x;", "hello world")]
        public void DeclareString_PrintIt_InterpretsCorrectValue(string input, string result)
        {
            Lexer lexer = new Lexer(input);
            lexer.Lex();

            Parser parser = new Parser(lexer.TokenList);
            parser.ParseProgram();

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                Evaluator eval = new Evaluator();
                eval.Evaluate(parser.ProgramNode);
            }
        }

        [TestCase("değişken x = 2 == 2; yazdır x;", "doğru")]
        [TestCase("değişken x = 2 != 2; yazdır x;", "yanlış")]
        [TestCase("değişken x = 3 == 2; yazdır x;", "yanlış")]
        [TestCase("değişken x = 5 != 6; yazdır x;", "doğru")]
        public void DeclareIBoolUsingEquality_PrintIt_InterpretsCorrectValue(string input, string result)
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
        }

        [TestCase("değişken x = 2 < 2; yazdır x;", "yanlış")]
        [TestCase("değişken x = 2 > 2; yazdır x;", "yanlış")]
        [TestCase("değişken x = 2 < 3; yazdır x;", "doğru")]
        [TestCase("değişken x = 3 > 2; yazdır x;", "doğru")]
        [TestCase("değişken x = 2 <= 3; yazdır x;", "doğru")]
        [TestCase("değişken x = 3 >= 2; yazdır x;", "doğru")]
        [TestCase("değişken x = 4 <= 3; yazdır x;", "yanlış")]
        [TestCase("değişken x = 1 >= 2; yazdır x;", "yanlış")]
        public void DeclareIBoolUsingComparison_PrintIt_InterpretsCorrectValue(string input, string result)
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

                Console.SetOut(out1);
            }
        }


        [TestCase("değişken x = !doğru; yazdır x;", "yanlış")]
        [TestCase("değişken x = !yanlış; yazdır x;", "doğru")]
        [TestCase("değişken x = !(doğru ve doğru); yazdır x;", "yanlış")]
        [TestCase("değişken x = doğru ve !yanlış; yazdır x;", "doğru")]
        public void DeclareBoolean_TestNotOperatorPrintIt_InterpretsCorrectValue(string input, string result)
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


                Console.SetOut(out1);
            }
        }

        #endregion
    }
}

using System;
using System.IO;
using NUnit.Framework;
using ProgrammingLanguage.Interpreter;
using ProgrammingLanguage.LexicalAnalysis;
using ProgrammingLanguage.SyntaxAnalysis;

namespace ProgrammingLanguageTest.InterpreterTest
{
    [TestFixture]
    public class InterpreterRecursiveFunctions
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

        [TestCase(@"değişken d= 3; fonk topla(v) {
        eğer (v > 0) { dön v + topla(v - 1); } değilse { dön 0; } } yazdır topla(d);", "6")]
        [TestCase(@"değişken d= 5; fonk faktoriyel(v) {
        eğer (v == 1) { dön 1; } değilse { dön v * faktoriyel(v-1); } } yazdır faktoriyel(d);", "120")]
        [TestCase(@"değişken d= 3; fonk merhabaYaz(v) {
        eğer (v < 1) { dön; } değilse { yazdır ""merhaba""; merhabaYaz(v-1); } } merhabaYaz(d);", "merhaba\r\nmerhaba\r\nmerhaba")]
        //[TestCase(@"değişken d= 9; fonk fibonacci(v) {
        //eğer (v == 0) { dön 0; }
        //eğer (v == 1) { dön 1; } 
        //değilse { dön fibonacci(v-1) + fibonacci(v-2); } } yazdır fibonacci(d);", "21")]
        [TestCase(@"değişken m= 60; değişken n = 20; fonk gcd(m, n) {
        eğer (m % n == 0) { dön n; } değilse { dön gcd(n, m%n); } } yazdır gcd(m, n);", "20")]
        public void Summation_Interpret_Successfull(string input, string result)
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

        #endregion


        //fibonacci
        //obeb, okek
        //hanoi

    }
}

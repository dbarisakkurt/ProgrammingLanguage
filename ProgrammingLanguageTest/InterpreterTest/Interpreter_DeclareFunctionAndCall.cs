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
    public class Interpreter_DeclareFunctionAndCall
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

        //if inside function
        //while inside function
        //function call and argument size different test = exception
        //function call inside function call
        //recursive function call
        //string equality test

        [TestCase("fonk merhabaYaz() { yazdır \"merhaba\"; } merhabaYaz();", "merhaba")]
        [TestCase("fonk doğruDön() { dön doğru; } yazdır doğruDön();", "doğru")]
        [TestCase("fonk stringDön() { dön \"merhaba\"; } değişken t = stringDön(); yazdır t;", "merhaba")]
        [TestCase("fonk birDön() { dön 1; } yazdır birDön();", "1")]
        [TestCase("fonk yazdırParametre(a) { yazdır \"merhaba 1 parametre\"; } yazdırParametre(1);", "merhaba 1 parametre")]
        [TestCase("fonk yazdırParametre(a, b) { yazdır \"merhaba 2 parametre\"; } yazdırParametre(1,2);", "merhaba 2 parametre")]
        [TestCase("fonk yazdırParametre(a, b, c) { yazdır \"merhaba 3 parametre\"; } yazdırParametre(1,2,3);", "merhaba 3 parametre")]
        [TestCase("fonk yazdırParametre(a, b, c, d) { yazdır \"merhaba 4 parametre\"; } yazdırParametre(1,2,3,4);", "merhaba 4 parametre")]
        [TestCase("fonk dönParametre(a) { dön a; }  yazdır dönParametre(1);", "1")]
        [TestCase("fonk dönParametre(a, b) { dön a+b; } yazdır dönParametre(1,2);", "3")]
        [TestCase("fonk dönParametre(a, b, c) { dön a+b+c; } yazdır dönParametre(1,2,3);", "6")]
        [TestCase("fonk dönParametre(a, b, c, d) { dön a+b+c+d } yazdır dönParametre(1,2,3,4);", "10")]
        [TestCase("fonk ikiKatı(a) { değişken b = a; dön b+b; } yazdır ikiKatı(6);", "12")]
        [TestCase("fonk matematikselİfade() { dön 4+5*2; } değişken z = matematikselİfade(); yazdır z;", "14")]
        [TestCase("fonk matematikselİfade() { değişken bb = 4+5*2; dön bb; } yazdır matematikselİfade();", "14")]
        [TestCase("fonk matematikselİfade() { değişken bb = (4+5)*2; dön bb; } yazdır matematikselİfade();", "18")]
        [TestCase("fonk matematikselİfade() { dön (4+5)*2; } yazdır matematikselİfade();", "18")]
        [TestCase("fonk arttir(a) { değişken z = a+1; dön z;} değişken c = arttir(2); yazdır c;", "3")]
        [TestCase("fonk birimFonksiyon(a) { dön a;} değişken c = birimFonksiyon(1); yazdır c;", "1")]
        [TestCase("fonk birimFonksiyon(a) { değişken z = a; dön z;} değişken c = birimFonksiyon(1); yazdır c;", "1")]
        [TestCase("fonk besDegeri () { değişken z = 5; dön z;} değişken c = besDegeri(); yazdır c;", "5")]
        [TestCase("değişken x = 3; değişken y = 4; fonk topla (a, b) { değişken z = a + b; dön z;} değişken c = topla(3, 4); yazdır c;", "7")]
        [TestCase("değişken x = 3; değişken y = 4; fonk topla (a, b) { değişken z = a + b; dön z;} yazdır topla(3, 4);", "7")]
        public void Function_Interpret_Successfull(string input, string result)
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
    }
}

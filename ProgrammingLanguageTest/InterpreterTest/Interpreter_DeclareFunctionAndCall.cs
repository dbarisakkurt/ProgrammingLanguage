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

        //parametre almayan, parametre donmeyen (sadece yazdir cagiracak)
        //parametre almayan, parametre donen
        //1 parametre alan, parametre donmeyen
        //2 parametre alan, parametre donmeyen
        //3 parametre alan, parametre donmeyen
        //1 parametre alan, parametre donen
        //2 parametre alan, parametre donen
        //3 parametre alan, parametre donen
        //dön 2*3+5 gibi seyler donen
        //dön 7 ile sayi donen
        //dön doğru ile bool dönen
        //dön "string" ile string dönen
        //dön c ile değişken dönen
        //dön c+c ile complex bir şey dönen

        [TestCase("fonk arttir(a) { değişken z = a+1; dön z;} değişken c = arttir(2); yazdır c;", "3")]
        [TestCase("fonk birimFonksiyon(a) { dön a;} değişken c = birimFonksiyon(1); yazdır c;", "1")]
        //[TestCase("fonk birimFonksiyon(a) { değişken z = a; dön z;} değişken c = birimFonksiyon(1); yazdır c;", "1")]
        //[TestCase("fonk besDegeri () { değişken z = 5; dön z;} değişken c = besDegeri(); yazdır c;", "5")]
        [TestCase("değişken x = 3; değişken y = 4; fonk topla (a, b) { değişken z = a + b; dön z;} değişken c = topla(3, 4); yazdır c;", "7")]
        //[TestCase("değişken x = 3; değişken y = 4; fonk topla (a, b) { değişken z = a + b; dön z;} yazdır topla(3, 4);", "7")]
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
                List<object> objRes = eval.Eval(parser.ProgramNode);

                Assert.AreEqual(result, sw.ToString().Trim());
            }
            Console.SetOut(out1);
        }

        #endregion
    }
}

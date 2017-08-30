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
                List<object> objRes = eval.Eval(parser.ProgramNode);

                Assert.AreEqual(result, sw.ToString().Trim());
            }
            Console.SetOut(out1);
        }

        #endregion
    }
}

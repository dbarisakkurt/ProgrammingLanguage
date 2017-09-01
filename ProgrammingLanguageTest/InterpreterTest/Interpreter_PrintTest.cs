using System.Collections.Generic;
using NUnit.Framework;
using ProgrammingLanguage.Interpreter;
using ProgrammingLanguage.LexicalAnalysis;
using ProgrammingLanguage.SyntaxAnalysis;
using System;
using System.IO;

namespace ProgrammingLanguageTest.InterpreterTest
{
    [TestFixture]
    public class Interpreter_PrintTest
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

        [TestCase("yazdır 6;", "6")]
        [TestCase("yazdır doğru;", "doğru")]
        [TestCase("yazdır \"house\";", "house")]
        public void Print_Interpret_Successfull(string input, string result)
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

        #endregion
    }
}

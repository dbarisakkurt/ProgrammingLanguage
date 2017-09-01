using System.Collections.Generic;
using NUnit.Framework;
using ProgrammingLanguage.Interpreter;
using ProgrammingLanguage.LexicalAnalysis;
using ProgrammingLanguage.SyntaxAnalysis;
using System.IO;
using System;

namespace ProgrammingLanguageTest.InterpreterTest
{
    [TestFixture]
    public class Interpreter_WhileStatementTest
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

        [TestCase(@" değişken x = 0;
oldukça(x < 5) { yazdır x; x = x + 1; }", "0\r\n1\r\n2\r\n3\r\n4")]
        public void WhileStatement_Interpret_Successfull(string input, string result)
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

using NUnit.Framework;
using ProgrammingLanguage.Interpreter;
using ProgrammingLanguage.LexicalAnalysis;
using ProgrammingLanguage.SyntaxAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageTest.InterpreterTest
{
    [TestFixture]
    class Interpreter_PrintTest
    {
        [TestCase("yazdır 6;", "6")]
        [TestCase("yazdır doğru;", "doğru")]
        [TestCase("yazdır \"house\";", "house")]
        public void Test1(string input, string result)
        {
            Lexer lexer = new Lexer(input);
            lexer.Lex();

            Parser parser = new Parser(lexer.TokenList);
            parser.ParseProgram();

            Evaluator eval = new Evaluator();
            List<object> objRes = eval.Eval(parser.ProgramNode);

            Assert.AreEqual(1, objRes.Count);
            Assert.AreEqual(result, objRes[0].ToString());
        }
    }
}

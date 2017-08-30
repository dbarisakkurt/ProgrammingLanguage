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
    public class Interpreter_IfStatementTest
    {
        [TestCase(@"değişken x = 4 + 5; değişken a = 1; değişken b = -1;
                    eğer(9 > 5) { yazdır a; } değilse  { yazdır b; }", "1")]

        //if (x > 5 ) condition
        //more than 1 statement in if
        //more than 1 statement in else
        //more than 1 statement in both if and else
        public void Test1(string input, string result)
        {
            Lexer lexer = new Lexer(input);
            lexer.Lex();

            Parser parser = new Parser(lexer.TokenList);
            parser.ParseProgram();

            Evaluator eval = new Evaluator();
            List<object> objRes = eval.Eval(parser.ProgramNode);

            Assert.AreEqual(4, objRes.Count);
            Assert.AreEqual(result, objRes[0].ToString());
        }
    }
}

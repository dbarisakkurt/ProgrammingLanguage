using ProgrammingLanguage.Interpreter;
using ProgrammingLanguage.LexicalAnalysis;
using ProgrammingLanguage.SyntaxAnalysis;
using System;
using System.IO;

namespace ProgrammingLanguage
{
    internal static class ConsoleHelper
    {
        //###################################################################################
        #region Internal Methods

        internal static void ParseArguments(string[] arguments)
        {
            if (arguments.Length == 0 || arguments.Length > 1)
            {
                PrintHelp();
            }
            else
            {
                if(arguments[0] == "-v")
                {
                    Console.WriteLine("Programming Language Version 1.0");
                    Console.WriteLine("Author: Dicle Baris Akkurt");
                    Console.WriteLine("2017 - Some rights reserved");
                }
                else
                {
                    string input = File.ReadAllText(arguments[0]);
                    RunInterpreter(input);
                    //process file
                }
            }
        }

        #endregion

        //###################################################################################
        #region Private Methods

        private static void PrintHelp()
        {
            Console.WriteLine("Usage: language.exe <filename>");
            Console.WriteLine("Usage: language.exe -v");
        }

        private static void RunInterpreter(string input)
        {
            Lexer lexer = new Lexer(input);
            lexer.Lex();

            Parser parser = new Parser(lexer.TokenList);
            parser.ParseProgram();

            Evaluator eval = new Evaluator();
            eval.Evaluate(parser.ProgramNode);
        }

        #endregion
    }
}

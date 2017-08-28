
//TODO LATER: add array type
//TODO LATER fix the curly brace requirement after değilse keyword.

//TODO: add assert section to all parser tests
//TODO: implement parse nodes and build tree.
//TODO: implement evaluator

using ProgrammingLanguage.LexicalAnalysis;
using ProgrammingLanguage.SyntaxAnalysis;

namespace ProgrammingLanguage
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "değişken x = 2;";
            Lexer lexer = new Lexer(input);
            lexer.Lex();

            Parser parser = new Parser(lexer.TokenList);
            parser.ParseProgram();

            System.Console.ReadLine();
        }
    }
}
 
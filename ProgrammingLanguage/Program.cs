
//TODO: negative test cases for lexer
//.5
//5.
//05
//05.4
//TODO: implement expression grammar
//TODO later: add array type

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
 
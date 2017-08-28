
//TODO LATER: add array type

//TODO: negative test cases for lexer
//.5
//5.
//05
//05.4
//TODO: negative test cases for parser
//TODO: some exception handling for parser
//TODO: add assert section to all parser tests
//TODO: write test for if, else if, else 
//TODO: write test that contains, func decleration, if-else, while and variable declaration

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
 
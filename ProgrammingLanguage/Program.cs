
//TODO LATER: add array type
//TODO LATER fix the curly brace requirement after değilse keyword.
//TODO LATER: enable float numbers
//TODO LATER: support for statements like yazdır topla(2,3); that calls function in print statement
//TODO LATER: recursive function call


//TODO: TEST return keyword not at the end of the function
//TODO: TEST  version that returns value =>[TestCase(@"fonk ikiKatı(a, b) { eğer (b == 1) {yazdır 12;} değilse { yazdır 24;} } ikiKatı(6, 2);", "24")]

using ProgrammingLanguage.Interpreter;
using ProgrammingLanguage.LexicalAnalysis;
using ProgrammingLanguage.SyntaxAnalysis;

namespace ProgrammingLanguage
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = @"
değişken x = 0;
oldukça( x < 5)
{
    yazdır x;
    x = x + 1;
}
";
            Lexer lexer = new Lexer(input);
            lexer.Lex();

            Parser parser = new Parser(lexer.TokenList);
            parser.ParseProgram();

            Evaluator eval = new Evaluator();
            eval.Evaluate(parser.ProgramNode);

            System.Console.ReadLine();
        }
    }
}
 
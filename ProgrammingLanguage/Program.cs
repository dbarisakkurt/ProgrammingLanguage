﻿
//TODO LATER: add array type
//TODO LATER fix the curly brace requirement after değilse keyword.

//TODO: if veya else bloklarina ekleme yapmiyor.
//TODO: if(x>2) calismiyor.
//TODO: add assert section to all parser tests
//TODO return ifadesi
//TODO: eğer-değilse
//TODO: oldukça
//TODO: fonksiyon tanımlama
//TODO fonksiyon çağırma

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
değişken x = 4+5;
değişken a =1;
değişken b=-1;
eğer( 9 > 5)
{
    yazdır a;
}
değilse
{
    yazdır b;
}
";
            Lexer lexer = new Lexer(input);
            lexer.Lex();

            Parser parser = new Parser(lexer.TokenList);
            parser.ParseProgram();

            Evaluator eval = new Evaluator();
            eval.Eval(parser.ProgramNode);

            System.Console.ReadLine();
        }
    }
}
 
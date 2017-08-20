using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguage
{
    internal static class ConsoleHelper
    {
        internal static void ParseArguments(string[] arguments)
        {
            if(arguments.Length == 0 || arguments.Length > 1)
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
                    //process file
                }
            }
        }

        private static void PrintHelp()
        {
            Console.WriteLine("Usage: language.exe <filename>");
            Console.WriteLine("Usage: language.exe -v");
        }
    }
}

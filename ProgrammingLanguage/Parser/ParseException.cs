using System;

namespace ProgrammingLanguage.SyntaxAnalysis
{
    internal class ParseException : Exception
    {
        public ParseException(string message) : base(message)
        {
        }
    }
}

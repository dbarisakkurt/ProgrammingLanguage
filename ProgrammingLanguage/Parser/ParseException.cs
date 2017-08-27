using System;

namespace ProgrammingLanguage.SyntaxAnalysis
{
    class ParseException : Exception
    {
        public ParseException(string message) : base(message)
        {
        }
    }
}

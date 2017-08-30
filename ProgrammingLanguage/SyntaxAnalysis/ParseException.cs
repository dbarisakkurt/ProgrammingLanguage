using System;

namespace ProgrammingLanguage.SyntaxAnalysis
{
    internal class ParseException : Exception
    {
        internal ParseException(string message) : base(message)
        {
        }
    }
}

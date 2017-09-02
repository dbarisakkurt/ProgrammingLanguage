using System;

namespace ProgrammingLanguage.Interpreter
{
    /// <summary>
    /// This exception is thrown to change flow control when there is return in a function
    /// </summary>
    internal class ReturnException : Exception
    {
        private object m_Value;

        internal object Value { get { return m_Value; } }

        internal ReturnException(string message, object value) : base(message)
        {
            m_Value = value;
        }
    }
}

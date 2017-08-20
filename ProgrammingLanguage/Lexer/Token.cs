using System;

namespace ProgrammingLanguage.Lexer
{
    internal class Token
    {
        private TokenType m_TokenType;
        private object m_Value;

        internal Token(TokenType tokenType, object value)
        {
            m_TokenType = tokenType;
            m_Value = value;
        }

        internal TokenType TokenType { get { return m_TokenType; } }
        internal object Value { get { return m_Value; } }

        public override string ToString()
        {
            string text = "TokenType = " + m_TokenType + " Value: " + m_Value;
            return text;
        }
    }
}

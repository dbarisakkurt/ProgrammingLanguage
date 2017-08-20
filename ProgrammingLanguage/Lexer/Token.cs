using System;

namespace ProgrammingLanguage.Lexer
{
    internal class Token
    {
        private TokenType m_TokenType;
        private object m_Value;

        public Token(TokenType tokenType, object value)
        {
            m_TokenType = tokenType;
            m_Value = value;
        }

        public override string ToString()
        {
            string text = "TokenType = " + m_TokenType + " Value: " + m_Value;
            return text;
        }
    }
}

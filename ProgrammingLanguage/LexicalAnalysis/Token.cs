

namespace ProgrammingLanguage.LexicalAnalysis
{
    internal class Token
    {
        //###################################################################################
        #region Fields

        private TokenType m_TokenType;
        private object m_Value;

        #endregion

        //###################################################################################
        #region Constructor

        internal Token(TokenType tokenType, object value)
        {
            m_TokenType = tokenType;
            m_Value = value;
        }

        #endregion

        //###################################################################################
        #region Properties

        internal TokenType TokenType { get { return m_TokenType; } }

        internal object Value { get { return m_Value; } }

        #endregion

        //###################################################################################
        #region Overrides

        public override string ToString()
        {
            string text = "TokenType = " + m_TokenType + " Value: " + m_Value;
            return text;
        }

        #endregion
    }
}

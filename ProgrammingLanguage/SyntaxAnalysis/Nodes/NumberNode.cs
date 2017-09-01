using ProgrammingLanguage.LexicalAnalysis;

namespace ProgrammingLanguage.SyntaxAnalysis.Nodes
{
    internal class NumberNode : Node
    {
        //###################################################################################
        #region Fields

        private Token m_Token;
        private int m_Value;

        #endregion

        //###################################################################################
        #region Properties

        internal int Value
        {
            get
            {
                return m_Value;
            }
        }

        internal Token Token
        {
            get
            {
                return m_Token;
            }
        }

        #endregion

        //###################################################################################
        #region Constructor

        internal NumberNode(Token token, int value)
        {
            m_Token = token;
            m_Value = value;
        }

        #endregion

        //###################################################################################
        #region Overrides

        public override string ToString()
        {
            return this.m_Value.ToString();
        }

        #endregion
    }
}

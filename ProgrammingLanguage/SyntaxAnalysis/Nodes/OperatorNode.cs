using ProgrammingLanguage.LexicalAnalysis;

namespace ProgrammingLanguage.SyntaxAnalysis.Nodes
{
    internal class OperatorNode : Node
    {
        //###################################################################################
        #region Fields

        private Token m_Token;
        private string m_OperatorValue;

        #endregion

        //###################################################################################
        #region Properties

        internal string OperatorValue
        {
            get
            {
                return m_OperatorValue;
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

        internal OperatorNode(Token token, string value)
        {
            m_Token = token;
            m_OperatorValue = value;
        }

        #endregion

        //###################################################################################
        #region Overrides

        public override string ToString()
        {
            return this.m_OperatorValue.ToString();
        }

        #endregion
    }
}

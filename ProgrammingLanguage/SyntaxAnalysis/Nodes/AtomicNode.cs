using ProgrammingLanguage.LexicalAnalysis;

namespace ProgrammingLanguage.SyntaxAnalysis.Nodes
{
    internal class AtomicNode : Node
    {
        //###################################################################################
        #region Fields

        private Token m_Token;
        private object m_Value;

        #endregion

        //###################################################################################
        #region Properties

        internal object Value
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

        internal AtomicNode(Token token, object value)
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

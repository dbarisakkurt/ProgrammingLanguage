using ProgrammingLanguage.LexicalAnalysis;

namespace ProgrammingLanguage.SyntaxAnalysis.Nodes
{
    internal class BooleanNode : Node
    {
        //###################################################################################
        #region Fields

        private Token m_Token;
        private bool m_Value;

        #endregion

        //###################################################################################
        #region Properties

        internal bool Value
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

        internal BooleanNode(Token token, bool value)
        {
            m_Token = token;
            m_Value = value;
        }

        #endregion

        //###################################################################################
        #region Overrides

        public override string ToString()
        {
            if(m_Value)
            {
                return "doğru";
            }
            else
            {
                return "yanlış";
            }
        }

        #endregion
    }
}

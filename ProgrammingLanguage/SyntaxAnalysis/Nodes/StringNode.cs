using ProgrammingLanguage.LexicalAnalysis;

namespace ProgrammingLanguage.SyntaxAnalysis.Nodes
{
    internal class StringNode : Node
    {
        //###################################################################################
        #region Fields

        private Token m_Token;
        private string m_Value;

        #endregion

        //###################################################################################
        #region Properties

        internal string Value
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

        internal StringNode(Token token, string value)
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

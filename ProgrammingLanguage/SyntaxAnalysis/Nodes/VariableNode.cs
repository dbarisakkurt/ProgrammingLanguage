using ProgrammingLanguage.LexicalAnalysis;

namespace ProgrammingLanguage.SyntaxAnalysis.Nodes
{
    internal class VariableNode : Node
    {
        //###################################################################################
        #region Fields

        private Token m_Token;
        private string m_VariableName;

        #endregion

        //###################################################################################
        #region Properties

        internal string VariableName
        {
            get
            {
                return m_VariableName;
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

        internal VariableNode(string variableName, Token token)
        {
            m_VariableName = variableName;
            m_Token = token;
        }

        #endregion

        //###################################################################################
        #region Overrides

        public override string ToString()
        {
            return this.m_VariableName;
        }

        #endregion
    }
}

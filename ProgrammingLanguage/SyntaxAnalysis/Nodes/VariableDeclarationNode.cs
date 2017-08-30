
namespace ProgrammingLanguage.SyntaxAnalysis.Nodes
{
    internal class VariableDeclarationNode : Node
    {
        //###################################################################################
        #region Fields

        private string m_VariableName;
        private object m_Value;
        private Node m_RightHandValue;

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

        internal Node RightHandValue
        {
            get
            {
                return m_RightHandValue;
            }
        }

        internal string VariableName
        {
            get
            {
                return m_VariableName;
            }
        }

        #endregion

        //###################################################################################
        #region Constructor

        internal VariableDeclarationNode(string variableName, object value, Node rhv)
        {
            m_VariableName = variableName;
            m_Value = value;
            m_RightHandValue = rhv;
        }

        #endregion
    }
}


namespace ProgrammingLanguage.SyntaxAnalysis.Nodes
{
    internal class ReturnNode : Node
    {
        //###################################################################################
        #region Fields

        private Node m_ToBeReturned;

        #endregion

        //###################################################################################
        #region Properties

        public Node ToBeReturned { get { return m_ToBeReturned; } }

        #endregion

        //###################################################################################
        #region Constructor

        public ReturnNode(Node toBeReturned)
        {
            m_ToBeReturned = toBeReturned;
        }

        #endregion
    }
}

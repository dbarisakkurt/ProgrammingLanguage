
namespace ProgrammingLanguage.SyntaxAnalysis.Nodes
{
    internal class UnaryNode : Node
    {
        //###################################################################################
        #region Fields

        private Node m_PrefixNode;
        private Node m_Node;

        #endregion

        //###################################################################################
        #region Properties

        internal Node PrefixNode { get { return m_PrefixNode; } }
        internal Node Node { get { return m_Node; } }

        #endregion

        //###################################################################################
        #region Constructor

        internal UnaryNode(Node prefixNode, Node node)
        {
            m_PrefixNode = prefixNode;
            m_Node = node;
        }

        #endregion
    }
}

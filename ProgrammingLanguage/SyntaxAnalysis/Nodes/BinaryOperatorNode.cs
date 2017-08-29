

namespace ProgrammingLanguage.SyntaxAnalysis.Nodes
{
    internal class BinaryOperatorNode : Node
    {
        //###################################################################################
        #region Fields

        private Node m_Left;
        private Node m_Right;
        private Node m_Oper;

        #endregion

        //###################################################################################
        #region Properties

        internal Node Left { get { return m_Left; } }
        internal Node Right { get { return m_Right; } }
        internal Node Operator { get { return m_Oper; } }

        #endregion

        //###################################################################################
        #region Constructor

        internal BinaryOperatorNode(Node left, Node oper, Node right)
        {
            m_Left = left;
            m_Oper = oper;
            m_Right = right;
        }

        #endregion
    }
}

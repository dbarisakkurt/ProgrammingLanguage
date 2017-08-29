using ProgrammingLanguage.LexicalAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguage.SyntaxAnalysis.Nodes
{
    internal class UnaryNode : Node
    {
        //###################################################################################
        #region Fields

        private Node m_PrefixNode;
        private Node m_Node;

        #endregion

        public Node PrefixNode { get { return m_PrefixNode; } }
        public Node Node { get { return m_Node; } }

        //###################################################################################
        #region Constructor

        public UnaryNode(Node prefixNode, Node node)
        {
            m_PrefixNode = prefixNode;
            m_Node = node;
        }

        #endregion
    }
}

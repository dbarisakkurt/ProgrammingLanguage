using System.Collections.Generic;

namespace ProgrammingLanguage.SyntaxAnalysis.Nodes
{
    class IfNode : Node, INodeList
    {
        //###################################################################################
        #region Fields

        private List<Node> m_Statements = new List<Node>();
        private Node m_Condition;
        private List<Node> m_ElseBlock;

        #endregion

        //###################################################################################
        #region Properties

        internal Node Condition
        {
            get { return m_Condition; }
            set { m_Condition = value; }
        }

        internal List<Node> ElseBlock
        {
            get { return m_ElseBlock; }
            set { m_ElseBlock = value; }
        }

        #endregion

        //###################################################################################
        #region Constructor

        internal IfNode(Node condition, List<Node> ifBlock, List<Node> elseBlock)
        {
            m_Condition = condition;
            m_Statements = ifBlock;
            m_ElseBlock = elseBlock;
        }

        #endregion

        //###################################################################################
        #region INodeList Implementation

        public List<Node> Statements
        {
            get { return m_Statements; }
            set { m_Statements = value; }
        }

        public void AddStatement(Node statementNode, bool elseCall = false)
        {
            if(elseCall)
            {
                m_ElseBlock.Add(statementNode);
            }
            else
            {
                m_Statements.Add(statementNode);
            }
        }

        #endregion
    }
}

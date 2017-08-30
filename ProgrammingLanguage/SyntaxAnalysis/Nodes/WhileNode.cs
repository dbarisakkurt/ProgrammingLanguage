using System.Collections.Generic;

namespace ProgrammingLanguage.SyntaxAnalysis.Nodes
{
    internal class WhileNode : Node, INodeList
    {
        //###################################################################################
        #region Fields

        private List<Node> m_Statements = new List<Node>();
        private Node m_Condition;

        #endregion

        //###################################################################################
        #region Properties

        internal Node Condition
        {
            get { return m_Condition; }
            set { m_Condition = value; }
        }

        #endregion

        //###################################################################################
        #region Constructor

        internal WhileNode( Node condition, List<Node> block)
        {
            m_Condition = condition;
            m_Statements = block;
        }

        #endregion

        //###################################################################################
        #region INodeList Implementation

        public List<Node> Statements
        {
            get
            {
                return m_Statements;
            }
            set
            {
                m_Statements = value;
            }
        }

        public void AddStatement(Node statementNode, bool elseCall)
        {
            m_Statements.Add(statementNode);
        }

        #endregion
    }
}

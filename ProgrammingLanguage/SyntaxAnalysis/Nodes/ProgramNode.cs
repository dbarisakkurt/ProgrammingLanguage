using System.Collections.Generic;

namespace ProgrammingLanguage.SyntaxAnalysis.Nodes
{
    internal class ProgramNode : Node, INodeList
    {
        //###################################################################################
        #region Fields

        private List<Node> m_Statements = new List<Node>();

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

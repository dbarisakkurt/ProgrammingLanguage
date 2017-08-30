using System;
using System.Collections.Generic;

namespace ProgrammingLanguage.SyntaxAnalysis.Nodes
{
    internal class ProgramNode : Node, INodeList
    {
        private List<Node> m_Statements = new List<Node>();

        public List<Node> Statements
        {
            get => m_Statements;
            set
            {
                m_Statements = value;
            }
        }

        public void AddStatement(Node statementNode, bool elseCall)
        {
            m_Statements.Add(statementNode);
        }
    }
}

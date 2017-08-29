using System;
using System.Collections.Generic;

namespace ProgrammingLanguage.SyntaxAnalysis.Nodes
{
    internal class ProgramNode : Node
    {
        private List<Node> m_Statements = new List<Node>();

        internal List<Node> Statements
        {
            get { return m_Statements; }
            set { m_Statements = value; }
        }

        internal void AddStatement(Node statementNode)
        {
            m_Statements.Add(statementNode);
        }
    }
}

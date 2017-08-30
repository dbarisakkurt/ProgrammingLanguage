using System;
using System.Collections.Generic;

namespace ProgrammingLanguage.SyntaxAnalysis.Nodes
{
    internal class WhileNode : Node, INodeList
    {
        private List<Node> m_Statements = new List<Node>();

        private Node m_WhileNode;
        private Node m_Condition;

        public Node WhileNodeProperty
        {
            get { return m_WhileNode; }
            set { m_WhileNode = value; }
        }

        public Node Condition
        {
            get { return m_Condition; }
            set { m_Condition = value; }
        }

        public WhileNode(Node whileNode, Node condition, List<Node> block)
        {
            m_WhileNode = whileNode;
            m_Condition = condition;
            m_Statements = block;
        }

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

using System;
using System.Collections.Generic;

namespace ProgrammingLanguage.SyntaxAnalysis.Nodes
{
    class IfNode : Node, INodeList
    {
        private List<Node> m_Statements = new List<Node>();

        private Node m_IfNode;
        private Node m_Condition;
        private List<Node> m_ElseBlock;

        public Node IfNodeProperty
        {
            get { return m_IfNode; }
            set { m_IfNode = value; }
        }

        public Node Condition
        {
            get { return m_Condition; }
            set { m_Condition = value; }
        }

        public List<Node> ElseBlock
        {
            get { return m_ElseBlock; }
            set { m_ElseBlock = value; }
        }

        public IfNode(Node ifNode, Node condition, List<Node> ifBlock, List<Node> elseBlock)
        {
            m_IfNode = ifNode;
            m_Condition = condition;
            m_Statements = ifBlock;
            m_ElseBlock = elseBlock;
        }

        public List<Node> Statements
        {
            get => m_Statements;
            set
            {
                m_Statements = value;
            }
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
    }
}

using ProgrammingLanguage.LexicalAnalysis;
using System;

namespace ProgrammingLanguage.SyntaxAnalysis.Nodes
{
    internal class VariableDeclarationNode : Node
    {
        private string m_VariableName;
        private object m_Value;
        private Node m_RightHandValue;

        public object Value
        {
            get
            {
                return m_Value;
            }
        }

        public Node RightHandValue { get { return m_RightHandValue; } }

        public string VariableName
        {
            get
            {
                return m_VariableName;
            }
        }

        public VariableDeclarationNode(string variableName, object value, Node rhv)
        {
            m_VariableName = variableName;
            m_Value = value;
            m_RightHandValue = rhv;
        }
    }
}

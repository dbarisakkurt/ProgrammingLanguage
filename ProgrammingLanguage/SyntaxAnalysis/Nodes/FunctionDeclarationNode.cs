using System;
using System.Collections.Generic;

namespace ProgrammingLanguage.SyntaxAnalysis.Nodes
{
    internal class FunctionDeclarationNode : Node, INodeList
    {
        //###################################################################################
        #region Fields

        private List<Node> m_Statements = new List<Node>();
        private Node m_FunctionName;
        private List<Node> m_ParameterList;

        #endregion

        //###################################################################################
        #region Properties

        internal Node FunctionName
        {
            get { return m_FunctionName; }
            set { m_FunctionName = value; }
        }

        internal List<Node> ParameterList
        {
            get { return m_ParameterList; }
            set { m_ParameterList = value; }
        }

        #endregion

        //###################################################################################
        #region Constructor

        public FunctionDeclarationNode(Node functionName, List<Node> parameterList, List<Node> statements)
        {
            m_FunctionName = functionName;
            m_ParameterList = parameterList;
            m_Statements = statements;
        }

        #endregion

        //###################################################################################
        #region INodeList Implementation

        public List<Node> Statements
        {
            get { return m_Statements; }
            set { m_Statements = value; }
        }

        public void AddStatement(Node statementNode, bool elseCall)
        {
            m_Statements.Add(statementNode);
        }

        #endregion
    }
}

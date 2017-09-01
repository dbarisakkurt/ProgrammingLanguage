using System.Collections.Generic;

namespace ProgrammingLanguage.SyntaxAnalysis.Nodes
{
    internal class CallNode : Node
    {
        //###################################################################################
        #region Fields

        private string m_FunctionName;
        private List<Node> m_Arguments;

        #endregion

        //###################################################################################
        #region Properties

        internal string FunctionName { get { return m_FunctionName; } }
        internal List<Node> Arguments { get { return m_Arguments; } }

        #endregion

        //###################################################################################
        #region Constructor

        internal CallNode(string functionName, List<Node> args)
        {
            m_FunctionName = functionName;
            m_Arguments = args;
        }

        #endregion
    }
}

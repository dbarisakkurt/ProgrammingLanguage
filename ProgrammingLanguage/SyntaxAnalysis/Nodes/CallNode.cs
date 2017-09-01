using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguage.SyntaxAnalysis.Nodes
{
    internal class CallNode : Node
    {
        private string m_FunctionName;
        private List<Node> m_Arguments;

        internal string FunctionName { get { return m_FunctionName; } }
        internal List<Node> Arguments { get { return m_Arguments; } }

        internal CallNode(string functionName, List<Node> args)
        {
            m_FunctionName = functionName;
            m_Arguments = args;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguage.SyntaxAnalysis.Nodes
{
    class CallNode : Node
    {
        public Node m_Name;
        public List<Node> m_Arguments;

        public CallNode(Node name, List<Node> args)
        {
            m_Name = name;
            m_Arguments = args;
        }
    }
}

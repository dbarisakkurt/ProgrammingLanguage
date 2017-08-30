using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguage.SyntaxAnalysis.Nodes
{
    class FunctionBlock : Node, INodeList
    {
        public List<Node> Statements { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void AddStatement(Node statementNode, bool elseCall)
        {
            throw new NotImplementedException();
        }
    }
}

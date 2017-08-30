using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguage.SyntaxAnalysis.Nodes
{
    internal interface INodeList
    {
        List<Node> Statements
        {
            get; set;
        }

        void AddStatement(Node statementNode);
    }
}

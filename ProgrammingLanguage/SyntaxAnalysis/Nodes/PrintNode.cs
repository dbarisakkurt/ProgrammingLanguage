using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguage.SyntaxAnalysis.Nodes
{
    internal class PrintNode : Node
    {
        //###################################################################################
        #region Fields

        private Node m_ToBePrinted;

        #endregion

        //###################################################################################
        #region Properties

        public Node ToBePrinted { get { return m_ToBePrinted; } }

        #endregion

        //###################################################################################
        #region Constructor

        public PrintNode(Node toBePrinted)
        {
            m_ToBePrinted = toBePrinted;
        }

        #endregion
    }
}

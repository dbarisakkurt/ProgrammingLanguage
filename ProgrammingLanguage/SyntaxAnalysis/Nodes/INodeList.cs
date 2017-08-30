using System.Collections.Generic;

namespace ProgrammingLanguage.SyntaxAnalysis.Nodes
{
    internal interface INodeList
    {
        //###################################################################################
        #region Properties

        List<Node> Statements
        {
            get; set;
        }

        #endregion

        //###################################################################################
        #region Methods

        void AddStatement(Node statementNode, bool elseCall = false);

        #endregion
    }
}

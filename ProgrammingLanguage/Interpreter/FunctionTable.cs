using ProgrammingLanguage.SyntaxAnalysis.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProgrammingLanguage.Interpreter
{
    internal class FunctionTable
    {
        //###################################################################################
        #region Fields

        private Dictionary<string, FunctionDeclarationNode> m_Functions = new Dictionary<string, FunctionDeclarationNode>();

        #endregion

        //###################################################################################
        #region Methods

        internal void Add(string functionName, FunctionDeclarationNode value)
        {
            m_Functions[functionName] = value;
        }

        internal FunctionDeclarationNode Get(string functionName)
        {
            if (m_Functions.ContainsKey(functionName))
            {
                return m_Functions[functionName];
            }
            else
            {
                throw new InvalidOperationException($"No such variable: {functionName}");
            }
        }

        #endregion
    }
}

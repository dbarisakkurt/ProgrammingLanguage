using ProgrammingLanguage.SyntaxAnalysis.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguage.Interpreter
{
    internal static class FunctionTable
    {
        //###################################################################################
        #region Fields

        private static Dictionary<string, Node> m_Variables = new Dictionary<string, Node>();

        #endregion

        //###################################################################################
        #region Methods

        internal static void Add(string variableName, Node value)
        {
            m_Variables[variableName] = value;
        }

        internal static Node Get(string variableName)
        {
            if (m_Variables.ContainsKey(variableName))
            {
                return m_Variables[variableName];
            }
            else
            {
                throw new InvalidOperationException($"No such variable: {variableName}");
            }
        }

        #endregion
    }
}

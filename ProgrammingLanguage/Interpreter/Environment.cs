using System;
using System.Collections.Generic;

namespace ProgrammingLanguage.Interpreter
{
    internal static class Environment
    {
        //###################################################################################
        #region Fields

        private static Dictionary<string, object> m_Variables = new Dictionary<string, object>();

        #endregion

        //###################################################################################
        #region Methods

        internal static void Add(string variableName, object value)
        {
            m_Variables[variableName] = value;
        }

        internal static object Get(string variableName)
        {
            if(m_Variables.ContainsKey(variableName))
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

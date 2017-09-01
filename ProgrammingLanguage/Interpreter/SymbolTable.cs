using System;
using System.Collections.Specialized;

namespace ProgrammingLanguage.Interpreter
{
    internal class SymbolTable
    {
        //###################################################################################
        #region Fields

        internal OrderedDictionary m_Variables = new OrderedDictionary();

        #endregion

        //###################################################################################
        #region Methods

        internal void Add(string variableName, object value)
        {
            m_Variables[variableName] = value;
        }

        internal object Get(string variableName)
        {
            if(m_Variables.Contains(variableName))
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

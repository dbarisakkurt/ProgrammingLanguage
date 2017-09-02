using System;
using System.Collections.Specialized;

namespace ProgrammingLanguage.Interpreter
{
    internal class SymbolTable
    {
        //###################################################################################
        #region Fields

        internal OrderedDictionary m_Variables = new OrderedDictionary();
        internal SymbolTable m_OuterTable;

        #endregion

        public SymbolTable(SymbolTable outerTable)
        {
            m_OuterTable = outerTable;
        }

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
                if (m_OuterTable != null)
                {
                    SymbolTable outer = m_OuterTable;
                    while (outer != null)
                    {
                        object result = (SymbolTable)outer.Get(variableName);
                        if (result != null)
                            return result;

                        outer = outer.m_OuterTable;
                    }
                    throw new InvalidOperationException("Given variable not found");
                }
                else
                {
                    throw new InvalidOperationException("Given variable not found");
                }
            }
        }

        #endregion
    }
}

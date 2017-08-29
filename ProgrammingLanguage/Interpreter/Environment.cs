using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguage.Interpreter
{
    internal static class Environment
    {
        private static Dictionary<string, object> m_Variables = new Dictionary<string, object>();

        internal static void Add(string variableName, object value)
        {
            m_Variables[variableName] = value;
        }

        internal static object Get(string variableName)
        {
            return m_Variables[variableName];
        }
    }
}

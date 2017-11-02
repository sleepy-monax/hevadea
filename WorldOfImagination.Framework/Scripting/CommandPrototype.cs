using System;
using System.Collections.Generic;
using System.Text;

namespace WorldOfImagination.Framework.Scripting
{
    /// <summary>
    /// Represents the format of a registerable external command.
    /// </summary>
    public class CommandPrototype
    {
        #region Private Variables

        private String m_strName;
        private List<Type> m_listParameterTypes;

        #endregion

        #region Internal Methods

        internal void VerifyParameters(List<object> listParameters)
        {
            if (listParameters.Count < m_listParameterTypes.Count)
                throw new ScriptException("One or more parameters missing.");

            if (listParameters.Count > m_listParameterTypes.Count)
                throw new ScriptException("Too many parameters specified.");

            for (int iIndex = 0; iIndex < listParameters.Count; iIndex++)
            {
                Type type = m_listParameterTypes[iIndex];
                if (listParameters[iIndex].GetType() != type)
                    throw new ScriptException(
                        "Parameter " + (iIndex + 1) + " is not of type " +type.Name + ".");
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Constructs a new command prototype.
        /// </summary>
        /// <param name="strName">Name of the command.</param>
        public CommandPrototype(String strName)
        {
            m_strName = strName;
            m_listParameterTypes = new List<Type>();
        }

        /// <summary>
        /// Adds a new parameter with the given type to the
        /// command prototype. Only int, float, boolean and
        /// String types are allowed.
        /// </summary>
        /// <param name="type">Type of the parameter.</param>
        public void AddParameterType(Type type)
        {
            if (type != typeof(int)
                && type != typeof(float)
                && type != typeof(bool)
                && type != typeof(String))
                throw new ScriptException(
                    "Only int, float, bool and String types are allowed in command prototypes.");

            m_listParameterTypes.Add(type);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Enumerable collection of parameter types defined by
        /// the command prototype.
        /// </summary>
        public List<Type>.Enumerator ParameterTypes
        {
            get { return m_listParameterTypes.GetEnumerator(); }
        }

        /// <summary>
        /// Returns a string representation of the command
        /// prototype.
        /// </summary>
        /// <returns>string representation of the command
        /// prototype.</returns>
        public override String ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(m_strName);
            foreach (Type type in m_listParameterTypes)
            {
                stringBuilder.Append(" {");
                stringBuilder.Append(type.Name);
                stringBuilder.Append("}");
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Name of the command.
        /// </summary>
        public String Name
        {
            get { return m_strName; }
        }

        #endregion
    }
}

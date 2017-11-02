using System;
using System.Collections.Generic;
using System.Text;

namespace WorldOfImagination.Framework.Scripting
{
    /// <summary>
    /// Local or global variable dictionary.
    /// </summary>
    public class VariableDictionary
    {
        #region Private Variables

        private VariableDictionary m_variableDicrionaryGlobal;
        private Dictionary<String, object> m_dictVariables;

        #endregion

        #region Public Methods

        /// <summary>
        /// Constructs a variable dictionary with an optional reference
        /// to a global dictionary. If no parameter is specified, this
        /// dictionary itself is assumed to be the global dictionary
        /// for the owning script manager.
        /// </summary>
        /// <param name="variableDictionaryGlobal"></param>
        public VariableDictionary(VariableDictionary variableDictionaryGlobal)
        {
            m_variableDicrionaryGlobal = variableDictionaryGlobal;
            m_dictVariables = new Dictionary<string, object>();
        }

        /// <summary>
        /// Clears the dictionary from all the variables.
        /// </summary>
        public void Clear()
        {
            m_dictVariables.Clear();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Returns an enumerable collection of variable identifiers.
        /// </summary>
        public Dictionary<String, object>.KeyCollection Identifiers
        {
            get { return m_dictVariables.Keys; }
        }

        /// <summary>
        /// Indicates if a given variable is defined within the dictionary.
        /// If this dictionary is not the global dictionary, it looks
        /// up the variable in the latter.
        /// </summary>
        /// <param name="strIdentifier">Variable identifier to test.</param>
        /// <returns>True if the variable is in the dictionary,
        /// or false otherwise.</returns>
        public bool HasVariable(String strIdentifier)
        {
            if (m_variableDicrionaryGlobal != null)
            {
                // has global reference

                // check if var exists as global
                if (m_variableDicrionaryGlobal.HasVariable(strIdentifier))
                    return true;
                else
                    // otherwise, check local scope
                    return m_dictVariables.ContainsKey(strIdentifier.ToUpper());
            }
            else
                // is global reference - check own scope
                return m_dictVariables.ContainsKey(strIdentifier.ToUpper());
        }

        /// <summary>
        /// Returns a variable value indexed by its identifier. If the
        /// dictionary is local, it refers to the global dictionary.
        /// </summary>
        /// <param name="strIdentifier">Identifier of the variable value
        /// to retrieve.</param>
        /// <returns>Variable value.</returns>
        public object this[String strIdentifier]
        {
            get
            {
                if (m_variableDicrionaryGlobal != null)
                {
                    // has global reference

                    // if in global scope, return value
                    if (m_variableDicrionaryGlobal.HasVariable(strIdentifier))
                        return m_variableDicrionaryGlobal[strIdentifier];
                    else
                    {
                        // otherwise, check local scope
                        if (!m_dictVariables.ContainsKey(strIdentifier.ToUpper()))
                            throw new ScriptException("Variable " + strIdentifier + " not set.");
                        return m_dictVariables[strIdentifier.ToUpper()];
                    }
                }
                else
                {
                    // is already global reference - check own scope
                    if (!m_dictVariables.ContainsKey(strIdentifier.ToUpper()))
                        throw new ScriptException("Global variable " + strIdentifier + " not set.");
                    return m_dictVariables[strIdentifier];
                }
            }
            set
            {
                if (m_variableDicrionaryGlobal != null)
                {
                    // has global reference

                    // if variable is global, update it
                    if (m_variableDicrionaryGlobal.HasVariable(strIdentifier))
                        m_variableDicrionaryGlobal[strIdentifier] = value;
                    else
                        // otherwise, set as local
                        m_dictVariables[strIdentifier.ToUpper()] = value;
                }
                else
                    // no global reference - set as local
                    m_dictVariables[strIdentifier.ToUpper()] = value;
            }
        }

        #endregion
    }
}

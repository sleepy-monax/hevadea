using System;
using System.Collections.Generic;
using System.Text;

namespace WorldOfImagination.Framework.Scripting
{
    /// <summary>
    /// Manager class for defining a script environment and global
    /// variable scope.
    /// </summary>
    public class ScriptManager
    {
        #region Private Variables

        private ScriptLoader m_scriptLoader;
        private VariableDictionary m_variableDictionary;
        private Dictionary<String, CommandPrototype> m_dictCommandPrototypes;

        #endregion

        #region Public Methods

        /// <summary>
        /// Constructs a new script manager.
        /// </summary>
        public ScriptManager()
        {
            m_scriptLoader = new ScriptLoaderDefault();
            m_variableDictionary = new VariableDictionary(null);
            m_dictCommandPrototypes = new Dictionary<string, CommandPrototype>();
        }

        /// <summary>
        /// Registers a new custom command using the given prototype.
        /// </summary>
        /// <param name="commandPrototype">Prototype for the command.</param>
        public void RegisterCommand(CommandPrototype commandPrototype)
        {
            String strKey = commandPrototype.Name.ToUpper();
            if (m_dictCommandPrototypes.ContainsKey(strKey))
                throw new ScriptException(
                    "Command " + commandPrototype.Name + " already registered.");

            m_dictCommandPrototypes[strKey] = commandPrototype;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Script loader bound to the manager. The default loader
        /// retrieves scripts from disk.
        /// </summary>
        public ScriptLoader Loader
        {
            get { return m_scriptLoader; }
            set { m_scriptLoader = value; }
        }

        /// <summary>
        /// Command prototypes registered with the script manager.
        /// </summary>
        public Dictionary<String, CommandPrototype> CommandPrototypes
        {
            get { return m_dictCommandPrototypes; }
        }

        /// <summary>
        /// Global variables currently set in the script manager.
        /// </summary>
        public VariableDictionary GlobalVariables
        {
            get { return m_variableDictionary; }
        }

        #endregion
    }
}

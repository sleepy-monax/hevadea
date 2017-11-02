using System;
using System.Collections.Generic;
using System.Text;

namespace WorldOfImagination.Framework.Scripting
{
    /// <summary>
    /// Custom script command interface.
    /// </summary>
    public interface ScriptHandler
    {
        #region Public Methods

        /// <summary>
        /// Called when a custom command is executed.
        /// </summary>
        /// <param name="scriptContext">Script context to which the handler is bound.</param>
        /// <param name="strCommand">Name of the custom command.</param>
        /// <param name="listParameters">List of parameter values passed from the script.</param>
        void OnScriptCommand(ScriptContext scriptContext, String strCommand, List<object> listParameters);

        /// <summary>
        /// Called when a local or global variable is set or updated.
        /// </summary>
        /// <param name="scriptContext">Script context to which the handler is bound.</param>
        /// <param name="strIdentifier">Variable identifier.</param>
        /// <param name="objectValue">Variable value.</param>
        void OnScriptVariableUpdate(ScriptContext scriptContext, String strIdentifier, object objectValue);

        /// <summary>
        /// Called when an interrupt is generated.
        /// </summary>
        /// <param name="scriptContext">Script context to which the handler is bound.</param>
        void OnScriptInterrupt(ScriptContext scriptContext);

        #endregion
    }
}

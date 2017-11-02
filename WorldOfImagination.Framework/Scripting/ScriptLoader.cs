using System;
using System.Collections.Generic;
using System.Text;

namespace WorldOfImagination.Framework.Scripting
{
    /// <summary>
    /// Interface for custom script loader implementations.
    /// </summary>
    public interface ScriptLoader
    {
        #region Public Methods

        /// <summary>
        /// Implements a script loading function for the given
        /// resource name. The method is used to load primary
        /// scripts and also other scripts referenced using
        /// the INCLUDE script statement.
        /// </summary>
        /// <param name="strResourceName">Name of the script to load.</param>
        /// <returns>List of statement lines constituting the script.</returns>
        List<String> LoadScript(String strResourceName);

        #endregion
    }
}

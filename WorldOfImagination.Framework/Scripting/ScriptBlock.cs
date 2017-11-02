using System;
using System.Collections.Generic;
using System.Text;

namespace WorldOfImagination.Framework.Scripting
{
    /// <summary>
    /// Main execution block, named block or anonymous statement block.
    /// </summary>
    public class ScriptBlock
    {
        #region Private Variables

        private Script m_script;
        private String m_strName;
        private List<Statement> m_listStatements;

        #endregion

        #region Internal Properties

        internal List<Statement> Statements
        {
            get { return m_listStatements; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Constructs a script block for the given script and with
        /// the given name.
        /// </summary>
        /// <param name="script">Script containing the block.</param>
        /// <param name="strName">Name of the block.</param>
        public ScriptBlock(Script script, String strName)
        {
            m_script = script;
            m_strName = strName;
            m_listStatements = new List<Statement>();
        }

        /// <summary>
        /// Returns a string representation of the script block.
        /// </summary>
        /// <returns>String representation of the script block</returns>
        public override string ToString()
        {
            return "Block: " + m_strName;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Script containing the block.
        /// </summary>
        public Script Script
        {
            get { return m_script; }
        }

        /// <summary>
        /// Name of the script block.
        /// </summary>
        public String Name
        {
            get { return m_strName; }
        }

        #endregion
    }
}

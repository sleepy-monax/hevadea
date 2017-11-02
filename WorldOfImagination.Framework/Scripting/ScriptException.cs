using System;
using System.Collections.Generic;
using System.Text;

namespace WorldOfImagination.Framework.Scripting
{
    /// <summary>
    /// The API's exception class for script compilation, runtime
    /// and host usage errors.
    /// </summary>
    public class ScriptException
        : Exception
    {
        #region Private Variables

        private String m_strMessage;
        private Exception m_exceptionInner;
        private Statement m_statement;

        #endregion

        #region Public Methods

        /// <summary>
        /// Constructs an exception without any parameters.
        /// </summary>
        public ScriptException()
        {
            m_strMessage = "";
            m_exceptionInner = null;
            m_statement = null;
        }

        /// <summary>
        /// Constructs an eexception with the given message.
        /// </summary>
        /// <param name="strMessage">Exception message.</param>
        public ScriptException(String strMessage)
        {
            m_strMessage = strMessage;
            m_exceptionInner = null;
            m_statement = null;
        }

        /// <summary>
        /// Constructs an exception with the given message and inner exception.
        /// </summary>
        /// <param name="strMessage">Exception message.</param>
        /// <param name="exceptionInner">Inner exception wrapped by this exception.</param>
        public ScriptException(String strMessage, Exception exceptionInner)
        {
            m_strMessage = strMessage;
            m_exceptionInner = exceptionInner;
            m_statement = null;
        }

        /// <summary>
        /// Constructs an exception with the given message and script statement.
        /// </summary>
        /// <param name="strMessage">Exception message.</param>
        /// <param name="statement">Script statement where the runtime or
        /// compilation exception occured.</param>
        public ScriptException(String strMessage, Statement statement)
        {
            m_strMessage = strMessage;
            m_exceptionInner = null;
            m_statement = statement;
        }

        /// <summary>
        /// Constructs an exception with the given message, script statement
        /// and inner exception.
        /// </summary>
        /// <param name="strMessage">Exception message.</param>
        /// <param name="exceptionInner">Inner exception wrapped by this exception.</param>
        /// <param name="statement">Script statement where the runtime or
        /// compilation exception occured.</param>
        public ScriptException(String strMessage, Exception exceptionInner, Statement statement)
        {
            m_strMessage = strMessage;
            m_exceptionInner = exceptionInner;
            m_statement = Statement;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Exception message.
        /// </summary>
        public override String Message
        {
            get
            {
                if (m_exceptionInner == null)
                    return m_strMessage;
                else
                    return m_strMessage + " Reason: " + m_exceptionInner.Message;
            }
        }

        /// <summary>
        /// Inner exception wrapped by this exception.
        /// </summary>
        public new Exception InnerException
        {
            get { return m_exceptionInner; }
        }

        /// <summary>
        /// Script statement where the runtime or
        /// compilation exception occured.
        /// </summary>
        public Statement Statement
        {
            get { return m_statement; }
        }

        #endregion
    }
}

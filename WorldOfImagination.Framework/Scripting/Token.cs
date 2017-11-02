using System;
using System.Collections.Generic;
using System.Text;

namespace WorldOfImagination.Framework.Scripting
{
    #region Internal Enumerations

    internal enum TokenType
    {
        Identifier,
        Integer,
        Float,
        Boolean,
        String,
        Equals,
        NotEquals,
        Greater,
        Less,
        INCLUDE,
        SETGLOBAL,
        SET,
        ADD,
        SUBTRACT,
        MULTIPLY,
        DIVIDE,
        TO,
        FROM,
        BY,
        IF,
        THEN,
        ELSE,
        ENDIF,
        WHILE,
        ENDWHILE,
        CALL,
        BLOCK,
        ENDBLOCK,
        YIELD
    }

    #endregion

    internal class Token
    {
        #region Private Variables

        private TokenType m_tokenType;
        private object m_objectValue;

        #endregion

        #region Public Methods

        public Token(TokenType tokenType, object objectValue)
        {
            m_tokenType = tokenType;
            m_objectValue = objectValue;
        }

        public override String ToString()
        {
            if (m_tokenType == TokenType.String)
                return m_tokenType + " (\"" + m_objectValue + "\")";
            else
                return m_tokenType + " (" + m_objectValue + ")";
        }

        #endregion

        #region Public Properties

        public TokenType Type
        {
            get { return m_tokenType; }
        }

        public object Value
        {
            get { return m_objectValue; }
        }

        #endregion
    }
}

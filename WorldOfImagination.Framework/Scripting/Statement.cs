using System;
using System.Collections.Generic;
using System.Text;

namespace WorldOfImagination.Framework.Scripting
{
    internal enum ParseState
    {
        Space,
        Identifier,
        NotEqual,
        Integer,
        Float,
        Boolean,
        String,
        StringEscape
    }

    /// <summary>
    /// Enumerated type representing the type of a
    /// particular script statement.
    /// </summary>
    public enum StatementType
    {
        /// <summary>
        /// Blank line.
        /// </summary>
        BlankLine,

        /// <summary>
        /// Comment line.
        /// </summary>
        Comment,

        /// <summary>
        /// Control statement such as IF-THEN, CALL or WHILE 
        /// </summary>
        Control,

        /// <summary>
        /// Custom command.
        /// </summary>
        Command
    }

    /// <summary>
    /// A single script line representation constituting
    /// a statement.
    /// </summary>
    public class Statement
    {
        #region Private Static Variables

        private static Dictionary<String, TokenType> s_dictTokenTypes;

        #endregion

        #region Private Variables

        private Script m_script;
        private int m_iLine;
        private StatementType m_statementType;
        private List<Token> m_listTokens;

        #endregion

        #region Private Methods

        private void InitialiseTokenTypeMap()
        {
            s_dictTokenTypes = new Dictionary<string, TokenType>();

            s_dictTokenTypes["TRUE"] = TokenType.Boolean;
            s_dictTokenTypes["FALSE"] = TokenType.Boolean;
            s_dictTokenTypes["INCLUDE"] = TokenType.INCLUDE;
            s_dictTokenTypes["SETGLOBAL"] = TokenType.SETGLOBAL;
            s_dictTokenTypes["SET"] = TokenType.SET;
            s_dictTokenTypes["ADD"] = TokenType.ADD;
            s_dictTokenTypes["SUBTRACT"] = TokenType.SUBTRACT;
            s_dictTokenTypes["MULTIPLY"] = TokenType.MULTIPLY;
            s_dictTokenTypes["DIVIDE"] = TokenType.DIVIDE;
            s_dictTokenTypes["TO"] = TokenType.TO;
            s_dictTokenTypes["FROM"] = TokenType.FROM;
            s_dictTokenTypes["BY"] = TokenType.BY;
            s_dictTokenTypes["IF"] = TokenType.IF;
            s_dictTokenTypes["THEN"] = TokenType.THEN;
            s_dictTokenTypes["ELSE"] = TokenType.ELSE;
            s_dictTokenTypes["ENDIF"] = TokenType.ENDIF;
            s_dictTokenTypes["WHILE"] = TokenType.WHILE;
            s_dictTokenTypes["ENDWHILE"] = TokenType.ENDWHILE;
            s_dictTokenTypes["CALL"] = TokenType.CALL;
            s_dictTokenTypes["BLOCK"] = TokenType.BLOCK;
            s_dictTokenTypes["ENDBLOCK"] = TokenType.ENDBLOCK;
            s_dictTokenTypes["YIELD"] = TokenType.YIELD;
        }

        private List<Token> GetTokens(String strStatement)
        {
            if (s_dictTokenTypes == null)
                InitialiseTokenTypeMap();

            strStatement += " ";

            List<Token> listTokens = new List<Token>();

            ParseState parseState = ParseState.Space;

            String strToken = null;

            int iIndex = 0;
            while (iIndex < strStatement.Length)
            {
                char chChar = strStatement[iIndex];

                switch (parseState)
                {
                    case ParseState.Space:
                        if (chChar == '_' || Char.IsLetter(chChar))
                        {
                            strToken = "" + chChar;
                            parseState = ParseState.Identifier;
                        }
                        else if (chChar == '-' || Char.IsDigit(chChar))
                        {
                            strToken = "" + chChar;
                            parseState = ParseState.Integer;
                        }
                        else if (chChar == '\"')
                        {
                            strToken = "";
                            parseState = ParseState.String;
                        }
                        else if (chChar == '=')
                        {
                            listTokens.Add(new Token(TokenType.Equals, "="));
                            parseState = ParseState.Space;
                        }
                        else if (chChar == '!')
                        {
                            parseState = ParseState.NotEqual;
                        }
                        else if (chChar == '>')
                        {
                            listTokens.Add(new Token(TokenType.Greater, ">"));
                            parseState = ParseState.Space;
                        }
                        else if (chChar == '<')
                        {
                            listTokens.Add(new Token(TokenType.Less, "<"));
                            parseState = ParseState.Space;
                        }
                        else if (!Char.IsWhiteSpace(chChar))
                            throw new ScriptException("Whitespace expected.");
                        break;
                    case ParseState.NotEqual:
                        if (chChar == '=')
                        {
                            listTokens.Add(new Token(TokenType.NotEquals, "!="));
                            parseState = ParseState.Space;
                        }
                        else if (!Char.IsWhiteSpace(chChar))
                            throw new ScriptException("Invalid carachter '!'.");
                        break;
                    case ParseState.Identifier:
                        if (chChar == '_' || Char.IsLetterOrDigit(chChar))
                            strToken += chChar;
                        else if (Char.IsWhiteSpace(chChar))
                        {
                            String strTokenUpper = strToken.ToUpper();
                            Token token = null;
                            if (s_dictTokenTypes.ContainsKey(strTokenUpper))
                            {
                                TokenType tokenType = s_dictTokenTypes[strTokenUpper];
                                if (strTokenUpper == "TRUE")
                                    token = new Token(tokenType, true);
                                else if (strTokenUpper == "FALSE")
                                    token = new Token(tokenType, false);
                                else
                                    token = new Token(tokenType, strTokenUpper);
                            }
                            else
                                token = new Token(TokenType.Identifier, strToken);

                            listTokens.Add(token);
                            parseState = ParseState.Space;
                        }
                        else if (!Char.IsWhiteSpace(chChar))
                            throw new ScriptException("Whitespace expected.");
                        break;
                    case ParseState.Integer:
                        if (Char.IsDigit(chChar))
                            strToken += chChar;
                        else if (chChar == '.')
                        {
                            strToken += chChar;
                            parseState = ParseState.Float;
                        }
                        else if (Char.IsWhiteSpace(chChar))
                        {
                            listTokens.Add(new Token(TokenType.Integer, int.Parse(strToken)));
                            parseState = ParseState.Space;
                        }
                        else if (!Char.IsWhiteSpace(chChar))
                            throw new ScriptException();
                        break;
                    case ParseState.Float:
                        if (Char.IsDigit(chChar))
                            strToken += chChar;
                        else if (Char.IsWhiteSpace(chChar))
                        {
                            listTokens.Add(new Token(TokenType.Float, float.Parse(strToken)));
                            parseState = ParseState.Space;
                        }
                        else if (!Char.IsWhiteSpace(chChar))
                            throw new ScriptException("Whitespace expected.");
                        break;
                    case ParseState.String:
                        if (chChar == '\"')
                        {
                            listTokens.Add(new Token(TokenType.String, strToken));
                            parseState = ParseState.Space;
                        }
                        else if (chChar == '\\')
                        {
                            parseState = ParseState.StringEscape;
                        }
                        else
                            strToken += chChar;
                        break;
                    case ParseState.StringEscape:
                        if (chChar == '\\')
                            strToken += "\\";
                        else if (chChar == 't')
                            strToken += "\t";
                        else if (chChar == 'n')
                            strToken += "\n";
                        else if (chChar == 'r')
                            strToken += "\r";
                        else if (chChar == '\"')
                            strToken += "\"";
                        else
                            throw new ScriptException("Invalid string escape sequence.");
                        parseState = ParseState.String;
                        break;
                }

                ++iIndex;
            }

            return listTokens;
        }

        private void VerifyIdentifierOrLiteral(Token token)
        {
            if (token.Type != TokenType.Identifier
                && token.Type != TokenType.Integer
                && token.Type != TokenType.Float
                && token.Type != TokenType.Boolean
                && token.Type != TokenType.String)
                throw new ScriptException(
                    "Variable name, number, boolean or string expected.");
        }

        private void VerifyIdentifierOrNumber(Token token)
        {
            if (token.Type != TokenType.Identifier
                && token.Type != TokenType.Integer
                && token.Type != TokenType.Float)
                throw new ScriptException(
                    "Variable name or number expected.");
        }

        private void VerifyIdentifierOrNumberOrString(Token token)
        {
            if (token.Type != TokenType.Identifier
                && token.Type != TokenType.Integer
                && token.Type != TokenType.Float
                && token.Type != TokenType.String)
                throw new ScriptException(
                    "Variable name, number or string expected.");
        }

        private void ParseStatementInclude(List<Token> listTokens)
        {
            if (listTokens.Count < 2)
                throw new ScriptException("Invalid INCLUDE statement.", this);

            Token token = listTokens[1];
            if (token.Type != TokenType.String)
                throw new ScriptException("String expected after INCLUDE command.", this);
        }

        private void ParseStatementSetGlobal(List<Token> listTokens)
        {
            if (listTokens.Count < 4)
                throw new ScriptException("Invalid SETGLOBAL statement.", this);

            Token token = listTokens[1];
            if (token.Type != TokenType.Identifier)
                throw new ScriptException("Variable name expected after SETGLOBAL command.", this);

            token = listTokens[2];
            if (token.Type != TokenType.TO)
                throw new ScriptException("TO keyword expected.", this);

            token = listTokens[3];
            VerifyIdentifierOrLiteral(token);
        }

        private void ParseStatementSet(List<Token> listTokens)
        {
            if (listTokens.Count < 4)
                throw new ScriptException("Invalid SET statement.", this);

            Token token = listTokens[1];
            if (token.Type != TokenType.Identifier)
                throw new ScriptException("Variable name expected after SET command.", this);

            token = listTokens[2];
            if (token.Type != TokenType.TO)
                throw new ScriptException("TO keyword expected.", this);

            token = listTokens[3];
            VerifyIdentifierOrLiteral(token);
        }

        private void ParseStatementAdd(List<Token> listTokens)
        {
            if (listTokens.Count < 4)
                throw new ScriptException("Invalid ADD statement.", this);

            Token token = listTokens[1];
            VerifyIdentifierOrNumberOrString(token);

            token = listTokens[2];
            if (token.Type != TokenType.TO)
                throw new ScriptException("TO keyword expected.", this);

            token = listTokens[3];
            if (token.Type != TokenType.Identifier)
                throw new ScriptException("Variable name expected after TO.", this);
        }

        private void ParseStatementSubtract(List<Token> listTokens)
        {
            if (listTokens.Count < 4)
                throw new ScriptException("Invalid SUBTRACT statement.", this);

            Token token = listTokens[1];
            VerifyIdentifierOrNumber(token);

            token = listTokens[2];
            if (token.Type != TokenType.FROM)
                throw new ScriptException("FROM keyword expected.", this);

            token = listTokens[3];
            if (token.Type != TokenType.Identifier)
                throw new ScriptException("Variable name expected after FROM.", this);
        }

        private void ParseStatementMultiply(List<Token> listTokens)
        {
            if (listTokens.Count < 4)
                throw new ScriptException("Invalid MULTIPLY statement.", this);

            Token token = listTokens[1];
            if (token.Type != TokenType.Identifier)
                throw new ScriptException("Variable name expected after MULTIPLY.", this);

            token = listTokens[2];
            if (token.Type != TokenType.BY)
                throw new ScriptException("BY keyword expected.", this);

            token = listTokens[3];
            VerifyIdentifierOrNumber(token);
        }

        private void ParseStatementDivide(List<Token> listTokens)
        {
            if (listTokens.Count < 4)
                throw new ScriptException("Invalid DIVIDE statement.", this);

            Token token = listTokens[1];
            if (token.Type != TokenType.Identifier)
                throw new ScriptException("Variable name expected after DIVIDE.", this);

            token = listTokens[2];
            if (token.Type != TokenType.BY)
                throw new ScriptException("BY keyword expected.", this);

            token = listTokens[3];
            VerifyIdentifierOrNumber(token);
        }

        private void ParseStatementIf(List<Token> listTokens)
        {
            if (listTokens.Count < 2)
                throw new ScriptException("Variable name, number, boolean or string expected after IF.", this);

            Token token = listTokens[1];
            VerifyIdentifierOrLiteral(token);

            if (listTokens.Count < 3)
                throw new ScriptException("THEN, =, > or < expected in IF.", this);

            token = listTokens[2];
            if (token.Type == TokenType.THEN)
            {
                Token tokenCondition = listTokens[1];
                if (tokenCondition.Type != TokenType.Identifier
                    && tokenCondition.Type != TokenType.Boolean)
                    throw new ScriptException("Variable or boolean expected IF", this);

                if (listTokens.Count > 3)
                    throw new ScriptException("Nothing expected after THEN", this);
            }
            else if (token.Type == TokenType.Equals
                || token.Type == TokenType.NotEquals
                || token.Type == TokenType.Greater
                || token.Type == TokenType.Less)
            {
                if (listTokens.Count < 4)
                    throw new ScriptException("Variable, number, boolean or string expected.", this);
                token = listTokens[3];
                VerifyIdentifierOrLiteral(token);

                if (listTokens.Count < 5 || listTokens[4].Type != TokenType.THEN)
                    throw new ScriptException("THEN expected.", this);

                if (listTokens.Count > 5)
                    throw new ScriptException("Nothing expected after THEN", this);
            }
            else
                throw new ScriptException("=, !=, > or < expected in IF.", this);
        }

        private void ParseStatementElse(List<Token> listTokens)
        {
            if (listTokens.Count > 1)
                throw new ScriptException("Nothing expected after ELSE.", this);
        }

        private void ParseStatementEndIf(List<Token> listTokens)
        {
            if (listTokens.Count > 1)
                throw new ScriptException("Nothing expected after ENDIF.", this);
        }

        private void ParseStatementWhile(List<Token> listTokens)
        {
            if (listTokens.Count < 2)
                throw new ScriptException(
                    "Variable name, number, boolean or string expected after WHILE.",
                    this);

            Token token = listTokens[1];
            VerifyIdentifierOrLiteral(token);

            if (listTokens.Count == 2) return;

            token = listTokens[2];
            if (token.Type == TokenType.Equals
                || token.Type == TokenType.NotEquals
                || token.Type == TokenType.Greater
                || token.Type == TokenType.Less)
            {
                if (listTokens.Count < 4)
                    throw new ScriptException(
                        "Variable, number, boolean or string expected.", this);
                token = listTokens[3];
                VerifyIdentifierOrLiteral(token);

                if (listTokens.Count > 4)
                    throw new ScriptException(
                        "Nothing else expected in WHILE statement.", this);
            }
            else
                throw new ScriptException(
                    "=, !=, > or < expected in WHILE statement.", this);
        }

        private void ParseStatementEndWhile(List<Token> listTokens)
        {
            if (listTokens.Count > 1)
                throw new ScriptException(
                    "Nothing expected after WHILE.", this);
        }

        private void ParseStatementCall(List<Token> listTokens)
        {
            if (listTokens.Count < 2)
                throw new ScriptException(
                    "Invalid CALL statement.", this);

            Token token = listTokens[1];
            if (token.Type != TokenType.Identifier)
                throw new ScriptException(
                    "Block name expected after CALL command.", this);

            // note: block name checks in subsequent pass to allow cross-calls
        }

        private void ParseStatementBlock(List<Token> listTokens)
        {
            if (listTokens.Count < 2)
                throw new ScriptException("Invalid BLOCK statememt.", this);

            Token token = listTokens[1];
            if (token.Type != TokenType.Identifier)
                throw new ScriptException(
                    "Block name expected after BLOCK command.", this);

            // note: duplicate block names checked in subsequent pass
        }

        private void ParseStatementEndBlock(List<Token> listTokens)
        {
            if (listTokens.Count > 1)
                throw new ScriptException("Nothing expected after ENDBLOCK.", this);
        }

        private void ParseStatementYield(List<Token> listTokens)
        {
            if (listTokens.Count > 1)
                throw new ScriptException("Nothing expected after YIELD.", this);
        }

        private void ParseStatement(List<Token> listTokens)
        {
            Token token = listTokens[0];

            switch (token.Type)
            {
                case TokenType.SETGLOBAL:
                    ParseStatementSetGlobal(listTokens);
                    break;
                case TokenType.SET:
                    ParseStatementSet(listTokens);
                    break;
                case TokenType.ADD:
                    ParseStatementAdd(listTokens);
                    break;
                case TokenType.SUBTRACT:
                    ParseStatementSubtract(listTokens);
                    break;
                case TokenType.MULTIPLY:
                    ParseStatementMultiply(listTokens);
                    break;
                case TokenType.DIVIDE:
                    ParseStatementDivide(listTokens);
                    break;
                case TokenType.IF:
                    ParseStatementIf(listTokens);
                    break;
                case TokenType.ELSE:
                    ParseStatementElse(listTokens);
                    break;
                case TokenType.ENDIF:
                    ParseStatementEndIf(listTokens);
                    break;
                case TokenType.WHILE:
                    ParseStatementWhile(listTokens);
                    break;
                case TokenType.ENDWHILE:
                    ParseStatementEndWhile(listTokens);
                    break;
                case TokenType.CALL:
                    ParseStatementCall(listTokens);
                    break;
                case TokenType.BLOCK:
                    ParseStatementBlock(listTokens);
                    break;
                case TokenType.ENDBLOCK:
                    ParseStatementEndBlock(listTokens);
                    break;
                case TokenType.YIELD:
                    ParseStatementYield(listTokens);
                    break;
            }
        }

        #endregion

        #region Internal Properties

        internal List<Token> Tokens
        {
            get { return m_listTokens; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Constructs a statement for the given script, with
        /// the given line number and given statement in string
        /// form. The statement is partially compiled to allow for
        /// efficient execution.
        /// </summary>
        /// <param name="script">Script to which the statement belongs.</param>
        /// <param name="iLine">Line number for the statement.</param>
        /// <param name="strStatement">Statement in uncompiled string form.</param>
        public Statement(Script script, int iLine, String strStatement)
        {
            m_script = script;
            m_iLine = iLine;
            strStatement = strStatement.Trim();

            if (strStatement.Length == 0)
            {
                m_statementType = StatementType.BlankLine;
                return;
            }

            if (strStatement.Length >= 2 && strStatement.StartsWith("//"))
            {
                m_statementType = StatementType.Comment;
                return;
            }

            List<Token> listTokens = GetTokens(strStatement);

            ParseStatement(listTokens);

            m_listTokens = listTokens;

            if (m_listTokens[0].Type == TokenType.Identifier)
                m_statementType = StatementType.Command;
            else
                m_statementType = StatementType.Control;
        }

        /// <summary>
        /// Returns a string representation of the statement.
        /// </summary>
        /// <returns>String representation of the statement</returns>
        public override string ToString()
        {
            if (m_statementType == StatementType.BlankLine)
                return m_iLine + ": (blank line)";

            if (m_statementType == StatementType.Comment)
                return m_iLine + ": (comment)";

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(m_iLine);
            stringBuilder.Append(": ");
            for (int iIndex = 0; iIndex < m_listTokens.Count; iIndex++)
            {
                if (iIndex > 0) stringBuilder.Append(" ");

                Token token = m_listTokens[iIndex];

                String strValue = (token.Type == TokenType.String)
                    ? "\"" + token.Value + "\""
                    : token.Value.ToString();
                stringBuilder.Append(strValue);
            }
            return stringBuilder.ToString();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Script to which this statement belongs.
        /// </summary>
        public Script Script
        {
            get { return m_script; }
        }

        /// <summary>
        /// Line number for the statement within the associated script.
        /// </summary>
        public int Line
        {
            get { return m_iLine; }
            internal set { m_iLine = value; }
        }

        /// <summary>
        /// Statement type (comment, blank line, control statement or
        /// custom command). 
        /// </summary>
        public StatementType Type
        {
            get { return m_statementType; }
        }

        #endregion
    }
}

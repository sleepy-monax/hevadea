using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace WorldOfImagination.Framework.Scripting
{
    #region "Internal Structures"

    internal struct IfBlock
    {
        public Statement m_statementIf;
        public ScriptBlock m_scriptBlockTrue;
        public ScriptBlock m_scriptBlockFalse;
        public int m_iNextStatement;
    }

    internal struct WhileBlock
    {
        public Statement m_statementWhile;
        public ScriptBlock m_scriptBlock;
        public int m_iNextStatement;
    }

    #endregion

    /// <summary>
    /// Class representation of a script containing the compiled statements,
    /// script blocks and other structures used to support execution.
    /// </summary>
    public class Script
    {
        #region Private Variables

        String m_strName;
        ScriptManager m_scriptManager;
        private List<Statement> m_listStatements;
        private ScriptBlock m_scriptBlockMain;
        private Dictionary<String, ScriptBlock> m_dictScriptBlocks;
        private Dictionary<Statement, IfBlock> m_dictIfBlocks;
        private Dictionary<Statement, WhileBlock> m_dictWhileBlocks;

        #endregion

        #region Private Methods

        private void Compile()
        {
            // organise statements into main and other blocks
            m_scriptBlockMain = new ScriptBlock(this, "_MAIN");

            Stack<ScriptBlock> stackScriptBlocks = new Stack<ScriptBlock>();
            stackScriptBlocks.Push(m_scriptBlockMain);
            ScriptBlock scriptBlock = null;

            Stack<IfBlock> stackIfBlocks = new Stack<IfBlock>();
            IfBlock ifBlock;

            Stack<WhileBlock> stackWhileBlocks = new Stack<WhileBlock>();
            WhileBlock whileBlock;

            foreach (Statement statement in m_listStatements)
            {
                // ignore blank lines and comments
                if (statement.Type == StatementType.BlankLine) continue;
                if (statement.Type == StatementType.Comment) continue;

                Token token = statement.Tokens[0];

                switch (token.Type)
                {
                    case TokenType.BLOCK:
                        if (stackScriptBlocks.Count > 1)
                            throw new ScriptException("A BLOCK cannot be defined within another BLOCK, IF or WHILE ",
                                statement);

                        String strBlockName = statement.Tokens[1].Value.ToString();

                        if (m_dictScriptBlocks.ContainsKey(strBlockName.ToUpper()))
                            throw new ScriptException("Block " + strBlockName + " is already defined.",
                                statement);

                        scriptBlock = new ScriptBlock(this, strBlockName);
                        stackScriptBlocks.Push(scriptBlock);

                        break;
                    case TokenType.ENDBLOCK:
                        if (stackScriptBlocks.Count > 2)
                            throw new ScriptException("ENDBLOCK cannot be specified within IF or WHILE.",
                                statement);
                        if (stackScriptBlocks.Count == 1)
                            throw new ScriptException("ENDBLOCK specified without a matching BLOCK.",
                                statement);

                        scriptBlock = stackScriptBlocks.Pop();
                        m_dictScriptBlocks[scriptBlock.Name.ToUpper()] = scriptBlock;

                        break;
                    case TokenType.IF:
                        // place IF statement in current block
                        scriptBlock = stackScriptBlocks.Pop();
                        scriptBlock.Statements.Add(statement);
                        stackScriptBlocks.Push(scriptBlock);

                        scriptBlock = new ScriptBlock(this, "__TEMP_IF_TRUE");
                        stackScriptBlocks.Push(scriptBlock);

                        ifBlock = new IfBlock();
                        ifBlock.m_statementIf = statement;
                        ifBlock.m_scriptBlockTrue = scriptBlock;
                        stackIfBlocks.Push(ifBlock);
                        break;
                    case TokenType.ELSE:
                        if (stackIfBlocks.Count == 0)
                            throw new ScriptException("ELSE without matching IF");

                        // pop true block
                        stackScriptBlocks.Pop();

                        scriptBlock = new ScriptBlock(this, "__TEMP_IF_FALSE");
                        stackScriptBlocks.Push(scriptBlock);

                        ifBlock = stackIfBlocks.Pop();
                        ifBlock.m_scriptBlockFalse = scriptBlock;
                        stackIfBlocks.Push(ifBlock);
                        break;
                    case TokenType.ENDIF:
                        if (stackIfBlocks.Count == 0)
                            throw new ScriptException("ENDIF without matching IF", statement);

                        // pop false block
                        stackScriptBlocks.Pop();

                        // set next statement to next in current block
                        ifBlock = stackIfBlocks.Pop();
                        ifBlock.m_iNextStatement = stackScriptBlocks.Peek().Statements.Count;

                        m_dictIfBlocks[ifBlock.m_statementIf] = ifBlock;

                        break;
                    case TokenType.WHILE:
                        // place WHILE statement in current block
                        scriptBlock = stackScriptBlocks.Pop();
                        scriptBlock.Statements.Add(statement);
                        stackScriptBlocks.Push(scriptBlock);

                        scriptBlock = new ScriptBlock(this, "__TEMP_WHILE");
                        stackScriptBlocks.Push(scriptBlock);

                        whileBlock = new WhileBlock();
                        whileBlock.m_statementWhile = statement;
                        whileBlock.m_scriptBlock = scriptBlock;
                        stackWhileBlocks.Push(whileBlock);
                        break;
                    case TokenType.ENDWHILE:
                        if (stackWhileBlocks.Count == 0)
                            throw new ScriptException("ENDWHILE without matching WHILE", statement);

                        // pop while block
                        stackScriptBlocks.Pop();

                        // set next statement to next in current block
                        whileBlock = stackWhileBlocks.Pop();
                        whileBlock.m_iNextStatement = stackScriptBlocks.Peek().Statements.Count;

                        m_dictWhileBlocks[whileBlock.m_statementWhile] = whileBlock;

                        break;
                    default:
                        // any other statement: place in top frame
                        scriptBlock = stackScriptBlocks.Pop();
                        scriptBlock.Statements.Add(statement);
                        stackScriptBlocks.Push(scriptBlock);
                        break;
                }
            }

            if (stackScriptBlocks.Count > 1)
                throw new ScriptException("Missing ENDBLOCK, ENDIF or ENDWHILE command.");

            // ensure CALLs refer to existing blocks
            foreach (Statement statement in m_listStatements)
            {
                if (statement.Type != StatementType.Control) continue;
                if (statement.Tokens[0].Type != TokenType.CALL) continue;
                String strBlockName = statement.Tokens[1].Value.ToString();
                if (!m_dictScriptBlocks.ContainsKey(strBlockName.ToUpper()))
                    throw new ScriptException("Block " + strBlockName + " not defined.", statement);
            }
        }

        #endregion

        #region Internal Properties

        internal List<Statement> Statements
        {
            get { return m_listStatements; }
        }

        internal Dictionary<Statement, IfBlock> IfBlocks
        {
            get { return m_dictIfBlocks; }
        }

        internal Dictionary<Statement, WhileBlock> WhileBlocks
        {
            get { return m_dictWhileBlocks; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Constructs a new script for the given script manager and
        /// resource name. The name is used to retrieve the script
        /// from disk or from another source through the usage of
        /// custom script loaders.
        /// </summary>
        /// <param name="scriptManager">
        /// Script manager associated with the script.</param>
        /// <param name="strName">Resource name for the script.</param>
        public Script(ScriptManager scriptManager, String strName)
        {
            m_strName = strName;
            m_scriptManager = scriptManager;
            m_listStatements = new List<Statement>();
            m_dictScriptBlocks = new Dictionary<string, ScriptBlock>();
            m_dictIfBlocks = new Dictionary<Statement, IfBlock>();
            m_dictWhileBlocks = new Dictionary<Statement, WhileBlock>();

            // load main script file
            List<String> listStatements
                = m_scriptManager.Loader.LoadScript(strName);

            // parse to statements
            foreach (String strStatement in listStatements)
                m_listStatements.Add(new Statement(this, 0, strStatement));

            // includes dictionary
            Dictionary<String, bool> dictIncludes = new Dictionary<string, bool>();

            // process statements for INCLUDEs
            for (int iIndex = 0; iIndex < m_listStatements.Count; iIndex++)
            {
                // skip anything that is not a control statement
                Statement statement = m_listStatements[iIndex];
                if (statement.Type != StatementType.Control) continue;

                // skip statements that are not INCLUDE
                Token token = statement.Tokens[0];
                if (token.Type != TokenType.INCLUDE) continue;

                // remove INCLUDE statement
                m_listStatements.RemoveAt(iIndex);

                // determine included script name
                String strIncludeName = statement.Tokens[1].Value.ToString();

                // ignore if already included
                if (dictIncludes.ContainsKey(strIncludeName.ToUpper())) continue;

                List<Statement> listStatementsIncluded = new List<Statement>();
                foreach (String strStatement in m_scriptManager.Loader.LoadScript(strIncludeName))
                    listStatementsIncluded.Add(new Statement(this, 0, strStatement));

                m_listStatements.InsertRange(iIndex, listStatementsIncluded);

                // mark include file as included
                dictIncludes[strIncludeName.ToUpper()] = true;
            }

            // renumber lines
            for (int iIndex = 0; iIndex < m_listStatements.Count; iIndex++)
                m_listStatements[iIndex].Line = iIndex + 1;

            Compile();
        }

        /// <summary>
        /// Returns a string representation of the script.
        /// </summary>
        /// <returns>String representation of the script</returns>
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Script: " + m_strName + "\r\n");
            foreach (Statement statement in m_listStatements)
                stringBuilder.Append(statement.ToString() + "\r\n");
            return stringBuilder.ToString();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Script resource name.
        /// </summary>
        public String Name
        {
            get { return m_strName; }
        }

        /// <summary>
        /// Script manager bound to this script.
        /// </summary>
        public ScriptManager Manager
        {
            get { return m_scriptManager; }
        }

        /// <summary>
        /// Main execution block.
        /// </summary>
        public ScriptBlock MainBlock
        {
            get { return m_scriptBlockMain; }
        }

        /// <summary>
        /// Named blocks within the scripts.
        /// </summary>
        public Dictionary<String, ScriptBlock> Blocks
        {
            get { return m_dictScriptBlocks; }
        }

        #endregion
    }
}

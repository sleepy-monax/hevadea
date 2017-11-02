using System;
using System.Collections.Generic;
using System.Text;

namespace WorldOfImagination.Framework.Scripting
{
    internal struct Frame
    {
        public ScriptBlock m_scriptBlock;
        public int m_iNextStatement;

        public Frame(ScriptBlock scriptBlock, int iNextStatement)
        {
            m_scriptBlock = scriptBlock;
            m_iNextStatement = iNextStatement;
        }
    }

    /// <summary>
    /// Execution context for a specific code block within a script. An
    /// instance of this class represents a running instance of the
    /// script. Multiple contexts can be created for any given script,
    /// each of which having its own state and local variable scope.
    /// </summary>
    public class ScriptContext
    {
        #region Private Variables

        private ScriptBlock m_scriptBlock;
        private VariableDictionary m_variableDicitonary;
        private int m_iCurrentBlockSize;
        private Stack<Frame> m_stackFrame;
        private bool m_bInterrupted;
        private ScriptHandler m_scriptHandler;
        private bool m_bInterruptOnCustomCommand;

        #endregion

        #region Private Methods

        private bool CompareParameters(Token tokenParameter1,
            TokenType tokenTypeOperator, Token tokenParameter2)
        {
            object objectValue1 = ResolveValue(tokenParameter1);
            object objectValue2 = ResolveValue(tokenParameter2);

            if (tokenParameter2.Type == TokenType.Identifier)
                objectValue2 = GetVariable(tokenParameter2.Value.ToString());
            else
                objectValue2 = tokenParameter2.Value;

            if (objectValue1.GetType() == typeof(int)
                && objectValue2.GetType() == typeof(int))
            {
                // pure int comparison
                int iValue1 = (int)objectValue1;
                int iValue2 = (int)objectValue2;
                switch (tokenTypeOperator)
                {
                    case TokenType.Equals:
                        return iValue1 == iValue2;
                    case TokenType.NotEquals:
                        return iValue1 != iValue2;
                    case TokenType.Greater:
                        return iValue1 > iValue2;
                    case TokenType.Less:
                        return iValue1 < iValue2;
                }
            }
            else if (objectValue1.GetType() == typeof(bool)
                || objectValue2.GetType() == typeof(bool))
            {
                bool bValue1 = (bool)objectValue1;
                bool bValue2 = (bool)objectValue2;
                switch (tokenTypeOperator)
                {
                    case TokenType.Equals:
                        return bValue1 == bValue2;
                    case TokenType.NotEquals:
                        return bValue1 != bValue2;
                    default:
                        throw new ScriptException("Boolean parameters cannot be compared using > or <.");
                }
            }
            else if (objectValue1.GetType() == typeof(String)
                && objectValue2.GetType() == typeof(String))
            {
                // string comparison
                String strValue1 = (String)objectValue1;
                String strValue2 = (String)objectValue2;
                switch (tokenTypeOperator)
                {
                    case TokenType.Equals:
                        return strValue1.CompareTo(strValue2) == 0;
                    case TokenType.NotEquals:
                        return strValue1.CompareTo(strValue2) != 0;
                    case TokenType.Greater:
                        return strValue1.CompareTo(strValue2) > 0;
                    case TokenType.Less:
                        return strValue1.CompareTo(strValue2) < 0;
                }
            }
            else if ((objectValue1.GetType() == typeof(float)
                || objectValue1.GetType() == typeof(int))
                && (objectValue2.GetType() == typeof(float)
                || objectValue2.GetType() == typeof(int)))
            {
                // pure float, or mixed number comparison
                float fValue1 = (float)objectValue1;
                float fValue2 = (float)objectValue2;
                switch (tokenTypeOperator)
                {
                    case TokenType.Equals:
                        return fValue1 == fValue2;
                    case TokenType.Greater:
                        return fValue1 > fValue2;
                    case TokenType.Less:
                        return fValue1 < fValue2;
                }
            }
            else
                throw new ScriptException("Paramaters cannot be compared.");

            return false;
        }

        private object ResolveValue(Token token)
        {
            switch (token.Type)
            {
                case TokenType.Identifier:
                    return GetVariable(token.Value.ToString());
                case TokenType.Integer:
                case TokenType.Float:
                case TokenType.Boolean:
                case TokenType.String:
                    return token.Value;
                default:
                    throw new ScriptException("Cannot resolve token value for type " + token.Type + ".");
            }
        }

        private object GetVariable(String strIdentifier)
        {
            return m_variableDicitonary[strIdentifier.ToUpper()];
        }

        private void SetVariable(String strIdentifier, object objectValue)
        {
            m_variableDicitonary[strIdentifier] = objectValue;

            // if handler set, notify it
            if (m_scriptHandler != null)
                m_scriptHandler.OnScriptVariableUpdate(this, strIdentifier, objectValue);
        }

        private void ExecuteSetGlobal(Statement statement)
        {
            Token token = statement.Tokens[1];
            String strIdentifier = token.Value.ToString();

            if (!GlobalVariables.HasVariable(strIdentifier)
                && LocalVariables.HasVariable(strIdentifier))
                throw new ScriptException(
                    "Variable " + strIdentifier + " already declared locally.",
                    statement);

            object objectValue = ResolveValue(statement.Tokens[3]);

            GlobalVariables[strIdentifier] = objectValue;

            // if handler set, notify it
            if (m_scriptHandler != null)
                m_scriptHandler.OnScriptVariableUpdate(this, strIdentifier, objectValue);
        }

        private void ExecuteSet(Statement statement)
        {
            List<Token> listTokens = statement.Tokens;
            String strIdentifier = listTokens[1].Value.ToString();
            object objectValue = ResolveValue(listTokens[3]);
            SetVariable(strIdentifier, objectValue);
        }

        private void ExecuteAdd(Statement statement)
        {
            List<Token> listTokens = statement.Tokens;

            object objectToAdd = ResolveValue(listTokens[1]);

            Token tokenVariable = listTokens[3];
            String strIdentifier = tokenVariable.Value.ToString();
            object objectValue = GetVariable(strIdentifier);
            object objectResult = null;

            Type typeValue = objectValue.GetType();
            if (typeValue == typeof(bool))
                throw new ScriptException(
                    "Cannot add a value to a boolean variable.", statement);

            Type typeToAdd = objectToAdd.GetType();

            if (typeValue == typeof(int))
            {
                if (typeToAdd == typeof(int))
                    objectResult = (int)objectValue + (int)objectToAdd;
                else if (typeToAdd == typeof(float))
                    objectResult = (float)((int)objectValue + (float)objectToAdd);
                else // string
                    objectResult = (int)objectValue + (String)objectToAdd;
            }
            else if (typeValue == typeof(float))
            {
                if (typeToAdd == typeof(int))
                    objectResult = (float)((float)objectValue + (int)objectToAdd);
                else if (typeToAdd == typeof(float))
                    objectResult = (float)objectValue + (float)objectToAdd;
                else // string
                    objectResult = (float)objectValue + (String)objectToAdd;
            }
            else // string
            {
                if (typeToAdd == typeof(int))
                    objectResult = (String)objectValue + (int)objectToAdd;
                else if (typeToAdd == typeof(float))
                    objectResult = (String)objectValue + (float)objectToAdd;
                else // string
                    objectResult = (String)objectValue + (String)objectToAdd;
            }

            SetVariable(strIdentifier, objectResult);
        }

        private void ExecuteSubtract(Statement statement)
        {
            List<Token> listTokens = statement.Tokens;

            object objectToSubtract = ResolveValue(listTokens[1]);

            Token tokenVariable = listTokens[3];
            String strIdentifier = tokenVariable.Value.ToString();
            object objectValue = GetVariable(strIdentifier);

            Type typeValue = objectValue.GetType();
            if (typeValue == typeof(bool)
                || typeValue == typeof(String))
                throw new ScriptException(
                    "Cannot subtract a value from a boolean or string variable.",
                    statement);

            if (objectValue.GetType() == typeof(int)
                && objectToSubtract.GetType() == typeof(int))
                // pure int
                SetVariable(strIdentifier,
                    (int)objectValue - (int)objectToSubtract);
            else
            {
                // float or mixed
                float fValue = objectValue.GetType() == typeof(float)
                    ? (float)objectValue : (int)objectValue;

                float fToSubtract = objectToSubtract.GetType() == typeof(float)
                    ? (float)objectToSubtract : (int)objectToSubtract;

                SetVariable(strIdentifier, fValue - fToSubtract);
            }
        }

        private void ExecuteMultiply(Statement statement)
        {
            List<Token> listTokens = statement.Tokens;

            Token tokenVariable = listTokens[1];
            String strIdentifier = tokenVariable.Value.ToString();
            object objectValue = GetVariable(strIdentifier);

            Type typeValue = objectValue.GetType();
            if (typeValue == typeof(bool)
                || typeValue == typeof(String))
                throw new ScriptException(
                    "Cannot multiply boolean or string variable.",
                    statement);

            object objectMultiplier = ResolveValue(listTokens[3]);

            if (objectValue.GetType() == typeof(int)
                && objectMultiplier.GetType() == typeof(int))
                // pure int
                SetVariable(strIdentifier,
                    (int)objectValue * (int)objectMultiplier);
            else
            {
                // float or mixed
                float fValue = objectValue.GetType() == typeof(float)
                    ? (float)objectValue : (int)objectValue;

                float fMultiplier = objectMultiplier.GetType() == typeof(float)
                    ? (float)objectMultiplier : (int)objectMultiplier;

                SetVariable(strIdentifier, fValue * fMultiplier);
            }
        }

        private void ExecuteDivide(Statement statement)
        {
            List<Token> listTokens = statement.Tokens;

            Token tokenVariable = listTokens[1];
            String strIdentifier = tokenVariable.Value.ToString();
            object objectValue = GetVariable(strIdentifier);

            Type typeValue = objectValue.GetType();
            if (typeValue == typeof(bool)
                || typeValue == typeof(String))
                throw new ScriptException(
                    "Cannot divide boolean or string variable.",
                    statement);

            object objectDivider = ResolveValue(listTokens[3]);

            float fDivider = 0.0f;
            if (objectDivider.GetType() == typeof(int))
                fDivider = (int)objectDivider;
            else
                fDivider = (float)objectDivider;

            // check for divide by zero
            if (fDivider == 0.0f)
                throw new ScriptException("Cannot divide by zero.", statement);

            if (objectValue.GetType() == typeof(int)
                && objectDivider.GetType() == typeof(int))
                // pure int
                SetVariable(strIdentifier,
                    (int)objectValue / (int)objectDivider);
            else
            {
                // float or mixed
                float fValue = objectValue.GetType() == typeof(float)
                    ? (float)objectValue : (int)objectValue;

                SetVariable(strIdentifier,
                    fValue / fDivider);
            }
        }

        private void ExecuteIf(Statement statement)
        {
            Script script = m_scriptBlock.Script;

            IfBlock ifBLock = script.IfBlocks[statement];

            // update next index for current frame
            Frame frame = m_stackFrame.Pop();
            frame.m_iNextStatement = ifBLock.m_iNextStatement;
            m_stackFrame.Push(frame);

            // perform comparison
            ScriptBlock scriptBlock = null;
            List<Token> listTokens = statement.Tokens;

            if (listTokens.Count == 3)
            {
                // simple boolean check
                Token token = listTokens[1];

                // literal TRUE or FALSE
                if (token.Type == TokenType.Boolean)
                    scriptBlock = (bool)token.Value
                        ? ifBLock.m_scriptBlockTrue
                        : ifBLock.m_scriptBlockFalse;
                else // is identifier
                {
                    String strIdentifier = token.Value.ToString();
                    object objectValue = GetVariable(strIdentifier);

                    // if bool var, process according to value
                    if (objectValue.GetType() == typeof(Boolean))
                    {
                        scriptBlock = (bool)objectValue
                            ? ifBLock.m_scriptBlockTrue
                            : ifBLock.m_scriptBlockFalse;
                    }
                    else
                        throw new ScriptException("Variable must contain a boolean value.", statement);
                }
            }
            else // comparison
            {
                scriptBlock
                    = CompareParameters(listTokens[1], listTokens[2].Type, listTokens[3])
                        ? ifBLock.m_scriptBlockTrue
                        : ifBLock.m_scriptBlockFalse;
            }

            // push block
            if (scriptBlock != null) // else part is optional
            {
                frame = new Frame(scriptBlock, 0);
                m_stackFrame.Push(frame);
            }
        }

        private void ExecuteWhile(Statement statement)
        {
            Script script = m_scriptBlock.Script;

            WhileBlock whileBlock = script.WhileBlocks[statement];

            // perform comparison
            List<Token> listTokens = statement.Tokens;
            bool bLoopContinue = false;
            if (listTokens.Count == 2)
            {
                // simple boolean check
                Token token = listTokens[1];

                // literal TRUE or FALSE
                if (token.Type == TokenType.Boolean)
                    bLoopContinue = (bool)token.Value;
                else // is identifier
                {
                    String strIdentifier = token.Value.ToString();
                    object objectValue = GetVariable(strIdentifier);

                    // if bool var, process according to value
                    if (objectValue.GetType() == typeof(Boolean))
                        bLoopContinue = (bool)objectValue;
                    else
                        throw new ScriptException("Variable must contain a boolean value.", statement);
                }
            }
            else // comparison
                bLoopContinue = CompareParameters(
                    listTokens[1], listTokens[2].Type, listTokens[3]);

            if (bLoopContinue)
            {
                // keep statement index on while statement
                Frame frame = m_stackFrame.Pop();
                frame.m_iNextStatement--;
                m_stackFrame.Push(frame);

                // push while block
                frame = new Frame(whileBlock.m_scriptBlock, 0);
                m_stackFrame.Push(frame);
            }
            else
            {
                // update next index for current frame
                Frame frame = m_stackFrame.Pop();
                frame.m_iNextStatement = whileBlock.m_iNextStatement;
                m_stackFrame.Push(frame);
            }
        }

        private void ExecuteCall(Statement statement)
        {
            List<Token> listTokens = statement.Tokens;

            String strBlockName = listTokens[1].Value.ToString().ToUpper();
            ScriptBlock scriptBlock = m_scriptBlock.Script.Blocks[strBlockName];
            Frame frameToCall = new Frame(scriptBlock, 0);

            m_stackFrame.Push(frameToCall);
        }

        private void ExecuteYield(Statement statement)
        {
            m_bInterrupted = true;
        }

        private void ExecuteCustom(Statement statement)
        {
            List<Token> listTokens = statement.Tokens;

            Token token = listTokens[0];

            String strCommand = token.Value.ToString();

            ScriptManager scriptManager = m_scriptBlock.Script.Manager;

            if (!scriptManager.CommandPrototypes.ContainsKey(
                strCommand.ToUpper()))
                throw new ScriptException("Command " + strCommand + " not registered.");

            CommandPrototype commandPrototype
                = scriptManager.CommandPrototypes[strCommand.ToUpper()];

            List<object> listParameters = new List<object>();
            for (int iIndex = 1; iIndex < listTokens.Count; iIndex++)
                listParameters.Add(ResolveValue(listTokens[iIndex]));

            try
            {
                commandPrototype.VerifyParameters(listParameters);

                // set interrupt flag to trigger break in
                // continuous execution
                if (m_bInterruptOnCustomCommand)
                    m_bInterrupted = true;

                // pass to handler
                if (m_scriptHandler != null)
                    m_scriptHandler.OnScriptCommand(this,
                        commandPrototype.Name, listParameters);
            }
            catch (Exception exception)
            {
                throw new ScriptException("Invalid command.", exception, statement);
            }
        }

        private void ExecuteNextStatement()
        {
            // reset interrupt
            m_bInterrupted = false;

            Frame frame = m_stackFrame.Pop();
            Statement statement = frame.m_scriptBlock.Statements[frame.m_iNextStatement++];
            m_stackFrame.Push(frame);

            Token token = statement.Tokens[0];
            switch (token.Type)
            {
                // SETGLOBAL
                case TokenType.SETGLOBAL:
                    ExecuteSetGlobal(statement);
                    break;
                // SET
                case TokenType.SET:
                    ExecuteSet(statement);
                    break;
                // ADD
                case TokenType.ADD:
                    ExecuteAdd(statement);
                    break;
                // SUBTRACT
                case TokenType.SUBTRACT:
                    ExecuteSubtract(statement);
                    break;
                // MULTIPLY
                case TokenType.MULTIPLY:
                    ExecuteMultiply(statement);
                    break;
                // DIVIDE
                case TokenType.DIVIDE:
                    ExecuteDivide(statement);
                    break;
                // IF
                case TokenType.IF:
                    ExecuteIf(statement);
                    break;
                // WHILE
                case TokenType.WHILE:
                    ExecuteWhile(statement);
                    break;
                // CALL
                case TokenType.CALL:
                    ExecuteCall(statement);
                    break;
                // YIELD
                case TokenType.YIELD:
                    ExecuteYield(statement);
                    break;
                // custom command
                case TokenType.Identifier:
                    ExecuteCustom(statement);
                    break;
            }

            // if interrupt set and handler set, notify it
            if (m_bInterrupted && m_scriptHandler != null)
                m_scriptHandler.OnScriptInterrupt(this);

            // pop stack frame(s) if end of block reached
            while (m_stackFrame.Count > 0)
            {
                frame = m_stackFrame.Pop();
                if (frame.m_iNextStatement < frame.m_scriptBlock.Statements.Count)
                {
                    m_stackFrame.Push(frame);
                    break;
                }
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Constructs a script context for the given script block.
        /// </summary>
        /// <param name="scriptBlock">Code block from a specific script.</param>
        public ScriptContext(ScriptBlock scriptBlock)
        {
            m_scriptBlock = scriptBlock;
            m_variableDicitonary = new VariableDictionary(
                scriptBlock.Script.Manager.GlobalVariables);
            m_iCurrentBlockSize = scriptBlock.Statements.Count;
            m_stackFrame = new Stack<Frame>();
            m_bInterrupted = false;
            m_scriptHandler = null;
            m_stackFrame.Push(new Frame(scriptBlock, 0));
            m_bInterruptOnCustomCommand = false;
        }

        /// <summary>
        /// Constructs a script context for the main code block of
        /// the given script.
        /// </summary>
        /// <param name="script">Script referenced by the context.</param>
        public ScriptContext(Script script)
            : this(script.MainBlock)
        {
        }

        /// <summary>
        /// Executes the associated script block indefinitely until the
        /// end of the block is reached or an interrupt is generated.
        /// This method may retain control indefinitely if the script
        /// block contains an infinite loop and no YIELD statements
        /// are used within the loop.
        /// </summary>
        /// <returns>Number of statements executed.</returns>
        public int Execute()
        {
            m_bInterrupted = false;
            int iCount = 0;
            while (!Terminated && !m_bInterrupted)
            {
                ExecuteNextStatement();
                ++iCount;
            }
            return iCount;
        }

        /// <summary>
        /// Executes the associated script block for up to the given
        /// maximum of statements. The method may return before this
        /// maximum is reached if the end of the block is reached
        /// or an interrupt is triggered.
        /// </summary>
        /// <param name="iStatements">Maximum number of statements to execute.</param>
        /// <returns>Number of statements executed.</returns>
        public int Execute(int iStatements)
        {
            m_bInterrupted = false;
            int iCount = 0;
            while (iStatements-- > 0 && !Terminated && !m_bInterrupted)
            {
                ExecuteNextStatement();
                ++iCount;
            }
            return iCount;
        }

        /// <summary>
        /// Executes the associated script block for the given time
        /// interval. The method may return earlier if the end of the
        /// code block is reached or an interrupt is generated.
        /// </summary>
        /// <param name="tsInterval">Time slot allowed for the script.</param>
        /// <returns></returns>
        public int Execute(TimeSpan tsInterval)
        {
            DateTime dtIntervalEnd = DateTime.Now + tsInterval;
            m_bInterrupted = false;
            int iCount = 0;
            while (DateTime.Now < dtIntervalEnd && !Terminated && !m_bInterrupted)
            {
                ExecuteNextStatement();
                ++iCount;
            }
            return iCount;
        }

        /// <summary>
        /// Resets the execution state of the script and clears the
        /// local variable scope from all variables.
        /// </summary>
        public void Restart()
        {
            m_variableDicitonary.Clear();
            m_stackFrame.Clear();
            m_stackFrame.Push(new Frame(m_scriptBlock, 0));
            m_bInterrupted = false;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Script block referenced by this context.
        /// </summary>
        public ScriptBlock Block
        {
            get { return m_scriptBlock; }
        }

        /// <summary>
        /// Enable/disable flag for automatic interrupt generation
        /// when custom commands are executed.
        /// </summary>
        public bool InterruptOnCustomCommand
        {
            get { return m_bInterruptOnCustomCommand; }
            set { m_bInterruptOnCustomCommand = value; }
        }

        /// <summary>
        /// Indicates if the context terminated due to an interrupt
        /// or otherwise.
        /// </summary>
        public bool Interrupted
        {
            get { return m_bInterrupted; }
        }

        /// <summary>
        /// Indicates if the end of the reference code block was
        /// reached in the last run.
        /// </summary>
        public bool Terminated
        {
            get { return m_stackFrame.Count == 0; }
        }

        /// <summary>
        /// The next statement in the execution pipeline or null
        /// if there are no statements to execute.
        /// </summary>
        public Statement NextStatement
        {
            get
            {
                if (m_stackFrame.Count == 0)
                    return null;
                int iNextStatement = m_stackFrame.Peek().m_iNextStatement;
                Script script = m_scriptBlock.Script;
                if (iNextStatement >= script.Statements.Count)
                    return null;
                return script.Statements[iNextStatement];
            }
        }

        /// <summary>
        /// Local variable dictionary.
        /// </summary>
        public VariableDictionary LocalVariables
        {
            get { return m_variableDicitonary; }
        }

        /// <summary>
        /// Global variable dictionary. This is also
        /// avialble from the associated script manager.
        /// </summary>
        public VariableDictionary GlobalVariables
        {
            get { return m_scriptBlock.Script.Manager.GlobalVariables; }
        }

        /// <summary>
        /// Script handler for this context.
        /// </summary>
        public ScriptHandler Handler
        {
            get { return m_scriptHandler; }
            set { m_scriptHandler = value; }
        }

        #endregion
    }
}

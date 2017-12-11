using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using WorldOfImagination.Scripting.CodeStruct;
using WorldOfImagination.Scripting.Compiler.Parser.RawStructure;
using WorldOfImagination.Scripting.Runtime;

namespace WorldOfImagination.Scripting.Compiler.Parser
{
    public static class Parser
    {
        public static List<RawStructure.RawFunction> ParserPhase1(List<Token> tokens)
        {
            var functions = new List<RawFunction>();
            string functionName = "";
            List<string> functionArgs = new List<string>();
            List<Token> code = new List<Token>();

            bool isFunction = false;
            bool isCode = false;
            int braceCounter = 0;
            foreach (var t in tokens)
            {
                if (t.Type == TokenType.FUNCTION)
                {
                    isFunction = true;
                    functionName = null;
                    functionArgs = new List<string>();
                    code = new List<Token>();
                }
                else if (isFunction && t.Type == TokenType.FUNCTIONNAME)
                {
                    functionName = t.Content;
                }
                else if (isFunction && t.Type == TokenType.FUNCTIONARGNAME)
                {
                    functionArgs.Add(t.Content);
                }
                else if (t.Type == TokenType.BRACEOPEN)
                {
                    braceCounter++;
                    isCode = true;
                    isFunction = false;
                } 
                else if (t.Type == TokenType.BRACECLOSE)
                {
                    braceCounter--;
                    if (braceCounter == 0)
                    {
                        isCode = false;
                        functions.Add(new RawFunction(functionName, functionArgs, code));
                    }
                }else if (isCode)
                {
                    code.Add(t);
                }
            }

            return functions;
        }

        public static List<Function> ParserPhase2(List<RawFunction> functions, bool showDebug = true)
        {
            
            foreach (var f in functions)
            {
                List<List<Token>> lines = new List<List<Token>>();
                List<Token> currentLine = new List<Token>();
                
                foreach (var t in f.Code)
                {
                    if (t.Type == TokenType.LINEEND)
                    {
                        lines.Add(currentLine);
                        currentLine = new List<Token>();
                        Console.Write(";\n");
                    }
                    else
                    {
                        currentLine.Add(t);
                        Console.Write(t.Type + " ");
                    }
                }   
            }

            return null;
        }
        
        public static Statement ParseStatement(List<Token> Tokens)
        {

            return null;
        }
    }
}
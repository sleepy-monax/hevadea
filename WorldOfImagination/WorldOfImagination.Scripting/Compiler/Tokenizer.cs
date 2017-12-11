using System;
using System.Collections.Generic;

namespace WorldOfImagination.Scripting.Compiler
{
    public static class Tokenizer
    {
        private static readonly Dictionary<string, TokenType> TokenBindDictionary =
            new Dictionary<string, TokenType>
            {
                {"{" ,TokenType.BRACEOPEN},
                {"}" ,TokenType.BRACECLOSE},
                {"(",TokenType.BRACKETOPEN},
                {")" ,TokenType.BRACKETCLOSE},
                {"+" ,TokenType.MATHADD},
                {"-" ,TokenType.MATHSUM},
                {"*" ,TokenType.MATHMULT},
                {"/" ,TokenType.MATHDIV},
                {"%" ,TokenType.MATHMOD},
                {"^" ,TokenType.MATHEXP},
                {"=" ,TokenType.MATHEQUAL},
                {"!=",TokenType.MATHENOTQUAL},
                {">" ,TokenType.MATHEBIGGERTHAN},
                {"<" ,TokenType.MATHSMALLERTHAN},
                {"and" ,TokenType.BOOLAND},
                {"or" ,TokenType.BOOLOR},
                {"not" ,TokenType.BOOLNOT},
                {"function" ,TokenType.kwFUNCTION},
                {"return" ,TokenType.kwRETURN},
                {"end" ,TokenType.kwEND},
                {"if" ,TokenType.kwIF},
                {"else" ,TokenType.kwELSE},
                {"elseif" ,TokenType.kwELSEIF},
                {"for" ,TokenType.kwFOR},
                {"while" ,TokenType.kwWHILE},
                {"do" ,TokenType.kwDO},
                {"true" ,TokenType.kwTRUE},
                {"false" ,TokenType.kwFALSE},
                {"->" ,TokenType.ARROW},
                {";", TokenType.LINEEND},
                {"\"", TokenType.QUOTE}
            };
        
        public static List<Token> TokenizePass1(List<string> strings)
        {
            var tokens = new List<Token>();

            foreach (var s in strings)
            {
                if (TokenBindDictionary.ContainsKey(s))
                {
                    tokens.Add(new Token(TokenBindDictionary[s], s));
                }
                else
                {
                    tokens.Add(new Token(TokenType.RAW, s));
                }
            }
            
            return tokens;
        }

        public static List<Token> TokenizePass2(List<Token> tokens)
        {
            for (var i = 0; i < tokens.Count; i++)
            {
                var t = tokens[i];

                // ReSharper disable once SwitchStatementMissingSomeCases
                switch (t.Type)
                {
                    case TokenType.kwFUNCTION when tokens[i + 1].Type == TokenType.RAW:
                        tokens[i + 1].Type = TokenType.FUNCTIONNAME;
                        break;
                    case TokenType.ARROW when tokens[i + 1].Type == TokenType.RAW:
                        tokens[i + 1].Type = TokenType.VARNAME;
                        break;
                    case TokenType.RAW when tokens[i-1].Type == TokenType.QUOTE  && tokens[i+1].Type == TokenType.QUOTE:
                        t.Type = TokenType.valSTRING;
                        break;
                }
            }

            return tokens;
        }
    }
}
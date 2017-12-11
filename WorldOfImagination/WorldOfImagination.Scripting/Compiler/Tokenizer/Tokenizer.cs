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
                {"=" ,TokenType.MATHEQUAL},
                {"!=",TokenType.MATHENOTQUAL},
                {">" ,TokenType.BIGGER_THAN},
                {"<" ,TokenType.SMALLER_THAN},
                {">=" ,TokenType.BIGGER_OR_EQUAL},
                {"<=" ,TokenType.SMALLER_OR_EQUAL},
                {"and" ,TokenType.AND},
                {"or" ,TokenType.OR},
                {"not" ,TokenType.NOT},
                {"fun" ,TokenType.FUNCTION},
                {"return" ,TokenType.RETURN},
                {"end" ,TokenType.END},
                {"if" ,TokenType.IF},
                {"else" ,TokenType.ELSE},
                {"elseif" ,TokenType.ELSEIF},
                {"for" ,TokenType.FOR},
                {"while" ,TokenType.WHILE},
                {"do" ,TokenType.DO},
                {"true" ,TokenType.TRUE},
                {"false" ,TokenType.FALSE},
                {"->" ,TokenType.ARROW},
                {";", TokenType.LINEEND},
                {"\"", TokenType.QUOTE},
                {",", TokenType.COMMA}
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

            var braceCounter = 0;
            var bracketCounter = 0;
            var isFunctionHeader = false;
            for (var i = 0; i < tokens.Count; i++)
            {
                var t = tokens[i];

                switch (t.Type)
                {
                    case TokenType.FUNCTION when tokens[i + 1].Type == TokenType.RAW:
                        tokens[i + 1].Type = TokenType.FUNCTIONNAME;
                        isFunctionHeader = true;
                        break;
                    case TokenType.ARROW when tokens[i + 1].Type == TokenType.RAW:
                        tokens[i + 1].Type = TokenType.VARNAME;
                        break;
                    case TokenType.RAW when tokens[i-1].Type == TokenType.QUOTE  && tokens[i+1].Type == TokenType.QUOTE:
                        t.Type = TokenType.STRING;
                        break;
                        
                    case TokenType.RAW when tokens[i+1].Type == TokenType.BRACKETOPEN:
                        t.Type = TokenType.FUNCTIONNAME;
                        break;
                        
                    case TokenType.RAW when isFunctionHeader && bracketCounter == 1:
                        t.Type = TokenType.FUNCTIONARGNAME;
                        break;
                    case TokenType.BRACEOPEN:
                        isFunctionHeader = false;
                        braceCounter++;
                        break;
                    case TokenType.BRACECLOSE:
                        braceCounter--;
                        break;
                    case TokenType.BRACKETOPEN:
                        bracketCounter++;
                        break;
                    case TokenType.BRACKETCLOSE:
                        bracketCounter--;
                        break;
                    case TokenType.QUOTE:
                        break;
                    case TokenType.MATHADD:
                        break;
                    case TokenType.MATHSUM:
                        break;
                    case TokenType.MATHMULT:
                        break;
                    case TokenType.MATHDIV:
                        break;
                    case TokenType.MATHMOD:
                        break;
                    case TokenType.MATHEQUAL:
                        break;
                    case TokenType.MATHENOTQUAL:
                        break;
                    case TokenType.BIGGER_THAN:
                        break;
                    case TokenType.SMALLER_THAN:
                        break;
                    case TokenType.AND:
                        break;
                    case TokenType.OR:
                        break;
                    case TokenType.NOT:
                        break;
                    case TokenType.RETURN:
                        break;
                    case TokenType.END:
                        break;
                    case TokenType.IF:
                        break;
                    case TokenType.ELSE:
                        break;
                    case TokenType.ELSEIF:
                        break;
                    case TokenType.FOR:
                        break;
                    case TokenType.WHILE:
                        break;
                    case TokenType.DO:
                        break;
                    case TokenType.TRUE:
                        break;
                    case TokenType.FALSE:
                        break;
                    case TokenType.INT:
                        break;
                    case TokenType.BOOL:
                        break;
                    case TokenType.STRING:
                        break;
                    case TokenType.FUNCTIONNAME:
                        break;
                    case TokenType.FUNCTIONARGNAME:
                        break;
                    case TokenType.VARNAME:
                        break;
                    case TokenType.LINEEND:
                        break;
                    case TokenType.COMMA:
                        break;
                    case TokenType.RAW:
                        t.Type = int.TryParse(t.Content, out var _) 
                                    ? TokenType.INT 
                                    : TokenType.VARNAME;
                        break;
                }
            }

            return tokens;
        }
    }
}
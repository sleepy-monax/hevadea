using System;
using System.Collections.Generic;
using System.Linq;

namespace WorldOfImagination.Scripting.Compiler
{
    public class Lexer
    {
        private string currentToken = "";
        
        
        public Lexer()
        {
            
        }

        private static readonly string[] KeyWords =
        {
            "fun",
            "return",
            "end",
            
            "if",
            "else",
            "elseif",
            
            "for",
            "while",
            "do"
        };

        private static readonly string[] Operator = 
        {
            "^", // exposant
            "*", // multiplication
            "/", // Division
            "%", // modulo
            "+", // add
            "-", // minus
            "=", // equal
            "!=",// not equal
            ">", // Bigger
            "<", // Smaller
            ">=",// Bigger or equal
            "<=",// Smalller or equal 
            "and", // bool and
            "or",  // bool or
            "xor", // bool xor
            "->"   // Variable assignement
            
            /* NOTES: operator like min, max, sqrt, not... Will be buildin functions */
        };

        private static readonly char[] Separator =
        {
            '{', '}', '[', ']', '(', ')', ';', ','
        };
        
        private static string PrepareString(string str)
        {
            return str.Replace("\r\n", "").Replace("\n", ""); // Remove new line char.
        }
        
        public List<Token> Lexe(string str)
        {
            var tokens = new List<Token>();

            var isString = false;
            var ignoreSpace = true;
            
            var token = "";
            
            foreach (char c in PrepareString(str))
            {
                var tokenEnd = false;
                var tokenType = TokenType.None;
                var ignore = false;
                
                if (c == '"')
                {
                    if (isString)
                    {
                        tokenEnd = true;
                        tokenType = TokenType.Literal;
                        
                    }
                    else
                    {
                        ignore = true;
                    }
                    
                    isString = !isString;
                    ignoreSpace = !isString;
                }

                if (!isString && Separator.Contains(c))
                {
                    tokenEnd = true;
                    tokenType = TokenType.Separator;
                }
                
                var saveToken = (c != ' ' || !ignoreSpace) && (isString || Separator.Contains(c)) && !ignore;
                
                if (saveToken)
                {
                     
                }

                token += c;
                
                if (tokenEnd && !string.IsNullOrEmpty(token))
                {
                    tokens.Add(new Token(tokenType, token));
                    token = "";
                }
                
            }
            
            return tokens;      
        }
    }
}
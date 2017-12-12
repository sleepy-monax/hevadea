using System;
using System.Collections.Generic;
using System.Linq;

namespace WorldOfImagination.Scripting.Compiler
{
    public class Lexer
    {
        private string currentToken = "";
        public List<Token> Tokens;

        private List<string> RawToken;
        private int currentIndex = 0;
        private bool isString = false;

        public Lexer(List<string> rawToken)
        {
            Tokens = new List<Token>();
            RawToken = rawToken;
        }

        internal void PushToken(TokenType type, string content)
        {
            Tokens.Add(new Token(type, content));
            currentToken = "";
        }

        public void Lexe()
        {
            bool isString = false;
            foreach (var i in RawToken)
            {
                if (i.IsKeyword()) PushToken(TokenType.Keyword, i);
                else if (i.IsOperator()) PushToken(TokenType.Operator, i);
                else if (i.IsSeparator()) PushToken(TokenType.Separator, i);
                else if (i == "\"") isString = !isString;
                else if (i.IsInt() || i.IsBool() || isString) PushToken(TokenType.Literal, i);
                else PushToken(TokenType.Identifier, i);

            }
        }
    }
}
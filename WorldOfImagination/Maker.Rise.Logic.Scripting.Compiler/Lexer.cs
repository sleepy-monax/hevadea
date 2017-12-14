using System.Collections.Generic;

namespace Maker.Rise.Logic.Scripting.Compiler
{
    public class Lexer
    {
        public List<Token> Tokens;

        private readonly List<string> RawToken;

        public Lexer(List<string> rawToken)
        {
            Tokens = new List<Token>();
            RawToken = rawToken;
        }

        internal void PushToken(TokenType type, string content)
        {
            Tokens.Add(new Token(type, content));
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
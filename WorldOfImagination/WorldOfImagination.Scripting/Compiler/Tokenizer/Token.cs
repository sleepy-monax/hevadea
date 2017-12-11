using System;

namespace WorldOfImagination.Scripting.Compiler
{
    public class Token : ICloneable
    {
        public TokenType       Type;
        public readonly string Content;
        public int Priority = 0; // use for statement.
        
        public Token(TokenType type, string content)
        {
            Type    = type;
            Content = content;
        }

        public object Clone()
        {
            return new Token(Type, Content);
        }
    }
}
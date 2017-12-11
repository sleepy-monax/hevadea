namespace WorldOfImagination.Scripting.Compiler
{
    public class Token
    {
        public TokenType       Type;
        public readonly string Content;
        
        public Token(TokenType type, string content)
        {
            Type    = type;
            Content = content;
        }
    }
}
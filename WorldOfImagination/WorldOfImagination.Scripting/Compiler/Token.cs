namespace WorldOfImagination.Scripting.Compiler
{
    public enum TokenType
    {
        None,
        Identifier, 	// x, color, UP
        Keyword, 	    // if, while, return
        Separator,  	//  }, (, ;
        Operator, 	    //+, <, =
        Literal,	    //true, 6.02e23, "music"
        Comment, 	    // must be negative, /* Retrieves user data */
    }

    public class Token
    {
        public Token(TokenType type, string content)
        {
            Type = type;
            Content = content;
        }

        public readonly TokenType Type;
        public readonly string Content;
    }
}
using System.Collections.Generic;

namespace Maker.Rise.Logic.Scripting.Compiler
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
        
        SyntaxeTreeNode,
        
        Indexer,
        Block,
    }

    public class Token
    {
        public Token(TokenType type, string content)
        {
            Type = type;
            Content = content;
            Childs = new List<Token>();
        }

        public readonly TokenType Type;
        public readonly string Content;
        public readonly List<Token> Childs;
    }
}
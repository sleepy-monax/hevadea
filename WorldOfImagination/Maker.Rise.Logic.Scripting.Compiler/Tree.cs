using System.Collections.Generic;
using WorldOfImagination.Scripting;

namespace Maker.Rise.Logic.Scripting.Compiler
{
    public class Tree
    {
        public Token Root;
        private readonly List<Token> tokens;

        public Tree(List<Token> tokens)
        {
            this.tokens = tokens;
        }

        public void Build()
        {
            
            var stack = new Stack<Token>();
            var parentToken = new Token(TokenType.SyntaxeTreeNode, "root");
            Root = parentToken;
            foreach (var t in tokens)
            {
                if (t.Content.IsOpenSeparator())
                {
                    var newParent = new Token(TokenType.SyntaxeTreeNode, "block");
                    parentToken.Childs.Add(newParent);
                    stack.Push(parentToken);
                    parentToken = newParent;
                }
                
                parentToken.Childs.Add(t);

                if (t.Content.IsCloseSeparator())
                {
                    parentToken = stack.Pop();
                }
            }
        }    
    }
}
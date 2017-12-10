using WorldOfImagination.Game.Scripting.CodeStruct;

namespace WorldOfImagination.Game.Scripting
{
    public class Compiler
    {
        public Script Compile(string code)
        {
            var script = new Script();
            var tokens = Tokenizer.Tokenize(code);
            var depth = 0;
            for (var i = 0; i < tokens.Count; i++)
            {
                var t = tokens[i];
                if (t == "{") depth++;
                else if (t == "}") depth--;
                else
                {
                    
                }

            }
            
            return script;
        }
    }
}
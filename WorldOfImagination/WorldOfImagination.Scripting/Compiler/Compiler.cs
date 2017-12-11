using WorldOfImagination.Scripting.Runtime;

namespace WorldOfImagination.Scripting.Compiler
{
    public class Compiler
    {
        public State Compile(string code)
        {
            var state = new State();
            var Tokens = Tokenizer.Tokenize(Spliter.SplitToken(code));
            
            return state;
        }
    }
}
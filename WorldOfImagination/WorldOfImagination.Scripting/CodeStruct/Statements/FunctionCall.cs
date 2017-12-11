using System.IO;
using WorldOfImagination.Scripting.Runtime;

namespace WorldOfImagination.Scripting.CodeStruct.Statements
{
    public class FunctionCall : Statement
    {
        public readonly string FunctionName;
        public readonly Statement[] Arguments;
        
        public FunctionCall(string functionName, params Statement[] arguments)
        {
            FunctionName = functionName;
            Arguments = arguments;
        }

        public override Value Evaluate(State state)
        {
            var args = new Value[Arguments.Length];
            
            for (var i = 0; i < Arguments.Length; i++)
            {
                args[i] = Arguments[i].Evaluate(state);
            }

            return state.GetFunction(FunctionName).Call(state, args);
        }
    }
}
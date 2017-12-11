using WorldOfImagination.Scripting.Runtime;

namespace WorldOfImagination.Scripting.CodeStruct
{
    public abstract class Statement
    {
        public abstract Value Evaluate(State state);
    }
}
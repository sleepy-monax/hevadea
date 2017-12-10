namespace WorldOfImagination.Game.Scripting.CodeStruct
{
    public abstract class Statement
    {
        public abstract Value Evaluate(Scope scope);
    }
}
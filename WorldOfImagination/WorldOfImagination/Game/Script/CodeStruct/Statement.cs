namespace WorldOfImagination.Game.Script.CodeStruct
{
    public abstract class Statement
    {
        public abstract Value Evaluate(Scope scope);
    }
}
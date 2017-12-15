namespace Maker.Rise.Logic.Scripting.Runtime
{
    public interface IStatement
    {
        IValue Evaluate(State state);
    }
}
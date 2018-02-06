using Maker.Hevadea.Game;

namespace Maker.Hevadea.WorldGenerator.Features.Functions
{
    public class CombinedFunction : FunctionBase
    {
        FunctionBase FuncA;
        FunctionBase FuncB;

        public CombinedFunction(FunctionBase funcA, FunctionBase funcB)
        {
            FuncA = funcA;
            FuncB = funcB;
        }

        public override double Compute(double x, double y, Generator gen, LevelGenerator levelGen, Level level)
        {
            return FuncA.Compute(x, y, gen, levelGen, level) * FuncB.Compute(x, y, gen, levelGen, level);
        }
    }
}

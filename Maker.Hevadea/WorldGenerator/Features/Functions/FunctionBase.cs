using Maker.Hevadea.Game;

namespace Maker.Hevadea.WorldGenerator.Features.Functions
{
    public abstract class FunctionBase
    {
        public abstract double Compute(double x, double y, Generator gen, LevelGenerator levelGen, Level level);
    }
}

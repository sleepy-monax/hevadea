using Maker.Hevadea.Game;

namespace Maker.Hevadea.WorldGenerator.Features.Functions
{
    public interface IFunction
    {
        double Compute(double x, double y, Generator gen, LevelGenerator levelGen, Level level);
    }
}
using Hevadea.Game;

namespace Hevadea.WorldGenerator.Functions
{
    public interface IFunction
    {
        double Compute(double x, double y, Generator gen, LevelGenerator levelGen, Level level);
    }
}
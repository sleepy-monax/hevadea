using Hevadea.Worlds;
using Hevadea.Worlds.Level;

namespace Hevadea.WorldGenerator.Functions
{
    public interface IFunction
    {
        double Compute(double x, double y, Generator gen, LevelGenerator levelGen, Level level);
    }
}
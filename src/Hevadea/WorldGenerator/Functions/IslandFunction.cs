using Hevadea.Framework.Utils;
using Hevadea.Worlds;

namespace Hevadea.WorldGenerator.Functions
{
    public class IslandFunction : IFunction
    {
        public double Compute(double x, double y, Generator gen, LevelGenerator levelGen, Level level)
        {
            var ground = gen.Perlin.OctavePerlin(x / 50d, y / 50d, 0, 10, 0.5);
            return ground * Mathf.Min(1, Mathf.Sin((float)x / gen.Size * Mathf.PI) * Mathf.Sin((float)y / gen.Size * Mathf.PI) * 4);
        }
    }
}
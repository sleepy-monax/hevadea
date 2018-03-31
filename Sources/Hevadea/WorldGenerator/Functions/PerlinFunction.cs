using Hevadea.Worlds;

namespace Hevadea.WorldGenerator.Functions
{
    public class PerlinFunction : IFunction
    {
        private readonly int _octaves = 1;
        private readonly double _persistance = 1;
        private readonly double _scretch = 10d;


        public PerlinFunction(int octaves = 1, double persistance = 1, double scretch = 10)
        {
            _octaves = octaves;
            _persistance = persistance;
            _scretch = scretch;
        }


        public double Compute(double x, double y, Generator gen, LevelGenerator levelGen, Level level)
        {
            return gen.Perlin.OctavePerlin(x / _scretch, y / _scretch, 0, _octaves, _persistance);
        }
    }
}
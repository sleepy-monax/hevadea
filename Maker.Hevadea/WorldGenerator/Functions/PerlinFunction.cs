using Maker.Hevadea.Game;
using Maker.Rise.Utils;

namespace Maker.Hevadea.WorldGenerator.Functions
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
            return Perlin.OctavePerlin((x + (gen.Seed ^ 2)) / _scretch, (y + (gen.Seed ^ 2)) / _scretch, 0, _octaves,
                _persistance);
        }
    }
}
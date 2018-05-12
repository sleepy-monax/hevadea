using Hevadea.Worlds;

namespace Hevadea.WorldGenerator.Functions
{
    public class PerlinFunction : IFunction
    {
        private int _octaves;
        private double _persistance, _scretch, _offsetX, _offsetY;

        public PerlinFunction(int octaves = 1, double persistance = 1, double scretch = 10, double offsetX = 0, double offsetY = 0)
        {
            _octaves = octaves;
            _persistance = persistance;
            _scretch = scretch;

            _offsetX = offsetX;
            _offsetY = offsetY;
        }

        public double Compute(double x, double y, Generator gen, LevelGenerator levelGen, Level level)
        {
            return gen.Perlin.OctavePerlin((x + _offsetX) / _scretch,
                                           (y + _offsetY) / _scretch, 0, _octaves, _persistance);
        }
    }
}
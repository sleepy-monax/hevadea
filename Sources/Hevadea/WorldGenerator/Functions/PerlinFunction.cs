using Hevadea.Worlds;
using Hevadea.Worlds.Level;

namespace Hevadea.WorldGenerator.Functions
{
    public class PerlinFunction : IFunction
    {
        int _octaves;
        double _persistance;
        double _scretch;
		double _offsetX;
		double _offsetY;
        

        public PerlinFunction(int octaves = 1, double persistance = 1, double scretch = 10, double offsetX = 0, double offsetY = 0)
        {
            _octaves = octaves;
            _persistance = persistance;
            _scretch = scretch;
        }


        public double Compute(double x, double y, Generator gen, LevelGenerator levelGen, Level level)
        {
			return gen.Perlin.OctavePerlin((x + _offsetX) / _scretch,
			                               (y + _offsetY) / _scretch, 0, _octaves, _persistance);
        }
    }
}
using Hevadea.Framework;
using Hevadea.Worlds;

namespace Hevadea.WorldGenerator
{
    public delegate double GeneratorFunctions(double x, double y, Generator gen, LevelGenerator levelGen, Level level);

    public static class Functions
    {
        public static GeneratorFunctions Island()
        {
            return new GeneratorFunctions((x, y, gen, levelgen, level) =>
            {
                var ground = gen.Noise.Generate(x / 50d, y / 50d, 0, 10, 0.5);
                return ground * Mathf.Min(1,
                           Mathf.Sin((float) x / gen.Size * Mathf.PI) * Mathf.Sin((float) y / gen.Size * Mathf.PI) * 4);
            });
        }

        public static GeneratorFunctions Flat(float value)
        {
            return new GeneratorFunctions((x, y, gen, levelgen, level) => { return value; });
        }

        public static GeneratorFunctions Perlin(int octaves = 1, double persistance = 1, double scretch = 10,
            double offsetX = 0, double offsetY = 0)
        {
            return new GeneratorFunctions((x, y, gen, levelgen, level) =>
            {
                return gen.Noise.Generate((x + offsetX) / scretch, (y + offsetY) / scretch, 0, octaves,
                    persistance);
            });
        }

        public static GeneratorFunctions Combine(GeneratorFunctions funA, GeneratorFunctions funB)
        {
            return new GeneratorFunctions((x, y, gen, levelgen, level) =>
            {
                return funA(x, y, gen, levelgen, level) * funB(x, y, gen, levelgen, level);
            });
        }
    }
}
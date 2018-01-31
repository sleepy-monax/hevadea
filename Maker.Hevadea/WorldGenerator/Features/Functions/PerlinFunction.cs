using Maker.Hevadea.Game;
using Maker.Rise.Utils;

namespace Maker.Hevadea.WorldGenerator.Features.Functions
{
    public class PerlinFunction : FunctionBase
    {
        public int Octaves { get; set; } = 1;
        public double Persistance { get; set; } = 1;
        public double Scretch { get; set; } = 10d;


        public PerlinFunction() { /* Do nothing */ }
        public PerlinFunction(int octaves, double persistance, double scretch)
        {
            Octaves = octaves;
            Persistance = persistance;
            Scretch = scretch;
        }


        public override double Compute(double x, double y, Generator gen, LevelGenerator levelGen, Level level)
        => Perlin.OctavePerlin((x + (gen.Seed^2)) / Scretch, (y +(gen.Seed^2)) / Scretch, 0, Octaves, Persistance);
    }
}

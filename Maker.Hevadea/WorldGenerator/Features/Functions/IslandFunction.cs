using System;
using Maker.Hevadea.Game;
using Maker.Rise.Utils;

namespace Maker.Hevadea.WorldGenerator.Features.Functions
{
    public class IslandFunction : FunctionBase
    {
        public override double Compute(double x, double y, Generator gen, LevelGenerator levelGen, Level level)
        {
            var ground = Perlin.OctavePerlin((x / 50d) + gen.Seed, (y / 50d) + gen.Seed, 0, 10, 0.5);
            return ground * Math.Min(1d, Math.Sin(((float)x / gen.Size) * Math.PI) 
                                       * Math.Sin(((float)y / gen.Size) * Math.PI) 
                                       * 4);
        }
    }
}

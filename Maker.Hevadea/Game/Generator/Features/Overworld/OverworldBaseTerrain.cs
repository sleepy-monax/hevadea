using Maker.Hevadea.Game.Registry;
using Maker.Rise.Utils;
using System;

namespace Maker.Hevadea.Game.Generator.Features.Overworld
{
    class OverworldBaseTerrain : GeneratorFeature
    {
        public OverworldBaseTerrain() : base(nameof(OverworldBaseTerrain))
        {
        }

        public override void ApplyInternal(Level level, GeneratorBase generator)
        {
            for (int x = 0; x < generator.LevelSize; x++)
            {
                for (int y = 0; y < generator.LevelSize; y++)
                {
                    double seed = generator.Seed;

                    var groundLevel = Perlin.OctavePerlin((x / 50d) + seed, (y / 50d) + seed, 0, 10, 0.5);

                    groundLevel = groundLevel * Math.Min(1d,
                                      Math.Sin(((float) x / generator.LevelSize) * Math.PI)
                                      * Math.Sin(((float) y / generator.LevelSize) * Math.PI) * 4);

                    var montains = Perlin.OctavePerlin(x / 20d + seed, y / 20d + seed, 0, 2, 0.5);
                    var biomes = Perlin.OctavePerlin(x / 30d + seed, y / 30d + seed, 0, 1, 1);

                    if (groundLevel > 1d)
                    {
                        if (montains > 0.7)
                        {
                            level.SetTile(x, y, TILES.GRASS);
                        }
                        else
                        {
                            level.SetTile(x, y, TILES.ROCK);
                        }
                    }
                    else if (groundLevel > 0.90)
                    {
                        if (biomes > 0.5d)
                        {
                            level.SetTile(x, y, TILES.GRASS);
                        }
                        else
                        {
                            level.SetTile(x, y, TILES.SAND);
                        }
                    }
                    else
                    {
                        level.SetTile(x, y, TILES.WATER);
                    }
                }
            }
        }
    }
}
using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.Tiles;
using Maker.Rise.Utils;
using System;

namespace Maker.Hevadea.Game.LevelGen.Features.Overworld
{
    public class GrassFeature : GeneratorFeature
    {
        public GrassFeature() : base("Grass")
        {
        }

        public override void ApplyInternal(Level level, Generator generator)
        {
            Random rnd = new Random(generator.Seed);

            for (int x = 0; x < generator.LevelSize; x++)
            {
                for (int y = 0; y < generator.LevelSize; y++)
                {
                    var v = Perlin.OctavePerlin((x / 10d) + generator.Seed, (y / 10d) + generator.Seed, 0, 10, 0.5);
                    if (level.GetTile(x, y).ID == Tile.Grass.ID & (v > 1) & (rnd.Next(5) == 1))
                    {
                        var grass = new GrassEntity();
                        level.AddEntity(grass);
                        grass.SetPosition(x * ConstVal.TileSize + rnd.Next(0, 4), y * ConstVal.TileSize + rnd.Next(0, 4));
                    }
                }
            }
        }
    }
}
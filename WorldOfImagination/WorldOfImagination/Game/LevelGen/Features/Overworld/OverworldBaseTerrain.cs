using Maker.Rise.Utils;
using WorldOfImagination.Game.Tiles;

namespace WorldOfImagination.Game.LevelGen.Features.Overworld
{
    class OverworldBaseTerrain : GeneratorFeature
    {
        public OverworldBaseTerrain() : base(nameof(OverworldBaseTerrain))
        {

        }

        public override void ApplyInternal(Level level, Generator generator)
        {
            for (int x = 0; x < generator.LevelSize; x++)
            {
                for (int y = 0; y < generator.LevelSize; y++)
                {
                    var groundLevel = Perlin.OctavePerlin(x / 50f, y / 50f, generator.Seed ^ generator.Seed, 10, 0.5);
                    var biomsVariant = Perlin.OctavePerlin(x / 20f, y / 20f, generator.Seed ^ generator.Seed, 2, 0.5);

                    if (groundLevel > 1)
                    {

                        if (biomsVariant > 0.7)
                        {
                            level.SetTile(x, y, Tile.Grass.ID);
                        }
                        else
                        {
                            level.SetTile(x, y, Tile.Rock.ID);
                        }

                    }
                    else if (groundLevel > 0.9)
                    {
                        if (biomsVariant > 0.5)
                        {
                            level.SetTile(x, y, Tile.Grass.ID);
                        }
                        else
                        {
                            level.SetTile(x, y, Tile.Sand.ID);
                        }
                    }
                    else
                    {
                        level.SetTile(x, y, Tile.Water.ID);
                    }

                }
            }
        }
    }
}

using System;
using WorldOfImagination.Game.Tiles;

namespace WorldOfImagination.Game.LevelGen.Features.Overworld
{
    public class AbandonedHouseFeature : GeneratorFeature
    {
        public AbandonedHouseFeature() : base(nameof(AbandonedHouseFeature))
        {
        }

        public override void ApplyInternal(Level level, Generator generator)
        {
            Random rnd = new Random(generator.Seed);
            for (int i = 0; i < 64; i++)
            {
                int x = rnd.Next(generator.LevelSize);
                int y = rnd.Next(generator.LevelSize);

                bool isEnoughtSpace = true;

                for (int dx = 0; dx < 9; dx++)
                {
                    for (int dy = 0; dy < 7; dy++)
                    {
                        int id = level.GetTile(x + dx, y + dy).ID;
                        if (!(id == Tile.Rock.ID | id == Tile.Grass.ID))
                        {
                            isEnoughtSpace = false;
                        }
                    }
                }

                if (isEnoughtSpace)
                {
                    Console.WriteLine($"House generated at {x},{y}");

                    for (int dx = 0; dx < 9; dx++)
                    {
                        for (int dy = 0; dy < 7; dy++)
                        {

                            if (dx == 0 | dx == 8 | dy == 0 | dy == 6)
                            {
                                level.SetTile(x + dx, y + dy, Tile.WoodWall.ID);
                            }
                            else
                            {
                                level.SetTile(x + dx, y + dy, Tile.WoodFloor.ID);
                            }
                        }
                    }
                }

            }
        }
    }
}

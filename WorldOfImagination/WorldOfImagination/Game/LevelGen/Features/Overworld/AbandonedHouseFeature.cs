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

                int sx = rnd.Next(5, 10);
                int sy = rnd.Next(5, 7);

                for (int dx = 0; dx < sx; dx++)
                {
                    for (int dy = 0; dy < sy; dy++)
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

                    for (int dx = 0; dx < sx; dx++)
                    {
                        for (int dy = 0; dy < sy; dy++)
                        {

                            if (dx == 0 | dx == sx - 1 | dy == 0 | dy == sy -1)
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

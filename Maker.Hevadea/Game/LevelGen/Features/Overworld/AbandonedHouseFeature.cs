using Maker.Hevadea.Game.Registry;
using Maker.Rise;
using Maker.Rise.Enum;
using Maker.Rise.Logging;
using System;

namespace Maker.Hevadea.Game.LevelGen.Features.Overworld
{
    public class AbandonedHouseFeature : GeneratorFeature
    {
        public AbandonedHouseFeature() : base(nameof(AbandonedHouseFeature))
        {
        }

        public override void ApplyInternal(Level level, Generator generator)
        {
            Random rnd = new Random(generator.Seed);
            for (int i = 0; i < 48 * (generator.LevelSize / 256); i++)
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
                        var tile = level.GetTile(x + dx, y + dy);
                        if (tile != TILES.GRASS)
                        {
                            isEnoughtSpace = false;
                        }
                    }
                }

                if (isEnoughtSpace)
                {
                    Logger.Log<AbandonedHouseFeature>(LoggerLevel.Info, $"House generated at {x}, {y}.");

                    for (int dx = 0; dx < sx; dx++)
                    {
                        for (int dy = 0; dy < sy; dy++)
                        {
                            if (dx == 0 | dx == sx - 1 | dy == 0 | dy == sy - 1)
                            {
                                level.SetTile(x + dx, y + dy, TILES.WOOD_WALL);
                            }
                            else
                            {
                                level.SetTile(x + dx, y + dy, TILES.WOOD_FLOOR);
                            }
                        }
                    }
                }
            }
        }
    }
}
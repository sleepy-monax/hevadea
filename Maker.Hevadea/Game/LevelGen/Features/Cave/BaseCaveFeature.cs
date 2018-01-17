using Maker.Hevadea.Game.Registry;
using Maker.Rise.Utils;

namespace Maker.Hevadea.Game.LevelGen.Features.Cave
{
    public class BaseCaveFeature : GeneratorFeature
    {
        public BaseCaveFeature() : base(nameof(BaseCaveFeature))
        {

        }

        public override void ApplyInternal(Level level, Generator generator)
        {
            for (int x = 0; x < generator.LevelSize; x++)
            {
                for (int y = 0; y < generator.LevelSize; y++)
                {
                    double seed = generator.Seed;
                    var montains = Perlin.OctavePerlin(x / 10d + seed, y / 10d + seed, 0, 2, 0.5);

                    if (montains > 0.8)
                    {
                        level.SetTile(x, y, TILES.DIRT);
                    }
                    else
                    {
                        level.SetTile(x, y, TILES.ROCK);
                    }

                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldOfImagination.Game.Entities;
using WorldOfImagination.Game.Tiles;

namespace WorldOfImagination.Game.LevelGen.Features.Overworld
{
    class TreeFeature : GeneratorFeature
    {
        public TreeFeature() : base(nameof(TreeFeature))
        {
        }

        public override void ApplyInternal(Level level, Generator generator)
        {
            Random rnd = new Random(generator.Seed);

            for (int x = 0; x < generator.LevelSize; x++)
            {
                for (int y = 0; y < generator.LevelSize; y++)
                {
 

                }
            }
        }
    }
}

using System;
using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.Tiles;

namespace Maker.Hevadea.Game.LevelGen.Features.Overworld
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
                    if (level.GetTile(x, y).ID == Tile.Grass.ID & (rnd.Next(5) == 1))
                    {
                        var tree = new TreeEntity
                        {

                            Position = new EntityPosition(x * ConstVal.TileSize + rnd.Next(0, ConstVal.TileSize - 4), y * ConstVal.TileSize + rnd.Next(0,ConstVal.TileSize - 4))
                        };

                        level.AddEntity(tree);
                    } 

                }
            }
        }
    }
}

using Maker.Hevadea.Enum;
using Maker.Hevadea.Game.Entities;

namespace Maker.Hevadea.Game.Generator.Features.Overworld
{
    public static class WorldGenerator
    {
        public static World Generate(int seed)
        {
            var world = new World
            {
                Player = new PlayerEntity()
            };

            world[Levels.Overworld] = new OverWorldGenerator { Seed = seed }.Generate();
            world[Levels.Caves] = new CaveGenerator { Seed = seed }.Generate();


            world[Levels.Overworld].AddEntity(world.Player, (world.Levels[0].Width / 2) * ConstVal.TileSize, (world.Levels[0].Height / 2) * ConstVal.TileSize);


            return world;
        }
    }
}

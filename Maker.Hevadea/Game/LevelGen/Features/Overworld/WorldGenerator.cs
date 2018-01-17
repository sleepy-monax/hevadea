using Maker.Hevadea.Game.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Hevadea.Game.LevelGen.Features.Overworld
{
    public static class WorldGenerator
    {
        public static World Generate(int seed)
        {
            var world = new World
            {
                [0] = new OverWorldGenerator { Seed = seed}.Generate(),
                [1] = new CaveGenerator { Seed = seed }.Generate(),
                Player = new PlayerEntity()
            };


            world[1].AddEntity(world.Player);
            world.Player.SetPosition((world.Levels[0].Width / 2) * ConstVal.TileSize,
                (world.Levels[0].Height / 2) * ConstVal.TileSize);


            return world;
        }
    }
}

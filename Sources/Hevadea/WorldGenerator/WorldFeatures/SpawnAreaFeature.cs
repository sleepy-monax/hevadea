using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hevadea.Game.Registry;
using Hevadea.Game.Worlds;
using Microsoft.Xna.Framework;

namespace Hevadea.WorldGenerator.WorldFeatures
{
    public class SpawnAreaFeature : WorldFeature
    {
        public string SpawnLevelName { get; set; } = "overworld";


        public override void Apply(Generator gen, World world)
        {
            var center = new Point(gen.Size / 2, gen.Size / 2);
            var spawnLevel = world.GetLevel(SpawnLevelName);
            spawnLevel.ClearEntitiesAt(center.X - 5, center.Y - 5, 10, 10);
            spawnLevel.FillRectangle(center.X - 1, center.Y - 1, 3, 3, TILES.DIRT);
        }

        public override string GetName()
        {
            return "Spawn area";
        }

        public override float GetProgress()
        {
            return 1f;
        }
    }
}

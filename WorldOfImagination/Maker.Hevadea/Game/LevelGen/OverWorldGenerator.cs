using Maker.Hevadea.Game.LevelGen.Features.Overworld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Hevadea.Game.LevelGen
{
    public class OverWorldGenerator : Generator
    {
        public OverWorldGenerator()
        {
            Features = new List<GeneratorFeature>
            {
                new OverworldBaseTerrain(),
                new AbandonedHouseFeature(),
                new TreeFeature()
            };
        }
    }
}

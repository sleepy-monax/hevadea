using Maker.Hevadea.Game.LevelGen.Features.Overworld;
using System.Collections.Generic;

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
                new GrassFeature(),
                new TreeFeature()
            };
        }
    }
}
using Maker.Hevadea.Game.Generator.Features.Overworld;
using System.Collections.Generic;

namespace Maker.Hevadea.Game.Generator
{
    public class OverWorldGenerator : GeneratorBase
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
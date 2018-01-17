using Maker.Hevadea.Game.LevelGen.Features.Cave;
using System.Collections.Generic;

namespace Maker.Hevadea.Game.LevelGen
{
    public class CaveGenerator : Generator
    {
        public CaveGenerator()
        {
            Features = new List<GeneratorFeature>() { new BaseCaveFeature()};
        }
    }
}

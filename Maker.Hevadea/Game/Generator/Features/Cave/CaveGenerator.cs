using Maker.Hevadea.Game.Generator.Features.Cave;
using System.Collections.Generic;

namespace Maker.Hevadea.Game.Generator
{
    public class CaveGenerator : GeneratorBase
    {
        public CaveGenerator()
        {
            Features = new List<GeneratorFeature>() { new BaseCaveFeature()};
        }
    }
}

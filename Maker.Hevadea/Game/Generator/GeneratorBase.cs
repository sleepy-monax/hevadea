using Maker.Rise;
using Maker.Rise.Enum;
using System.Collections.Generic;

namespace Maker.Hevadea.Game.Generator
{
    public class GeneratorBase
    {
        public int Seed = 0;
        public int LevelSize = 256;
        public List<GeneratorFeature> Features = new List<GeneratorFeature>();

        public Level Generate()
        {
            var level = new Level(LevelSize, LevelSize);

            foreach (var feature in Features)
            {
                Logger.Log<GeneratorBase>(LoggerLevel.Info, $"{feature.GetType().Name}...");
                feature.Apply(level, this);
            }

            return level;
        }
    }
}
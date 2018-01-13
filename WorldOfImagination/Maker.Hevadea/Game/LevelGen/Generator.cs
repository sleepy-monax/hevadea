using Maker.Rise;
using Maker.Rise.Enum;
using Maker.Rise.Logging;
using System;
using System.Collections.Generic;

namespace Maker.Hevadea.Game.LevelGen
{
    public class Generator
    {
        public int Seed = 0;
        public int LevelSize = 512;
        public List<GeneratorFeature> Features = new List<GeneratorFeature>();

        public Level Generate()
        {
            var level = new Level(LevelSize, LevelSize);

            foreach (var feature in Features)
            {
                Logger.Log<Generator>(LoggerLevel.Info, $"{feature.GetType().Name}...");
                feature.Apply(level, this);
            }

            return level;
        }
    }
}
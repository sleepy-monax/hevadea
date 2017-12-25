using System.Collections.Generic;

namespace WorldOfImagination.Game.LevelGen
{
    public class Generator
    {

        public int Seed = 0;
        public int LevelSize = 256;
        public List<GeneratorFeature> Features = new List<GeneratorFeature>();

        public Level Generate()
        {
            var level = new Level(LevelSize, LevelSize);

            foreach (var feature in Features)
            {
                feature.Apply(level, this);
            }

            return level;
        }

    }
}

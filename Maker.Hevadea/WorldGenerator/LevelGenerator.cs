using Maker.Hevadea.Game;
using System.Collections.Generic;
using Maker.Utils;

namespace Maker.Hevadea.WorldGenerator
{
    public class LevelGenerator
    {
        public int LevelId { get; set; } = 0;
        public string LevelName { get; set; } = "none";

        private List<GenFeature> features;
        public List<GenFeature> Features => features ?? (features = new List<GenFeature>());

        public Level Generate(Generator gen)
        {
            var level = new Level(gen.Size, gen.Size) { Id = LevelId, Name = LevelName};
            
            foreach (var f in features)
            {
                Logger.Log<LevelGenerator>($"Applying feature '{f.GetName()}'...");
                f.Apply(gen, this, level);
            }

            return level;
        }
    }
}

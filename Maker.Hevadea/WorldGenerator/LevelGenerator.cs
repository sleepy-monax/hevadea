using System.Collections.Generic;
using Maker.Hevadea.Game;
using Maker.Utils;

namespace Maker.Hevadea.WorldGenerator
{
    public class LevelGenerator
    {
        public int LevelId { get; set; } = 0;
        public string LevelName { get; set; } = "none";


        public List<LevelFeature> Features { get; set; } = new List<LevelFeature>();
        public LevelFeature CurrentFeature = null;

        public Level Generate(Generator gen)
        {
            var level = new Level(gen.Size, gen.Size) {Id = LevelId, Name = LevelName};

            foreach (var f in Features)
            {
                Logger.Log<LevelGenerator>($"Applying feature '{f.GetName()}'...");
                CurrentFeature = f;
                f.Apply(gen, this, level);
            }

            return level;
        }
    }
}
using Hevadea.Framework.Utils;
using Hevadea.Game.Registry;
using Hevadea.Game.Worlds;
using System.Collections.Generic;
using System.Linq;

namespace Hevadea.WorldGenerator
{
    public class LevelGenerator
    {
        public int LevelId { get; set; } = 0;
        public string LevelName { get; set; } = "none";
        public LevelProperties Properties { get; set; } = LEVELS.SURFACE;


        public List<LevelFeature> Features { get; set; } = new List<LevelFeature>();
        public LevelFeature CurrentFeature;

        public Level Generate(Generator gen)
        {
            var level = new Level(Properties, gen.Size, gen.Size) {Id = LevelId, Name = LevelName};

            foreach (var f in Features)
            {
                Logger.Log<LevelGenerator>($"Applying feature '{f.GetName()}'...");
                CurrentFeature = f;
                f.Apply(gen, this, level);
            }

            return level;
        }

        public float GetProgress()
        {
            var sum = Features.Aggregate(0f, (a, b) => a + b.GetProgress());
            return sum / Features.Count;
        }
    }
}
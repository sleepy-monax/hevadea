using Hevadea.Worlds;
using System.Collections.Generic;

namespace Hevadea.WorldGenerator.LevelFeatures
{
    public class CompoundFeature : LevelFeature
    {
        private string _description;
        public List<LevelFeature> Content { get; set; } = new List<LevelFeature>();

        public CompoundFeature(string description)
        {
            _description = description;
        }

        public override string GetName()
        {
            return _description;
        }

        public override float GetProgress()
        {
            var total = 0f;

            foreach (var f in Content) total += f.GetProgress();

            return total / Content.Count;
        }

        public override void Apply(Generator gen, LevelGenerator levelGen, Level level)
        {
            foreach (var f in Content) f.Apply(gen, levelGen, level);
        }
    }
}
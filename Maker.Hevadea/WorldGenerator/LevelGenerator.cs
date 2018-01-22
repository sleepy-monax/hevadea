using Maker.Hevadea.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Hevadea.WorldGenerator
{
    public class LevelGenerator
    {
        public int LevelID { get; set; } = 0;
        public string LevelName { get; set; } = "none";

        private List<GenFeature> features;
        public List<GenFeature> Features { get { return features ?? (features = new List<GenFeature>()); } }

        public Level Generate(Generator gen)
        {
            var level = new Level(gen.Size, gen.Size) { Id = LevelID, Name = LevelName};
            
            foreach (var f in features)
            {
                f.Apply(gen, this, level);
            }

            return level;
        }
    }
}

using Hevadea.Framework.Utils;
using Hevadea.Registry;
using Hevadea.Worlds;
using System.Collections.Generic;
using System.Linq;

namespace Hevadea.WorldGenerator
{
    public class LevelGenerator
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = "none";
        public LevelProperties Properties { get; set; } = LEVELS.SURFACE;
        public List<LevelFeature> Features { get; set; } = new List<LevelFeature>();
    }
}
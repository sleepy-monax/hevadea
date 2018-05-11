using System.Collections.Generic;

namespace Hevadea.Storage
{
    public class LevelStorage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        public int[] Tiles;

        public Dictionary<string, string> TileBidinding { get; set; }
        public Dictionary<string, object>[] TilesData { get; set; }
        public List<EntityStorage> Entities { get; set; } = new List<EntityStorage>();
    }
}
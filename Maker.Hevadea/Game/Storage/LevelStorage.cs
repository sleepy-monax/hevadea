using System.Collections.Generic;

namespace Maker.Hevadea.Game.Storage
{
    public class LevelStorage
    {
        public List<EntityStorage> Entities { get; set; } = new List<EntityStorage>();
        public int Width { get; set; }
        public int Height { get; set; }
        public byte[] Tiles;
        public Dictionary<string, object>[] TilesData;
    }
}

using System.Collections.Generic;

namespace Hevadea.Game.Storage
{
    public class WorldStorage
    {
        public int SaveVersion { get; set; } = 1;
        public List<LevelStorage> Levels { get; set; } = new List<LevelStorage>();
        public double Time { get; set; } = 0;
        public string PlayerSpawnLevel { get; set; } = "null";
    }
}
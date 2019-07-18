using System.Collections.Generic;

namespace Hevadea.Storage
{
    public class WorldStorage
    {
        public double Time { get; set; } = 0;
        public int SaveVersion { get; set; } = 1;

        public string PlayerSpawnLevel { get; set; } = "null";
        public List<string> Levels { get; set; } = new List<string>();

        public WorldStorage()
        {
        }
    }
}
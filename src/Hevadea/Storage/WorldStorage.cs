using System.Collections.Generic;

namespace Hevadea.Storage
{
    public class WorldStorage
    {
        public double Time { get; set; } = 0;
        public int UeidCounter { get; set; } = 0;
        public int SaveVersion { get; set; } = 1;

        public List<string> Levels { get; set; } = new List<string>();
        public string PlayerSpawnLevel { get; set; } = "null";
    }
}
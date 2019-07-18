using Hevadea.Worlds;
using System.Collections.Generic;

namespace Hevadea.Storage
{
    public class ChunkStorage
    {
        public Dictionary<string, string> Registry { get; set; }

        public int X { get; set; }
        public int Y { get; set; }
        public int Level { get; set; }

        public int[] Tiles { get; set; }
        public Dictionary<string, object>[] Data { get; set; }
        public List<EntityStorage> Entities { get; set; }

        public ChunkStorage()
        {
            X = 0;
            Y = 0;
            Level = 0;

            Tiles = new int[Chunk.SIZE * Chunk.SIZE];
            Data = new Dictionary<string, object>[Chunk.SIZE * Chunk.SIZE];
            Entities = new List<EntityStorage>();
            Registry = new Dictionary<string, string>();
        }
    }
}
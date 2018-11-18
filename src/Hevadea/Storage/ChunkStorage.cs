using Hevadea.Worlds;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Hevadea.Storage
{
    [JsonObject]
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

            Tiles = new int[Chunk.CHUNK_SIZE * Chunk.CHUNK_SIZE];
            Data = new Dictionary<string, object>[Chunk.CHUNK_SIZE * Chunk.CHUNK_SIZE];
            Entities = new List<EntityStorage>();
            Registry = new Dictionary<string, string>();
        }
    }
}
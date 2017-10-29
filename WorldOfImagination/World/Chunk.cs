using System.IO;
using WorldOfImagination.Framework;

namespace WorldOfImagination.World
{
    public class Chunk
    {
        int X, Y;
        public int[,,] Tiles;

        public Chunk()
        {
            X = Y = 0;
            Tiles = new int[16, 16, 3];
        }

        public static Chunk LoadFromFile(string fileName)           => File.ReadAllText(fileName).FromJson<Chunk>();
        public static void SaveToFile(string fileName, Chunk chunk) => File.WriteAllText(fileName, chunk.ToJson());
    }
}
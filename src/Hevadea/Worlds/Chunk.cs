using System.Collections.Generic;
using Hevadea.GameObjects;
using Hevadea.GameObjects.Entities;
using Hevadea.GameObjects.Tiles;

namespace Hevadea.Worlds
{
    public class Chunk
    {
		public const int CHUNK_SIZE = 16;

        public Tile[,] Tiles;
        public List<Entity> Entities;
		public Dictionary<string, object>[,] Data { get; set; }

		public Chunk()
		{
			Tiles = new Tile[CHUNK_SIZE, CHUNK_SIZE];
			Data = new Dictionary<string, object>[CHUNK_SIZE, CHUNK_SIZE];
            Entities = new List<Entity>();

            for (int x = 0; x < CHUNK_SIZE; x++)
			{
                for (int y = 0; y < CHUNK_SIZE; y++)
				{
					Tiles[x, y] = TILES.VOID;
					Data[x, y] = new Dictionary<string, object>();
				}
			}
		}

		public void Load()
		{
			
		}

		public void Save()
		{
			
		}
    }
}
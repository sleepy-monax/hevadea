using Hevadea.GameObjects;
using Hevadea.GameObjects.Entities;
using Hevadea.GameObjects.Tiles;
using Hevadea.GameObjects.Tiles.Renderers;
using System.Collections.Generic;

namespace Hevadea.Worlds
{
    public class Chunk
    {
        public static readonly int CHUNK_SIZE = 16;

        public Tile[,] Tiles;
        public List<Entity> Entities;
        public Dictionary<string, object>[,] Data { get; set; }
        public List<Entity>[,] EntitiesOnTiles;
        public TileConnection[,] CachedTileConnection;

        public Chunk()
        {
            Tiles = new Tile[CHUNK_SIZE, CHUNK_SIZE];
            Data = new Dictionary<string, object>[CHUNK_SIZE, CHUNK_SIZE];
            CachedTileConnection = new TileConnection[CHUNK_SIZE, CHUNK_SIZE];
            Entities = new List<Entity>();
            EntitiesOnTiles = new List<Entity>[CHUNK_SIZE, CHUNK_SIZE];

            for (int x = 0; x < CHUNK_SIZE; x++)
            {
                for (int y = 0; y < CHUNK_SIZE; y++)
                {
                    Tiles[x, y] = TILES.VOID;
                    Data[x, y] = new Dictionary<string, object>();
                    EntitiesOnTiles[x, y] = new List<Entity>();
                }
            }
        }

        public void Load() { }
        public void Save() { }
    }
}
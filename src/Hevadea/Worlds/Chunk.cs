using Hevadea.GameObjects;
using Hevadea.GameObjects.Entities;
using Hevadea.GameObjects.Tiles;
using Hevadea.GameObjects.Tiles.Renderers;
using Hevadea.Registry;
using Hevadea.Storage;
using System.Collections.Generic;

namespace Hevadea.Worlds
{
    public class Chunk
    {
        public static readonly int CHUNK_SIZE = 16;

        public int X { get; }
        public int Y { get; }

        public Tile[,] Tiles { get; set; }
        public List<Entity> Entities { get; set; }
        public Dictionary<string, object>[,] Data { get; set; }

        public List<Entity>[,] EntitiesOnTiles { get; set; }
        public TileConnection[,] CachedTileConnection { get; set; }

        public Level Level { get; set; }

        public Chunk(int x, int y)
        {
            X = x;
            Y = y;

            Tiles = new Tile[CHUNK_SIZE, CHUNK_SIZE];
            Data = new Dictionary<string, object>[CHUNK_SIZE, CHUNK_SIZE];
            CachedTileConnection = new TileConnection[CHUNK_SIZE, CHUNK_SIZE];
            Entities = new List<Entity>();
            EntitiesOnTiles = new List<Entity>[CHUNK_SIZE, CHUNK_SIZE];

            for (int xx = 0; xx < CHUNK_SIZE; xx++)
            {
                for (int yy = 0; yy < CHUNK_SIZE; yy++)
                {
                    Tiles[xx, yy] = TILES.VOID;
                    Data[xx, yy] = new Dictionary<string, object>();
                    EntitiesOnTiles[xx, yy] = new List<Entity>();
                }
            }
        }

        public void AddEntity(Entity e)
        {
            Coordinates tPos = e.Coordinates;

            Entities.Add(e);
            EntitiesOnTiles[tPos.X % CHUNK_SIZE, tPos.Y % CHUNK_SIZE].Add(e);

            e.Removed = false;
        }

        public void RemoveEntity(Entity e)
        {
            Coordinates tPos = e.Coordinates;

            Entities.Remove(e);
            EntitiesOnTiles[tPos.X % CHUNK_SIZE, tPos.Y % CHUNK_SIZE].Remove(e);

            e.Removed = true;
        }

        public static Chunk Load(ChunkStorage store)
        {
            Chunk chunk = new Chunk(store.X, store.Y);

            // Loading tile
            for (int x = 0; x < CHUNK_SIZE; x++)
            {
                for (int y = 0; y < CHUNK_SIZE; y++)
                {
                    chunk.Tiles[x, y] = TILES.GetTile(store.Registry[store.Tiles[y * CHUNK_SIZE + x].ToString()]);
                    chunk.Data[x, y] = store.Data[y * CHUNK_SIZE + x];
                }
            }

            // Loading entities
            foreach (EntityStorage entityData in store.Entities)
            {
                Entity entity = entityData.ConstructEntity();
                chunk.AddEntity(entity);
            }

            return chunk;
        }

        public ChunkStorage Save()
        {
            ChunkStorage store = new ChunkStorage();
            Dictionary<Tile, int> tileToId = TILES.GetTileToID();

            store.X = X;
            store.Y = Y;
            store.Level = Level.Id;

            store.Registry = TILES.GetIDToName();

            // Saving tile
            for (int x = 0; x < CHUNK_SIZE; x++)
                for (int y = 0; y < CHUNK_SIZE; y++)
                {
                    store.Tiles[y * CHUNK_SIZE + x] = tileToId[Tiles[x, y]];
                    store.Data[y * CHUNK_SIZE + x] = Data[x, y];
                }

            // Saving entities
            foreach (var e in Entities)
            {
                if (!e.MemberOf(ENTITIES.GROUPE_SAVE_EXCUDED))
                {
                    store.Entities.Add(e.Save());
                }
            }

            return store;
        }
    }
}
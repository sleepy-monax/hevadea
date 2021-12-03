using Hevadea.Entities;
using Hevadea.Registry;
using Hevadea.Storage;
using Hevadea.Tiles;
using Hevadea.Tiles.Renderers;
using System.Collections.Generic;
using System.Linq;

namespace Hevadea.Worlds
{
    public class Chunk
    {
        public const int SIZE = 16;

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

            Tiles = new Tile[SIZE, SIZE];
            Data = new Dictionary<string, object>[SIZE, SIZE];
            CachedTileConnection = new TileConnection[SIZE, SIZE];
            Entities = new List<Entity>();
            EntitiesOnTiles = new List<Entity>[SIZE, SIZE];

            for (var xx = 0; xx < SIZE; xx++)
                for (var yy = 0; yy < SIZE; yy++)
                {
                    Tiles[xx, yy] = TILES.VOID;
                    Data[xx, yy] = new Dictionary<string, object>();
                    EntitiesOnTiles[xx, yy] = new List<Entity>();
                }
        }

        public void AddEntity(Entity e)
        {
            lock (Entities)
            {
                Entities.Add(e);
                EntitiesOnTiles[e.Coordinates.X % SIZE, e.Coordinates.Y % SIZE].Add(e);

                e.Removed = false;
            }
        }

        public void RemoveEntity(Entity e)
        {
            lock (Entities)
            {
                Entities.Remove(e);
                EntitiesOnTiles[e.Coordinates.X % SIZE, e.Coordinates.Y % SIZE].Remove(e);

                e.Removed = true;
            }
        }

        public static Chunk Load(ChunkStorage store)
        {
            var chunk = new Chunk(store.X, store.Y);

            // Loading tile
            for (var x = 0; x < SIZE; x++)
            for (var y = 0; y < SIZE; y++)
            {
                chunk.Tiles[x, y] = TILES.GetTile(store.Registry[store.Tiles[y * SIZE + x].ToString()]);
                chunk.Data[x, y] = store.Data[y * SIZE + x];
            }

            // Loading entities
            foreach (var entityData in store.Entities)
            {
                var entity = entityData.ConstructEntity();
                chunk.AddEntity(entity);
            }

            return chunk;
        }

        public ChunkStorage Save()
        {
            var storage = new ChunkStorage();
            var tileToId = TILES.GetTileToID();

            storage.X = X;
            storage.Y = Y;
            storage.Level = Level.Id;

            storage.Registry = TILES.GetIDToName();

            // Saving tile
            for (var x = 0; x < SIZE; x++)
            for (var y = 0; y < SIZE; y++)
            {
                storage.Tiles[y * SIZE + x] = tileToId[Tiles[x, y]];
                storage.Data[y * SIZE + x] = Data[x, y];
            }

            // Saving entities
            lock (Entities)
            {
                foreach (var entity in Entities.Where(e => !e.MemberOf(ENTITIES.GROUPE_SAVE_EXCUDED)))
                    storage.Entities.Add(entity.Save());
            }

            return storage;
        }
    }
}
using Hevadea.Framework.Utils;
using Hevadea.GameObjects;
using Hevadea.GameObjects.Entities;
using Hevadea.GameObjects.Entities.Blueprints;
using Hevadea.GameObjects.Tiles;
using Hevadea.GameObjects.Tiles.Renderers;
using System.Collections.Generic;
using System.Linq;

namespace Hevadea.Worlds
{
    public partial class Level
    {

        public Chunk GetChunkAt(TilePosition t) => GetChunkAt(t.X, t.Y);
        public Chunk GetChunkAt(int tx, int ty)
        {
            if (tx < 0 || ty < 0 || tx >= Width || ty >= Height) return null;
            return Chunks[tx / Chunk.CHUNK_SIZE, ty / Chunk.CHUNK_SIZE];
        }

        /* ----------------------------------------------------------------- */

        public Tile GetTile(TilePosition t) => GetTile(t.X, t.Y);
        public Tile GetTile(int tx, int ty)
        {
            Chunk chunk = GetChunkAt(tx, ty);

            if (chunk != null)
            {
                return chunk.Tiles[tx % Chunk.CHUNK_SIZE, ty % Chunk.CHUNK_SIZE];
            }

            return TILES.VOID;
        }

        public bool SetTile(TilePosition t, Tile tile) => SetTile(t.X, t.Y, tile);
        public bool SetTile(int tx, int ty, Tile tile)
        {
            Chunk chunk = GetChunkAt(tx, ty);

            if (chunk != null)
            {
                chunk.Tiles[tx % Chunk.CHUNK_SIZE, ty % Chunk.CHUNK_SIZE] = tile;

                if (IsInitialized)
                {
                    for (var x = -1; x <= 1; x++)
                        for (var y = -1; y <= 1; y++)
                        {
                            var xx = tx + x;
                            var yy = ty + y;

                            if (xx >= 0 && yy >= 0 && xx < Width && yy < Height)
                                SetTileConnection(xx, yy, null);
                        }
                }

                return true;
            }

            return false;
        }

        /* ----------------------------------------------------------------- */

        public Dictionary<string, object> GetTileDataAt(TilePosition t) => GetTileDataAt(t.X, t.Y);
        public Dictionary<string, object> GetTileDataAt(int tx, int ty)
        {
            Chunk chunk = GetChunkAt(tx, ty);

            if (chunk != null)
            {
                return chunk.Data[tx % Chunk.CHUNK_SIZE, ty % Chunk.CHUNK_SIZE];
            }

            return null;
        }

        public T GetTileData<T>(TilePosition t, string dataName, T defaultValue) => GetTileData(t.X, t.Y, dataName, defaultValue);
        public T GetTileData<T>(int tx, int ty, string dataName, T defaultValue)
        {
            return (T)GetTileDataAt(tx, ty).GetValueOrDefault(dataName, defaultValue);
        }

        public void SetTileDataAt(TilePosition t, Dictionary<string, object> data) => SetTileDataAt(t.X, t.Y, data);
        public void SetTileDataAt(int tx, int ty, Dictionary<string, object> data)
        {
            Chunk chunk = GetChunkAt(tx, ty);

            if (chunk != null)
            {
                chunk.Data[tx % Chunk.CHUNK_SIZE, ty % Chunk.CHUNK_SIZE] = data;
            }
        }

        internal void SetTileData<T>(TilePosition t, string dataName, T value) => SetTileData(t.X, t.Y, dataName, value);

        public void SetTileData<T>(int tx, int ty, string dataName, T value)
        {
            GetTileDataAt(tx, ty)[dataName] = value;
        }

        public void ClearTileDataAt(TilePosition tilePosition) => ClearTileDataAt(tilePosition.X, tilePosition.Y);
        public void ClearTileDataAt(int tx, int ty)
        {
            GetTileDataAt(tx, ty)?.Clear();
        }

        /* ----------------------------------------------------------------- */

        public TileConnection GetTileConnection(TilePosition t) => GetTileConnection(t.X, t.Y);
        public TileConnection GetTileConnection(int tx, int ty)
        {
            Chunk chunk = GetChunkAt(tx, ty);

            if (chunk != null)
            {
                return chunk.CachedTileConnection[tx % Chunk.CHUNK_SIZE, ty % Chunk.CHUNK_SIZE];
            }

            return null;
        }

        public void SetTileConnection(TilePosition t, TileConnection tileConnection) => SetTileConnection(t.X, t.Y, tileConnection);
        public void SetTileConnection(int tx, int ty, TileConnection tileConnection)
        {
            Chunk chunk = GetChunkAt(tx, ty);

            if (chunk != null)
            {
                chunk.CachedTileConnection[tx % Chunk.CHUNK_SIZE, ty % Chunk.CHUNK_SIZE] = tileConnection;
            }
        }

        /* ----------------------------------------------------------------- */

        public void AddEntity(Entity e)
        {
            TilePosition tPos = e.GetTilePosition();
            Chunk chunk = GetChunkAt(tPos);

            chunk.Entities.Add(e);
            chunk.EntitiesOnTiles[tPos.X % Chunk.CHUNK_SIZE, tPos.Y % Chunk.CHUNK_SIZE].Add(e);

            e.Removed = false;
            e.Level = this;

            if (IsInitialized)
            {
                e.Initialize(this, _world, _game);
            }
        }

        public Entity AddEntityAt(Entity e, float tx, float ty)
        {
            AddEntity(e);
            e.SetPosition(tx * GLOBAL.Unit, ty * GLOBAL.Unit);
            return e;
        }

        public Entity AddEntityAt(Entity e, int tx, int ty, float offX = 0f, float offY = 0f)
        {
            AddEntity(e);
            e.SetPosition(tx * GLOBAL.Unit + (GLOBAL.Unit / 2) + offX, ty * GLOBAL.Unit + (GLOBAL.Unit / 2) + offY);
            return e;
        }

        public Entity AddEntityAt(EntityBlueprint b, int tx, int ty, float offX = 0f, float offY = 0f)
        {
            var entity = b.Construct();
            AddEntityAt(entity, tx, ty, offX, offY);
            return entity;
        }

        public void RemoveEntity(Entity e)
        {
            TilePosition tPos = e.GetTilePosition();
            Chunk chunk = GetChunkAt(tPos);

            chunk.Entities.Remove(e);
            chunk.EntitiesOnTiles[tPos.X % Chunk.CHUNK_SIZE, tPos.Y % Chunk.CHUNK_SIZE].Remove(e);

            e.Removed = true;
        }

        public List<Entity> GetEntitiesAt(TilePosition t) => GetEntitiesAt(t.X, t.Y);
        public List<Entity> GetEntitiesAt(int tx, int ty)
        {
            Chunk chunk = GetChunkAt(tx, ty);

            if (chunk != null)
            {
                return chunk.EntitiesOnTiles[tx % Chunk.CHUNK_SIZE, ty % Chunk.CHUNK_SIZE].ToList();
            }

            return new List<Entity>();
        }

    }
}

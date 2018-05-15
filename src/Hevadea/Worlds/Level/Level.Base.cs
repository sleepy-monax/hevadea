using Hevadea.Framework.Graphic.Particles;
using Hevadea.Framework.Utils;
using Hevadea.GameObjects;
using Hevadea.GameObjects.Entities;
using Hevadea.GameObjects.Entities.Blueprints;
using Hevadea.GameObjects.Entities.Components;
using Hevadea.GameObjects.Tiles;
using Hevadea.GameObjects.Tiles.Renderers;
using Hevadea.Registry;
using Hevadea.Storage;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hevadea.Worlds
{
    public partial class Level
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public LevelProperties Properties { get; }

        public Chunk[,] Chunks { get; set; } 

        public ParticleSystem ParticleSystem { get; }
        public Minimap Minimap { get; set; }

        private GameManager _game;
        private World _world;

        public Level(LevelProperties properties, int width, int height)
        {
            Properties = properties;
            Width = width;
            Height = height;
            ParticleSystem = new ParticleSystem();
            Minimap = new Minimap(this);

            Chunks = new Chunk[width / 16, height / 16];

            for (int x = 0; x < width / 16; x++)
            {
                for (int y = 0; y < height / 16; y++)
                {
                    Chunks[x, y] = new Chunk(x, y);
                }
            }
        }

        /* --- Gameloop -----------------------------------------------------*/

        // TODO: move the code here.

        /* --- Save & Load ------------------------------------------------- */

        public static Level Load(LevelStorage store)
        {
            // TODO: Fallback when 'store == null'.

            return new Level(LEVELS.GetProperties(store.Type), store.Width, store.Height)
            {
                Id = store.Id,
                Name = store.Name,
            };
        }

        public LevelStorage Save()
        {
            return new LevelStorage()
            {
                Id = Id,
                Name = Name,
                Type = Properties.Name,

                Width = Width,
                Height = Height,
            };
        }

        /* --- Chunks ------------------------------------------------------ */

        public Chunk GetChunkAt(TilePosition t) => GetChunkAt(t.X, t.Y);
        public Chunk GetChunkAt(int tx, int ty)
        {
            if (tx < 0 || ty < 0 || tx >= Width || ty >= Height) return null;
            return Chunks[tx / Chunk.CHUNK_SIZE, ty / Chunk.CHUNK_SIZE];
        }

        /* --- Tiles ------------------------------------------------------- */

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


        public bool IsAll<T>(Rectangle area) where T : TileComponent => IsAll(area, (t) => t.HasTag<T>());
        public bool IsAll(Rectangle area, Tile tile) => IsAll(area, (t) => t == tile);
        public bool IsAll(Rectangle area, Predicate<Tile> predicat)
        {
            var beginX = area.X / GLOBAL.Unit - 1;
            var beginY = area.Y / GLOBAL.Unit - 1;

            var endX = (area.X + area.Width) / GLOBAL.Unit + 1;
            var endY = (area.Y + area.Height) / GLOBAL.Unit + 1;

            var result = true;

            for (var x = beginX; x < endX; x++)
                for (var y = beginY; y < endY; y++)
                {
                    if (x < 0 || y < 0 || x >= Width || y >= Height) continue;
                    result &= predicat(GetTile(x, y));
                }

            return result;
        }

        /* --- Tile data --------------------------------------------------- */

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

        /* --- Tile Connections -------------------------------------------- */

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

        /* --- Entities ---------------------------------------------------- */

        public void AddEntity(Entity e)
        {
            Chunk chunk = GetChunkAt(e.GetTilePosition());
        
            chunk.AddEntity(e);

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
            Chunk chunk = GetChunkAt(e.GetTilePosition());

            chunk.RemoveEntity(e);
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


        public List<Entity> GetEntitiesOnArea(float cx, float cy, float radius)
        {
            var entities = GetEntitiesOnArea(new RectangleF(cx - radius, cy - radius, radius * 2, radius * 2));
            return entities.Where(e => Mathf.Distance(e.X, e.Y, cx, cy) <= radius).ToList();
        }

        public List<Entity> GetEntitiesOnArea(Rectangle area) => GetEntitiesOnArea(new RectangleF(area.X, area.Y, area.Width, area.Height));
        public List<Entity> GetEntitiesOnArea(RectangleF area)
        {
            var result = new List<Entity>();

            var beginX = area.X / GLOBAL.Unit - 1;
            var beginY = area.Y / GLOBAL.Unit - 1;

            var endX = (area.X + area.Width) / GLOBAL.Unit + 1;
            var endY = (area.Y + area.Height) / GLOBAL.Unit + 1;

            for (int x = (int)beginX; x < endX; x++)
                for (int y = (int)beginY; y < endY; y++)
                {
                    if (x < 0 || y < 0 || x >= Width || y >= Height) continue;
                    var entities = GetEntitiesAt(x, y);

                    result.AddRange(entities.Where(i => i.GetComponent<Colider>()?.GetHitBox().IntersectsWith(area) ?? area.Contains(i.Position)));
                }

            return result;
        }
    }
}
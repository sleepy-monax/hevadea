using Hevadea.Framework;
using Hevadea.Framework.Extension;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.Graphic.Particles;
using Hevadea.Entities;
using Hevadea.Entities.Blueprints;
using Hevadea.Entities.Components;
using Hevadea.Tiles;
using Hevadea.Tiles.Renderers;
using Hevadea.Registry;
using Hevadea.Storage;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hevadea.Worlds
{
    public class Level
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Point Size => new Point(Width, Height);
        public LevelProperties Properties { get; }
        public bool IsInitialized { get; private set; } = false;
        public Chunk[,] Chunks { get; set; }

        public ParticleSystem ParticleSystem { get; }
        public Minimap Minimap { get; set; }

        private GameState _gameState;
        private World _world;

        private static readonly BlendState LightBlend = new BlendState
        {
            ColorBlendFunction = BlendFunction.Add,
            ColorSourceBlend = Blend.DestinationColor,
            ColorDestinationBlend = Blend.Zero
        };

        public Level(LevelProperties properties, int width, int height)
        {
            Properties = properties;
            Width = width;
            Height = height;
            ParticleSystem = new ParticleSystem();
            Minimap = new Minimap(this);

            Chunks = new Chunk[width / 16, height / 16];

            for (var x = 0; x < width / 16; x++)
            for (var y = 0; y < height / 16; y++)
                Chunks[x, y] = new Chunk(x, y);
        }

        /* --- Game Loop -----------------------------------------------------*/

        public void Initialize(World world, GameState gameState)
        {
            _world = world;
            _gameState = gameState;
            foreach (var c in Chunks)
            {
                c.Level = this;
                foreach (var e in c.Entities) e.Initialize(this, world, _gameState);
            }

            IsInitialized = true;
        }

        public void Update(GameTime gameTime)
        {
            // Update all alive entities.
            QueryEntity(_gameState.Camera.Bound)
                .ForEarch((e) =>
                {
                    if (!e.Removed) e.Update(gameTime);
                });

            // Do the random update of tiles.
            for (var i = 0; i < Width * Height / 50; i++)
            {
                var tile = new Coordinates(Rise.Rnd.Next(Width), Rise.Rnd.Next(Height));
                GetTile(tile).Update(tile, GetTileDataAt(tile), this, gameTime);
            }

            ParticleSystem.Update(gameTime);
        }

        public void Draw(LevelSpriteBatchPool spriteBatchPool, GameTime gameTime)
        {
            spriteBatchPool.Begin(_gameState.Camera);

            // Draw Tiles.
            foreach (var coords in QueryCoordinates(_gameState.Camera.Bound))
                GetTile(coords).Draw(spriteBatchPool.Tiles, coords, GetTileDataAt(coords), this, gameTime);

            ParticleSystem.Draw(spriteBatchPool.Tiles, gameTime);

            var entitiesToDraw = new EntityCollection();
            entitiesToDraw.AddRange(QueryEntity(_gameState.Camera.Bound.Inflate(Game.Unit * 4f, Game.Unit * 12f)));
            entitiesToDraw.SortForRender();

            // Draw Entities, Shadows and lights.
            foreach (var e in entitiesToDraw)
            {
                // Draw the entity.
                e.Draw(spriteBatchPool, gameTime);

                // Draw Entity overlay.
                if (Rise.Ui.Enabled) e.Overlay(spriteBatchPool.Overlay, gameTime);

                if (Rise.Debug.GAME) spriteBatchPool.Overlay.PutPixel(e.Position, Color.Magenta);
            }

            FinalizeDraw(spriteBatchPool);
        }

        private void FinalizeDraw(LevelSpriteBatchPool spriteBatchPool)
        {
            // Get the ambiant lightning.
            var ambiantLight = Properties.AmbiantLight;

            if (Properties.AffectedByDayNightCycle) ambiantLight = _world.DayNightCycle.GetAmbiantLight();

            // Get temporary render targets.
            var worldRenderTarget = Rise.Graphic.RenderTarget[0];
            var lightRenderTarget = Rise.Graphic.RenderTarget[1];

            // Draw Entities and tiles to their own rendertarget.
            Rise.Graphic.SetRenderTarget(worldRenderTarget);
            Rise.Graphic.Clear(new Color(148, 120, 92));
            spriteBatchPool.Tiles.End();
            spriteBatchPool.Shadows.End();
            spriteBatchPool.Entities.End();
            spriteBatchPool.Overlay.End();

            // Draw shadow to their own rendertarget.
            Rise.Graphic.SetRenderTarget(lightRenderTarget);
            Rise.Graphic.Clear(ambiantLight);
            spriteBatchPool.Lights.End();

            // Now let's draw everything to the screen.
            Rise.Graphic.SetDefaultRenderTarget();

            // Blit the world on screen.
            spriteBatchPool.Generic.Begin();
            spriteBatchPool.Generic.Draw(worldRenderTarget, Rise.Graphic.GetBound(), Color.White);
            spriteBatchPool.Generic.End();

            // Apply lightning.
            spriteBatchPool.Generic.Begin(SpriteSortMode.Immediate, LightBlend);
            spriteBatchPool.Generic.Draw(lightRenderTarget, Rise.Graphic.GetBound(), Color.White);
            spriteBatchPool.Generic.End();
        }

        /* --- Save & Load -------------------------------------------------- */

        public static Level Load(LevelStorage store)
        {
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

        /* --- Chunks ------------------------------------------------------- */

        public Chunk GetChunkAt(Coordinates t)
        {
            return GetChunkAt(t.X, t.Y);
        }

        public Chunk GetChunkAt(int tx, int ty)
        {
            if (tx < 0 || ty < 0 || tx >= Width || ty >= Height) return null;
            return Chunks[tx / Chunk.SIZE, ty / Chunk.SIZE];
        }

        /* --- Tiles -------------------------------------------------------- */

        public Tile GetTile(Coordinates t)
        {
            return GetTile(t.X, t.Y);
        }

        public Tile GetTile(int tx, int ty)
        {
            var chunk = GetChunkAt(tx, ty);

            if (chunk != null) return chunk.Tiles[tx % Chunk.SIZE, ty % Chunk.SIZE];

            return TILES.VOID;
        }

        public bool SetTile(Coordinates t, Tile tile)
        {
            return SetTile(t.X, t.Y, tile);
        }

        public bool SetTile(int tx, int ty, Tile tile)
        {
            var chunk = GetChunkAt(tx, ty);

            if (chunk != null)
            {
                chunk.Tiles[tx % Chunk.SIZE, ty % Chunk.SIZE] = tile;

                if (IsInitialized)
                    for (var x = -1; x <= 1; x++)
                    for (var y = -1; y <= 1; y++)
                    {
                        var xx = tx + x;
                        var yy = ty + y;

                        if (xx >= 0 && yy >= 0 && xx < Width && yy < Height)
                            SetTileConnection(xx, yy, null);
                    }

                return true;
            }

            return false;
        }

        public bool IsAll<T>(Rectangle area) where T : TileComponent
        {
            return IsAll(area, (t) => t.HasTag<T>());
        }

        public bool IsAll(Rectangle area, Tile tile)
        {
            return IsAll(area, (t) => t == tile);
        }

        public bool IsAll(Rectangle area, Predicate<Tile> predicat)
        {
            var beginX = area.X / Game.Unit;
            var beginY = area.Y / Game.Unit;

            var endX = (area.X + area.Width) / Game.Unit;
            var endY = (area.Y + area.Height) / Game.Unit;

            var result = true;

            for (var x = beginX; x <= endX; x++)
            for (var y = beginY; y <= endY; y++)
            {
                if (x < 0 || y < 0 || x >= Width || y >= Height) continue;
                result &= predicat(GetTile(x, y));
            }

            return result;
        }

        /* --- Tile data ---------------------------------------------------- */

        public Dictionary<string, object> GetTileDataAt(Coordinates t)
        {
            return GetTileDataAt(t.X, t.Y);
        }

        public Dictionary<string, object> GetTileDataAt(int tx, int ty)
        {
            var chunk = GetChunkAt(tx, ty);

            if (chunk != null) return chunk.Data[tx % Chunk.SIZE, ty % Chunk.SIZE];

            return null;
        }

        public T GetTileData<T>(Coordinates t, string dataName, T defaultValue)
        {
            return GetTileData(t.X, t.Y, dataName, defaultValue);
        }

        public T GetTileData<T>(int tx, int ty, string dataName, T defaultValue)
        {
            return (T) GetTileDataAt(tx, ty).GetValueOrDefault(dataName, defaultValue);
        }

        public void SetTileDataAt(Coordinates t, Dictionary<string, object> data)
        {
            SetTileDataAt(t.X, t.Y, data);
        }

        public void SetTileDataAt(int tx, int ty, Dictionary<string, object> data)
        {
            var chunk = GetChunkAt(tx, ty);

            if (chunk != null) chunk.Data[tx % Chunk.SIZE, ty % Chunk.SIZE] = data;
        }

        internal void SetTileData<T>(Coordinates t, string dataName, T value)
        {
            SetTileData(t.X, t.Y, dataName, value);
        }

        public void SetTileData<T>(int tx, int ty, string dataName, T value)
        {
            GetTileDataAt(tx, ty)[dataName] = value;
        }

        public void ClearTileDataAt(Coordinates tilePosition)
        {
            ClearTileDataAt(tilePosition.X, tilePosition.Y);
        }

        public void ClearTileDataAt(int tx, int ty)
        {
            GetTileDataAt(tx, ty)?.Clear();
        }

        /* --- Tile Connections --------------------------------------------- */

        public TileConnection GetTileConnection(Coordinates t)
        {
            return GetTileConnection(t.X, t.Y);
        }

        public TileConnection GetTileConnection(int tx, int ty)
        {
            var chunk = GetChunkAt(tx, ty);

            if (chunk != null) return chunk.CachedTileConnection[tx % Chunk.SIZE, ty % Chunk.SIZE];

            return null;
        }

        public void SetTileConnection(Coordinates t, TileConnection tileConnection)
        {
            SetTileConnection(t.X, t.Y, tileConnection);
        }

        public void SetTileConnection(int tx, int ty, TileConnection tileConnection)
        {
            var chunk = GetChunkAt(tx, ty);

            if (chunk != null) chunk.CachedTileConnection[tx % Chunk.SIZE, ty % Chunk.SIZE] = tileConnection;
        }

        /* --- Entities ----------------------------------------------------- */

        public Entity AddEntityAt(EntityBlueprint blueprint, Coordinates coordinates)
        {
            return AddEntityAt(blueprint.Construct(), coordinates.X, coordinates.Y);
        }

        public Entity AddEntityAt(Entity entity, Coordinates coordinates)
        {
            return AddEntityAt(entity, coordinates.X, coordinates.Y);
        }

        public Entity AddEntityAt(EntityBlueprint blueprint, Coordinates coordinates, Vector2 offset)
        {
            return AddEntityAt(blueprint.Construct(), coordinates.X, coordinates.Y, offset.X, offset.Y);
        }

        public Entity AddEntityAt(Entity entity, Coordinates coordinates, Vector2 offset)
        {
            return AddEntityAt(entity, coordinates.X, coordinates.Y, offset.X, offset.Y);
        }

        public Entity AddEntityAt(EntityBlueprint blueprint, int tx, int ty, float offX = 0f, float offY = 0f)
        {
            return AddEntityAt(blueprint.Construct(), tx, ty, offX, offY);
        }

        public Entity AddEntityAt(Entity e, int tx, int ty, float offX = 0f, float offY = 0f)
        {
            AddEntity(e);
            e.Position = new Vector2(tx, ty) * Game.Unit + new Vector2(Game.Unit / 2) + new Vector2(offX, offY);
            return e;
        }

        public void AddEntity(Entity e)
        {
            GetChunkAt(e.Coordinates).AddEntity(e);
            e.Level = this;
            if (IsInitialized) e.Initialize(this, _world, _gameState);
        }

        public void RemoveEntity(Entity e)
        {
            var chunk = GetChunkAt(e.Coordinates);
            chunk.RemoveEntity(e);
        }

        public bool AnyEntityAt(Coordinates coords)
        {
            return QueryEntity(coords).Any();
        }

        /* === Queries ====================================================== */

        /* --- Coordinates Query -------------------------------------------- */

        public IEnumerable<Coordinates> QueryCoordinates(Vector2 center, float radius)
        {
            return QueryCoordinates(new CircleF(center, radius));
        }

        public IEnumerable<Coordinates> QueryCoordinates(CircleF c)
        {
            foreach (var coords in QueryCoordinates(c.Bound))
                if (c.Containe(coords.ToVector2()))
                    yield return coords;
        }

        public IEnumerable<Coordinates> QueryCoordinates(Rectangle r)
        {
            return QueryCoordinates(new RectangleF(r.X, r.Y, r.Width, r.Height));
        }

        public IEnumerable<Coordinates> QueryCoordinates(RectangleF r)
        {
            var beginX = r.X / Game.Unit - 1;
            var beginY = r.Y / Game.Unit - 1;

            var endX = (r.X + r.Width) / Game.Unit + 1;
            var endY = (r.Y + r.Height) / Game.Unit + 1;

            for (var x = (int) beginX; x < endX; x++)
            for (var y = (int) beginY; y < endY; y++)
            {
                if (x < 0 || y < 0 || x >= Width || y >= Height) continue;

                yield return new Coordinates(x, y);
            }
        }

        /* --- Entity Query ------------------------------------------------- */

        public IEnumerable<Entity> QueryEntity(Vector2 center, float radius)
        {
            return QueryEntity(new CircleF(center, radius));
        }

        public IEnumerable<Entity> QueryEntity(CircleF c)
        {
            foreach (var e in QueryEntity(c.Bound))
                if (c.Containe(e.Position))
                    yield return e;
        }

        public IEnumerable<Entity> QueryEntity(int tx, int ty)
        {
            return QueryEntity(new Coordinates(tx, ty));
        }

        public IEnumerable<Entity> QueryEntity(Coordinates coords)
        {
            var chunk = GetChunkAt(coords.X, coords.Y);

            if (chunk != null)
                foreach (var e in chunk.EntitiesOnTiles[coords.X % Chunk.SIZE, coords.Y % Chunk.SIZE].Clone())
                    yield return e;
        }

        public IEnumerable<Entity> QueryEntity(Rectangle r)
        {
            return QueryEntity(new RectangleF(r.X, r.Y, r.Width, r.Height));
        }

        public IEnumerable<Entity> QueryEntity(RectangleF r)
        {
            var beginX = r.X / Game.Unit - 1;
            var beginY = r.Y / Game.Unit - 1;

            var endX = (r.X + r.Width) / Game.Unit + 1;
            var endY = (r.Y + r.Height) / Game.Unit + 1;

            for (var x = (int) beginX; x < endX; x++)
            for (var y = (int) beginY; y < endY; y++)
            {
                if (x < 0 || y < 0 || x >= Width || y >= Height) continue;

                foreach (var e in QueryEntity(new Coordinates(x, y)))
                    if (e.GetComponent<ComponentCollider>()?.GetHitBox().IntersectsWith(r) ?? r.Contains(e.Position))
                        yield return e;
            }
        }
    }
}
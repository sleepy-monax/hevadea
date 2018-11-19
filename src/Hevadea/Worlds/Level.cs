using Hevadea.Framework;
using Hevadea.Framework.Extension;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.Graphic.Particles;
using Hevadea.Framework.Utils;
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

            for (int x = 0; x < width / 16; x++)
            {
                for (int y = 0; y < height / 16; y++)
                {
                    Chunks[x, y] = new Chunk(x, y);
                }
            }
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

        public RenderState Prepare(bool drawHint)
        {
            Coordinates focusedTile = _gameState.Camera.FocusedTile;

            Point dist = new Point((_gameState.Camera.GetWidth() / 2 / Game.Unit) + 1,
                                    _gameState.Camera.GetHeight() / 2 / Game.Unit);

            Point renderBegin = new Point(Math.Max(0, focusedTile.X - dist.X),
                                          Math.Max(0, focusedTile.Y - dist.Y - 1));

            Point renderEnd = new Point(Math.Min(Width, focusedTile.X + dist.X + 1),
                                        Math.Min(Height, focusedTile.Y + dist.Y + 6));

            EntityColection onScreenEntities = new EntityColection();
            EntityColection aliveEntities = new EntityColection();

            for (int x = renderBegin.X; x < renderEnd.X; x++)
            {
                for (int y = renderBegin.Y; y < renderEnd.Y; y++)
                {
                    onScreenEntities.AddRange(QueryEntity(x, y));
                }
            }

            if (drawHint) onScreenEntities.SortForRender();

            // TODO: For now the alives entities is equal to on screen entities,
            // BUT: in the futur this will depend on the field of view of all the player connected on the server.
            aliveEntities.AddRange(QueryEntity(focusedTile.ToVector2(), 256));

            return new RenderState(renderBegin, renderEnd, onScreenEntities, aliveEntities);
        }

        public void Update(GameTime gameTime)
        {
            RenderState renderState = Prepare(false);

            // Update all alive entities.
            foreach (var e in renderState.AliveEntities)
            {
                foreach (var sys in SYSTEMS.UpdateSystems)
                {
                    if (sys.Enable && e.Match(sys.Filter))
                    {
                        sys.Update(e, gameTime);
                    }
                }

                e.Update(gameTime);
            }

            // Do the random update of tiles.
            for (int i = 0; i < Width * Height / 50; i++)
            {
                Coordinates tile = new Coordinates(Rise.Rnd.Next(Width), Rise.Rnd.Next(Height));
                GetTile(tile).Update(tile, GetTileDataAt(tile), this, gameTime);
            }

            ParticleSystem.Update(gameTime);
        }

        public void Draw(LevelSpriteBatchPool spriteBatchPool, GameTime gameTime)
        {
            RenderState renderState = Prepare(true);
            spriteBatchPool.Begin(_gameState.Camera);

            // Draw Tiles.
            for (int x = renderState.RenderBegin.X; x < renderState.RenderEnd.X; x++)
            {
                for (int y = renderState.RenderBegin.Y; y < renderState.RenderEnd.Y; y++)
                {
                    Coordinates tile = new Coordinates(x, y);
                    GetTile(tile).Draw(spriteBatchPool.Tiles, tile, GetTileDataAt(tile), this, gameTime);
                }
            }

            ParticleSystem.Draw(spriteBatchPool.Tiles, gameTime);

            // Draw Entities, Shadows and lights.
            foreach (var e in renderState.OnScreenEntities)
            {
                foreach (var sys in SYSTEMS.DrawSystems)
                {
                    if (sys.Enable && e.Match(sys.Filter))
                    {
                        sys.Draw(e, spriteBatchPool, gameTime);
                    }
                }

                // Draw the entity.
                e.Draw(spriteBatchPool.Entities, gameTime);

                // Draw Entity overlay.
                if (Rise.Ui.Enabled)
                {
                    e.Overlay(spriteBatchPool.Overlay, gameTime);
                }

                if (Rise.Debug.GAME)
                {
                    spriteBatchPool.Overlay.PutPixel(e.Position2D, Color.Magenta);
                    spriteBatchPool.Overlay.DrawString(Ressources.FontHack, e.Ueid.ToString(), e.Position2D, Color.Black * 0.5f, Anchor.Center, 1 / _gameState.Camera.Zoom, new Vector2(0, 5f) * 1 / _gameState.Camera.Zoom);
                    spriteBatchPool.Overlay.DrawString(Ressources.FontHack, e.Ueid.ToString(), e.Position2D, ColorPalette.Accent, Anchor.Center, 1 / _gameState.Camera.Zoom, new Vector2(0, 4f) * 1 / _gameState.Camera.Zoom);
                }
            }

            FinalizeDraw(spriteBatchPool);
        }

        private void FinalizeDraw(LevelSpriteBatchPool spriteBatchPool)
        {
            // Get the ambiant lightning.
            Color ambiantLight = Properties.AmbiantLight;

            if (Properties.AffectedByDayNightCycle)
            {
                ambiantLight = _world.DayNightCycle.GetAmbiantLight();
            }

            // Get temporary render targets.
            RenderTarget2D worldRenderTarget = Rise.Graphic.RenderTarget[0];
            RenderTarget2D lightRenderTarget = Rise.Graphic.RenderTarget[1];

            // Draw Entities and tiles to their own rendertarget.
            Rise.Graphic.SetRenderTarget(worldRenderTarget);
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

        public Chunk GetChunkAt(Coordinates t) => GetChunkAt(t.X, t.Y);

        public Chunk GetChunkAt(int tx, int ty)
        {
            if (tx < 0 || ty < 0 || tx >= Width || ty >= Height) return null;
            return Chunks[tx / Chunk.CHUNK_SIZE, ty / Chunk.CHUNK_SIZE];
        }

        /* --- Tiles -------------------------------------------------------- */

        public Tile GetTile(Coordinates t) => GetTile(t.X, t.Y);

        public Tile GetTile(int tx, int ty)
        {
            Chunk chunk = GetChunkAt(tx, ty);

            if (chunk != null)
            {
                return chunk.Tiles[tx % Chunk.CHUNK_SIZE, ty % Chunk.CHUNK_SIZE];
            }

            return TILES.VOID;
        }

        public bool SetTile(Coordinates t, Tile tile) => SetTile(t.X, t.Y, tile);

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

        public Dictionary<string, object> GetTileDataAt(Coordinates t) => GetTileDataAt(t.X, t.Y);

        public Dictionary<string, object> GetTileDataAt(int tx, int ty)
        {
            Chunk chunk = GetChunkAt(tx, ty);

            if (chunk != null)
            {
                return chunk.Data[tx % Chunk.CHUNK_SIZE, ty % Chunk.CHUNK_SIZE];
            }

            return null;
        }

        public T GetTileData<T>(Coordinates t, string dataName, T defaultValue) => GetTileData(t.X, t.Y, dataName, defaultValue);

        public T GetTileData<T>(int tx, int ty, string dataName, T defaultValue)
        {
            return (T)GetTileDataAt(tx, ty).GetValueOrDefault(dataName, defaultValue);
        }

        public void SetTileDataAt(Coordinates t, Dictionary<string, object> data) => SetTileDataAt(t.X, t.Y, data);

        public void SetTileDataAt(int tx, int ty, Dictionary<string, object> data)
        {
            Chunk chunk = GetChunkAt(tx, ty);

            if (chunk != null)
            {
                chunk.Data[tx % Chunk.CHUNK_SIZE, ty % Chunk.CHUNK_SIZE] = data;
            }
        }

        internal void SetTileData<T>(Coordinates t, string dataName, T value) => SetTileData(t.X, t.Y, dataName, value);

        public void SetTileData<T>(int tx, int ty, string dataName, T value)
        {
            GetTileDataAt(tx, ty)[dataName] = value;
        }

        public void ClearTileDataAt(Coordinates tilePosition) => ClearTileDataAt(tilePosition.X, tilePosition.Y);

        public void ClearTileDataAt(int tx, int ty)
        {
            GetTileDataAt(tx, ty)?.Clear();
        }

        /* --- Tile Connections --------------------------------------------- */

        public TileConnection GetTileConnection(Coordinates t) => GetTileConnection(t.X, t.Y);

        public TileConnection GetTileConnection(int tx, int ty)
        {
            Chunk chunk = GetChunkAt(tx, ty);

            if (chunk != null)
            {
                return chunk.CachedTileConnection[tx % Chunk.CHUNK_SIZE, ty % Chunk.CHUNK_SIZE];
            }

            return null;
        }

        public void SetTileConnection(Coordinates t, TileConnection tileConnection) => SetTileConnection(t.X, t.Y, tileConnection);

        public void SetTileConnection(int tx, int ty, TileConnection tileConnection)
        {
            Chunk chunk = GetChunkAt(tx, ty);

            if (chunk != null)
            {
                chunk.CachedTileConnection[tx % Chunk.CHUNK_SIZE, ty % Chunk.CHUNK_SIZE] = tileConnection;
            }
        }

        /* --- Entities ----------------------------------------------------- */

		public Entity AddEntityAt(EntityBlueprint blueprint, Coordinates coordinates)
		    => AddEntityAt(blueprint.Construct(), coordinates.X, coordinates.Y);

		public Entity AddEntityAt(Entity entity, Coordinates coordinates)
		    => AddEntityAt(entity, coordinates.X, coordinates.Y);

        public Entity AddEntityAt(EntityBlueprint blueprint, Coordinates coordinates, Vector2 offset)
		=> AddEntityAt(blueprint.Construct(), coordinates.X, coordinates.Y, offset.X, offset.Y);

		public Entity AddEntityAt(Entity entity, Coordinates coordinates, Vector2 offset)
		=> AddEntityAt(entity, coordinates.X, coordinates.Y, offset.X, offset.Y);

        public Entity AddEntityAt(EntityBlueprint blueprint, int tx, int ty, float offX = 0f, float offY = 0f)
            => AddEntityAt(blueprint.Construct(), tx, ty, offX, offY);

		public Entity AddEntityAt(Entity e, int tx, int ty, float offX = 0f, float offY = 0f)
        {
            AddEntity(e);
            e.Position2D = new Vector2(tx, ty) * Game.Unit + new Vector2(Game.Unit / 2) + new Vector2(offX, offY);
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
            Chunk chunk = GetChunkAt(e.Coordinates);
            chunk.RemoveEntity(e);
        }

        public bool AnyEntityAt(Coordinates coords) => QueryEntity(coords).Any();

        /* --- Entity Query ------------------------------------------------- */

        public IEnumerable<Entity> QueryEntity(Vector2 center, float radius) => QueryEntity(new CircleF(center, radius));
        public IEnumerable<Entity> QueryEntity(CircleF c)
        {
            foreach (var e in QueryEntity(c.Bound))
            {
                if (c.Containe(e.Position2D)) yield return e;
            }
        }

        public IEnumerable<Entity> QueryEntity(int tx, int ty) => QueryEntity(new Coordinates(tx, ty));
        public IEnumerable<Entity> QueryEntity(Coordinates coords)
        {
            Chunk chunk = GetChunkAt(coords.X, coords.Y);

            if (chunk != null)
            {
                foreach (var e in chunk.EntitiesOnTiles[coords.X % Chunk.CHUNK_SIZE, coords.Y % Chunk.CHUNK_SIZE])
                {
                    yield return e;
                }
            }
        }

        public IEnumerable<Entity> QueryEntity(Rectangle r) => QueryEntity(new RectangleF(r.X, r.Y, r.Width, r.Height));
        public IEnumerable<Entity> QueryEntity(RectangleF r)
        {
            var beginX = (r.X / Game.Unit) - 1;
            var beginY = (r.Y / Game.Unit) - 1;

            var endX = ((r.X + r.Width) / Game.Unit) + 1;
            var endY = ((r.Y + r.Height) / Game.Unit) + 1;

            for (int x = (int)beginX; x < endX; x++)
            {
                for (int y = (int)beginY; y < endY; y++)
                {
                    if (x < 0 || y < 0 || x >= Width || y >= Height) continue;

                    foreach (var e in QueryEntity(new Coordinates(x, y)))
                    {
                        if (e.GetComponent<Colider>()?.GetHitBox().IntersectsWith(r) ?? r.Contains(e.Position2D))
                        {
                            yield return e;
                        }
                    }
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Hevadea.Framework.Utils;
using Hevadea.Game.Entities;
using Hevadea.Game.Entities.Component;
using Hevadea.Game.Registry;
using Hevadea.Game.Storage;
using Hevadea.Game.Tiles;
using Maker.Rise;
using Maker.Rise.Extension;
using Maker.Rise.Graphic.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Game.Worlds
{
    public class Level
    {
        private int[] _tiles;
        private Dictionary<string, object>[] _tilesData;
        public LevelProperties Properties { get; }
        
        public ParticleSystem ParticleSystem { get; }
        public GameManager Game;
        private List<Entity> _entities;
        private List<Entity>[,] _entitiesOnTiles;


        private World World;

        public Level(LevelProperties properties, int width, int height)
        {
            Properties = properties;
            Width = width;
            Height = height;
            
            ParticleSystem = new ParticleSystem();

            _tiles = new int[Width * Height];
            _tilesData = new Dictionary<string, object>[Width * Height];
            _entities = new List<Entity>();
            _entitiesOnTiles = new List<Entity>[Width, Height];
            
            for (var x = 0; x < Width; x++)
            for (var y = 0; y < Height; y++)
            {
                _entitiesOnTiles[x, y] = new List<Entity>();
                _tilesData[x + y * Width] = new Dictionary<string, object>();
            }
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public int Width { get; private set; }
        public int Height { get; private set; }

        // ENTITIES -----------------------------------------------------------
        public void AddEntity(Entity e, float x, float y)
        {
            AddEntity(e);
            e.SetPosition(x, y);
        }

        public Entity SpawnEntity(Entity entity, int tx, int ty, float offX = 0f, float offY = 0f)
        {
            AddEntity(entity);
            entity.SetPosition(tx * ConstVal.TileSize + (ConstVal.TileSize / 2 - entity.Width / 2) + offX, ty * ConstVal.TileSize + (ConstVal.TileSize / 2 - entity.Height / 2) + offY);
            return entity;
        }

        public Entity SpawnEntity(EntityBlueprint blueprint, int tx, int ty, float offX = 0f, float offY = 0f)
        {
            var entity = blueprint.Build();
            SpawnEntity(entity, tx, ty, offX, offY);
            return entity;
        }
        
        public void AddEntity(Entity e)
        {
            e.Removed = false;
            if (!_entities.Contains(e)) _entities.Add(e);

            e.Initialize(this, World, Game);
            AddEntityToTile(e.GetTilePosition(), e);
        }

        public void RemoveEntity(Entity e)
        {
            _entities.Remove(e);
            RemoveEntityFromTile(e.GetTilePosition(), e);
            e.Removed = true;
        }

        public void AddEntityToTile(TilePosition p, Entity e)
        {
            if (p.X < 0 || p.Y < 0 || p.X >= Width || p.Y >= Height) return;
            _entitiesOnTiles[p.X, p.Y].Add(e);
        }

        public void RemoveEntityFromTile(TilePosition p, Entity e)
        {
            if (p.X < 0 || p.Y < 0 || p.X >= Width || p.Y >= Height) return;
            _entitiesOnTiles[p.X, p.Y].Remove(e);
        }

        internal List<Entity> GetEntityOnTile(TilePosition selectedTile)
        {
            return GetEntityOnTile(selectedTile.X, selectedTile.Y);
        }

        public List<Entity> GetEntityOnTile(int tx, int ty)
        {
            var result = new List<Entity>();


            if (tx < Width && ty < Height && tx >= 0 && ty >= 0) result.AddRange(_entitiesOnTiles[tx, ty]);

            return result;
        }

        public List<Entity> GetEntitiesOnArea(Rectangle area)
        {
            var result = new List<Entity>();

            var beginX = area.X / ConstVal.TileSize - 1;
            var beginY = area.Y / ConstVal.TileSize - 1;

            var endX = (area.X + area.Width) / ConstVal.TileSize + 1;
            var endY = (area.Y + area.Height) / ConstVal.TileSize + 1;


            for (var x = beginX; x < endX; x++)
            for (var y = beginY; y < endY; y++)
            {
                if (x < 0 || y < 0 || x >= Width || y >= Height) continue;
                var entities = _entitiesOnTiles[x, y];
                result.AddRange(entities.Where(i => i.IsColliding(area)));
            }

            return result;
        }

        public bool IsAll(Tile tile, Rectangle rectangle)
        {
            bool result = true;

            var beginX = rectangle.X / ConstVal.TileSize - 1;
            var beginY = rectangle.Y / ConstVal.TileSize - 1;

            var endX = (rectangle.X + rectangle.Width) / ConstVal.TileSize + 1;
            var endY = (rectangle.Y + rectangle.Height) / ConstVal.TileSize + 1;


            for (var x = beginX; x < endX; x++)
            for (var y = beginY; y < endY; y++)
            {
                if (x < 0 || y < 0 || x >= Width || y >= Height) continue;
                result &= GetTile(x, y) == tile;
            }

            return result;
        }
        
        public bool IsAll<T>(Rectangle rectangle) where T: TileTag
        {

            var beginX = rectangle.X / ConstVal.TileSize;
            var beginY = rectangle.Y / ConstVal.TileSize;

            var endX = (rectangle.X + rectangle.Width) / ConstVal.TileSize;
            var endY = (rectangle.Y + rectangle.Height) / ConstVal.TileSize;


            bool result =  GetTile(beginX, beginY).HasTag<T>();;
            for (var x = beginX; x <= endX; x++)
            for (var y = beginY; y <= endY; y++)
            {
                if (x < 0 || y < 0 || x >= Width || y >= Height) continue;
                result &= GetTile(x, y).HasTag<T>();
            }

            return result;
        }

        // TILES --------------------------------------------------------------

        public Tile GetTile(TilePosition tPos)
        {
            return GetTile(tPos.X, tPos.Y);
        }

        public Tile GetTile(int tx, int ty)
        {
            if (tx < 0 || ty < 0 || tx >= Width || ty >= Height) return TILES.WATER;
            return TILES.ById[_tiles[tx + ty * Width]];
        }

        public bool SetTile(TilePosition pos, Tile tile)
        {
            return SetTile(pos.X, pos.Y, tile.Id);
        }

        public bool SetTile(int tx, int ty, Tile tile)
        {
            return SetTile(tx, ty, tile.Id);
        }

        public bool SetTile(int tx, int ty, int id)
        {
            if (tx < 0 || ty < 0 || tx >= Width || ty >= Height) return false;
            _tiles[tx + ty * Width] = id;
            return true;
        }

        public void ClearTileData(TilePosition tilePosition) => ClearTileData(tilePosition.X, tilePosition.Y);

        public void ClearTileData(int tx, int ty)
        {
            _tilesData[tx + ty * Width].Clear();
        }

        public T GetTileData<T>(TilePosition tilePosition, string dataName, T defaultValue)
        {
            return GetTileData(tilePosition.X, tilePosition.Y, dataName, defaultValue);
        }

        public T GetTileData<T>(int tx, int ty, string dataName, T defaultValue)
        {
            if (_tilesData[tx + ty * Width].ContainsKey(dataName)) return (T) _tilesData[tx + ty * Width][dataName];

            _tilesData[tx + ty * Width].Add(dataName, defaultValue);
            return defaultValue;
        }

        internal void SetTileData<T>(TilePosition tilePosition, string dataName, T value)
        {
            SetTileData(tilePosition.X, tilePosition.Y, dataName, value);
        }

        public void SetTileData<T>(int tx, int ty, string dataName, T value)
        {
            _tilesData[tx + ty * Width][dataName] = value;
        }

        // GAME LOOPS ---------------------------------------------------------

        public void Initialize(World world, GameManager game)
        {
            Logger.Log<Level>(LoggerLevel.Info, "Initializing level...");
            World = world;
            Game = game;
            Logger.Log<Level>(LoggerLevel.Info, "Initializing entities...");
            foreach (var e in _entities) e.Initialize(this, world, Game);
            Logger.Log<Level>(LoggerLevel.Fine, "Done!");
        }

        public LevelStorage Save()
        {
            Logger.Log<Level>(LoggerLevel.Info, "Saving level...");
            var store = new LevelStorage
            {
                Width = Width,
                Height = Height,
                Tiles = _tiles,
                TilesData = _tilesData,
                Name = Name,
                Id = Id, 
                Type = Properties.Name
            };

            Logger.Log<Level>(LoggerLevel.Info, "Saving entities...");
            foreach (var e in _entities) store.Entities.Add(e.Save());

            Logger.Log<Level>(LoggerLevel.Fine, "Done!");
            return store;
        }

        public void Load(LevelStorage store)
        {
            var asm = Assembly.GetExecutingAssembly();
            foreach (var item in store.Entities)
            {
                var e = ENTITIES.GetBlueprint(item.Type).Build();
                e.Load(item);
                AddEntity(e);
            }

            Width = store.Width;
            Height = store.Height;
            _tiles = store.Tiles;
            _tilesData = store.TilesData;
            Name = store.Name;
            Id = store.Id;
        }

        public void Update(LevelRenderState state, GameTime gameTime)
        {
            // Randome tick tiles.
            for (var i = 0; i < Width * Height / 50; i++)
            {
                var tx = Engine.Random.Next(Width);
                var ty = Engine.Random.Next(Height);
                GetTile(tx, ty).Update(new TilePosition(tx, ty), _tilesData[tx + ty * Width], this, gameTime);
            }

            ParticleSystem.Update(gameTime);
            
            // Update entities
            foreach (var e in state.OnScreenEntities) e.Update(gameTime);
        }

        public LevelRenderState GetRenderState(Camera camera)
        {
            var entitiesOnScreen = new List<Entity>();
            var focusEntity = new Point((int) camera.X / ConstVal.TileSize, (int) camera.Y / ConstVal.TileSize);
            var dist = new Point(camera.GetWidth() / 2 / ConstVal.TileSize + 4,
                camera.GetHeight() / 2 / ConstVal.TileSize + 4);

            var state = new LevelRenderState
            {
                Begin = new Point(Math.Max(0, focusEntity.X - dist.X),
                    Math.Max(0, focusEntity.Y - dist.Y + 1)),

                End = new Point(Math.Min(Width, focusEntity.X + dist.X + 1),
                    Math.Min(Height, focusEntity.Y + dist.Y + 6))
            };

            for (var tx = state.Begin.X; tx < state.End.X; tx++)
            for (var ty = state.Begin.Y; ty < state.End.Y; ty++)
                entitiesOnScreen.AddRange(_entitiesOnTiles[tx, ty]);

            entitiesOnScreen.Sort((a, b) => (a.Y + a.Origin.Y).CompareTo(b.Y + b.Origin.Y));

            state.OnScreenEntities = entitiesOnScreen;

            return state;
        }

        public void DrawTerrain(LevelRenderState state, SpriteBatch spriteBatch, GameTime gameTime)
        {
            for (var tx = state.Begin.X; tx < state.End.X; tx++)
            for (var ty = state.Begin.Y; ty < state.End.Y; ty++)
                GetTile(tx, ty).Draw(spriteBatch, new TilePosition(tx, ty), _tilesData[tx + ty * Width], this, gameTime);
            
            ParticleSystem.Draw(spriteBatch, gameTime);
        }

        public void DrawEntities(LevelRenderState state, SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (var e in state.OnScreenEntities) e.Draw(spriteBatch, gameTime);
        }

        public void DrawEntitiesOverlay(LevelRenderState state, SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (var e in state.OnScreenEntities)
            {
                e.DrawOverlay(spriteBatch, gameTime);

                if (Engine.Debug.Visible)
                {
                    spriteBatch.DrawRectangle(e.Bound, Color.Aqua * 0.5f, 0.4f);
                    spriteBatch.PutPixel(e.Position + e.Origin.ToVector2(), Color.Magenta);
                }
            }
        }

        public void DrawLightMap(LevelRenderState state, SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (var e in state.OnScreenEntities)
            {
                var light = e.Components.Get<Light>();

                if (light != null)
                    spriteBatch.Draw(Ressources.ImgLight,
                        new Rectangle((int) e.X - light.Power + e.Width / 2, (int) e.Y - light.Power + e.Height / 2,
                            light.Power * 2, light.Power * 2), light.Color);
            }
        }
    }
}
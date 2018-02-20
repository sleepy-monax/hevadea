using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.Entities.Component;
using Maker.Hevadea.Game.Registry;
using Maker.Hevadea.Game.Storage;
using Maker.Hevadea.Game.Tiles;
using Maker.Rise;
using Maker.Rise.Extension;
using Maker.Utils;
using Maker.Utils.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Maker.Hevadea.Game
{
    public class Level
    {
        public List<Entity> Entities;
        public List<Entity>[,] EntitiesOnTiles;
        public List<Entity> EntitiesToRemove = new List<Entity>();
        private GameManager Game;

        private int[] Tiles;
        private Dictionary<string, object>[] TilesData;

        private World World;

        public Level(int width, int height)
        {
            Width = width;
            Height = height;

            Tiles = new int[Width * Height];
            TilesData = new Dictionary<string, object>[Width * Height];
            Entities = new List<Entity>();
            EntitiesOnTiles = new List<Entity>[Width, Height];

            for (var x = 0; x < Width; x++)
            for (var y = 0; y < Height; y++)
            {
                EntitiesOnTiles[x, y] = new List<Entity>();
                TilesData[x + y * Width] = new Dictionary<string, object>();
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

        public void SpawnEntity(Entity e, int tx, int ty, float offX = 0f, float offY = 0f)
        {
            AddEntity(e);
            e.SetPosition(tx * ConstVal.TileSize + (ConstVal.TileSize / 2 - e.Width / 2) + offX,
                ty * ConstVal.TileSize + (ConstVal.TileSize / 2 - e.Height / 2) + offY);
        }

        public void AddEntity(Entity e)
        {
            e.Removed = false;
            if (!Entities.Contains(e)) Entities.Add(e);

            e.Initialize(this, World, Game);
            AddEntityToTile(e.GetTilePosition(), e);
        }

        public void RemoveEntity(Entity e)
        {
            EntitiesToRemove.Add(e);
        }

        public void AddEntityToTile(TilePosition p, Entity e)
        {
            if (p.X < 0 || p.Y < 0 || p.X >= Width || p.Y >= Height) return;
            EntitiesOnTiles[p.X, p.Y].Add(e);
        }

        public void RemoveEntityFromTile(TilePosition p, Entity e)
        {
            if (p.X < 0 || p.Y < 0 || p.X >= Width || p.Y >= Height) return;
            EntitiesOnTiles[p.X, p.Y].Remove(e);
        }

        internal List<Entity> GetEntityOnTile(TilePosition selectedTile)
        {
            return GetEntityOnTile(selectedTile.X, selectedTile.Y);
        }

        public List<Entity> GetEntityOnTile(int tx, int ty)
        {
            var result = new List<Entity>();


            if (tx < Width && ty < Height && tx >= 0 && ty >= 0) result.AddRange(EntitiesOnTiles[tx, ty]);

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
                var entities = EntitiesOnTiles[x, y];
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
            return TILES.ById[Tiles[tx + ty * Width]];
        }

        public void SetTile(TilePosition pos, Tile tile)
        {
            SetTile(pos.X, pos.Y, tile.Id);
        }

        public void SetTile(int tx, int ty, Tile tile)
        {
            SetTile(tx, ty, tile.Id);
        }

        public void SetTile(int tx, int ty, int id)
        {
            if (tx < 0 || ty < 0 || tx >= Width || ty >= Height) return;
            Tiles[tx + ty * Width] = id;
        }

        internal T GetTileData<T>(TilePosition tilePosition, string dataName, T defaultValue)
        {
            return GetTileData(tilePosition.X, tilePosition.Y, dataName, defaultValue);
        }

        public T GetTileData<T>(int tx, int ty, string dataName, T defaultValue)
        {
            if (TilesData[tx + ty * Width].ContainsKey(dataName)) return (T) TilesData[tx + ty * Width][dataName];

            TilesData[tx + ty * Width].Add(dataName, defaultValue);
            return defaultValue;
        }

        internal void SetTileData<T>(TilePosition tilePosition, string dataName, T value)
        {
            SetTileData(tilePosition.X, tilePosition.Y, dataName, value);
        }

        public void SetTileData<T>(int tx, int ty, string dataName, T value)
        {
            TilesData[tx + ty * Width][dataName] = value;
        }

        // GAME LOOPS ---------------------------------------------------------

        public void Initialize(World world, GameManager game)
        {
            Logger.Log<Level>(LoggerLevel.Info, "Initializing level...");
            World = world;
            Game = game;
            Logger.Log<Level>(LoggerLevel.Info, "Initializing entities...");
            foreach (var e in Entities) e.Initialize(this, world, Game);
            Logger.Log<Level>(LoggerLevel.Fine, "Done!");
        }

        public LevelStorage Save()
        {
            Logger.Log<Level>(LoggerLevel.Info, "Saving level...");
            var store = new LevelStorage
            {
                Width = Width,
                Height = Height,
                Tiles = Tiles,
                TilesData = TilesData,
                Name = Name,
                Id = Id
            };

            Logger.Log<Level>(LoggerLevel.Info, "Saving entities...");
            foreach (var e in Entities) store.Entities.Add(e.Save());

            Logger.Log<Level>(LoggerLevel.Fine, "Done!");
            return store;
        }

        public void Load(LevelStorage store)
        {
            var asm = Assembly.GetExecutingAssembly();
            foreach (var item in store.Entities)
            {
                var e = (Entity) asm.CreateInstance(item.Type);
                e.Load(item);
                AddEntity(e);
            }

            Width = store.Width;
            Height = store.Height;
            Tiles = store.Tiles;
            TilesData = store.TilesData;
        }

        public void Update(GameTime gameTime)
        {
            // Randome tick tiles.
            for (var i = 0; i < Width * Height / 50; i++)
            {
                var tx = Engine.Random.Next(Width);
                var ty = Engine.Random.Next(Height);
                GetTile(tx, ty).Update(new TilePosition(tx, ty), TilesData[tx + ty * Width], this, gameTime);
            }

            // Update entities
            foreach (var e in Entities) e.Update(gameTime);

            // Remove removed entities.
            foreach (var er in EntitiesToRemove)
            {
                Entities.Remove(er);
                RemoveEntityFromTile(er.GetTilePosition(), er);
                er.Removed = true;
            }

            EntitiesToRemove.Clear();
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
                    Math.Min(Height, focusEntity.Y + dist.Y + 1))
            };

            for (var tx = state.Begin.X; tx < state.End.X; tx++)
            for (var ty = state.Begin.Y; ty < state.End.Y; ty++)
                entitiesOnScreen.AddRange(EntitiesOnTiles[tx, ty]);

            entitiesOnScreen.Sort((a, b) => (a.Y + a.Origin.Y).CompareTo(b.Y + b.Origin.Y));

            state.OnScreenEntities = entitiesOnScreen;

            return state;
        }

        public void DrawTerrain(LevelRenderState state, SpriteBatch spriteBatch, GameTime gameTime)
        {
            for (var tx = state.Begin.X; tx < state.End.X; tx++)
            for (var ty = state.Begin.Y; ty < state.End.Y; ty++)
                GetTile(tx, ty).Draw(spriteBatch, new TilePosition(tx, ty), TilesData[tx + ty * Width], this, gameTime);
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
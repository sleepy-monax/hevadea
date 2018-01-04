using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.SaveStorage;
using Maker.Hevadea.Game.Tiles;
using Maker.Hevadea.Json;
using Maker.Rise.UI;
using Maker.Rise.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;

namespace Maker.Hevadea.Game
{
    public class Level
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Color AmbiantLight { get; set; } = Color.Blue * 0.25f;

        private byte[] Tiles;
        private Dictionary<string, object>[] TilesData;

        public List<Entity> Entities;
        public List<Entity>[,] EntitiesOnTiles;

        public Color NightColor = Color.Blue * 0.25f;
        public Color DayColor = Color.White;


        bool ItsNight = false;
        Animation dayNightTransition = new Animation { Speed = 0.003f };

        private Random Random;
        private World World;

        public Level(int w, int h)
        {
            Width = w;
            
            Height = h;
            Tiles = new byte[Width * Height];
            TilesData = new Dictionary<string, object>[Width * Height];
            Entities = new List<Entity>();
            EntitiesOnTiles = new List<Entity>[Width, Height];
            Random = new Random();

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    EntitiesOnTiles[x, y] = new List<Entity>();
                    TilesData[x + y * Width] = new Dictionary<string, object>();
                }
            }
        }

        // ENTITIES -----------------------------------------------------------

        public void AddEntity(Entity e)
        {
            e.Removed = false;
            Entities.Add(e);

            e.Initialize(this, World);
            AddEntityToTile(e.GetTilePosition(), e);
        }

        public void RemoveEntity(Entity e)
        {
            Entities.Remove(e);
            RemoveEntityFromTile(e.GetTilePosition(), e);
        }

        private void AddEntityToTile(TilePosition p, Entity e)
        {
            if (p.X < 0 || p.Y < 0 || p.X >= Width || p.Y >= Height) return;
            EntitiesOnTiles[p.X, p.Y].Add(e);
        }

        private void RemoveEntityFromTile(TilePosition p, Entity e)
        {
            if (p.X < 0 || p.Y < 0 || p.X >= Width || p.Y >= Height) return;
            EntitiesOnTiles[p.X, p.Y].Remove(e);
        }

        public List<Entity> GetEntityOnTile(int tx, int ty)
        {
            if (tx < Width && ty < Height)
            {
                return EntitiesOnTiles[tx, ty];
            }
            else
            {
                return new List<Entity>();
            }
        }

        public List<Entity> GetEntitiesOnArea(Rectangle area)
        {
            var result = new List<Entity>();

            var beginX = area.X / ConstVal.TileSize - 1;
            var beginY = area.Y / ConstVal.TileSize - 1;

            var endX = (area.X + area.Width)  / ConstVal.TileSize + 1;
            var endY = (area.Y + area.Height) / ConstVal.TileSize + 1;


            for (int x = beginX; x < endX; x++)
            {
                for (int y = beginY; y < endY; y++)
                {
                    if (x < 0 || y < 0 || x >= Width || y >= Height) continue;

                    var entities = EntitiesOnTiles[x, y];

                    foreach (var i in entities)
                    {
                        if (i.IsColliding(area)) { result.Add(i); }
                    }
                    
                }
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
            if (tx< 0 || ty < 0 || tx>= Width || ty>= Height) return Tile.Water;
            return Tile.Tiles[Tiles[tx + ty * Width]];
        }

        public void SetTile(int tx, int ty, Tile tile)
        {
            SetTile(tx, ty, tile.ID);
        }

        public void SetTile(int tx, int ty, byte id)
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
            if (TilesData[tx + ty * Width].ContainsKey(dataName))
            {
                return (T)TilesData[tx + ty * Width][dataName];
            }

            TilesData[tx + ty * Width].Add(dataName, defaultValue);
            return defaultValue;
        }

        internal void SetTileData<T>(TilePosition tilePosition, string dataName, T value)
        {
            SetTileData(tilePosition.X, tilePosition.Y, dataName, value);
        }

        public void SetTileData<T>(int tx, int ty, string dataName, T Value)
        {
            TilesData[tx + ty * Width][dataName] = Value;
        }

        // GAME LOOPS ---------------------------------------------------------

        public void Initialize(World world)
        {
            this.World = world;
        }

        public void Update(GameTime gameTime)
        {
            // Randome tick tiles.
            for (int i = 0; i < Width * Height / 50; i++)
            {
                var tx = Random.Next(Width);
                var ty = Random.Next(Height);
                GetTile(tx, ty).Update(this, tx, ty);
            }

            // Tick entities.
            for (int i = 0; i < Entities.Count; i++)
            {
                var e = Entities[i];

                var oldPosition = e.GetTilePosition();

                e.Update(gameTime);

                if (e.Removed)
                {
                    Entities.RemoveAt(i);
                    i--;
                    RemoveEntityFromTile(oldPosition, e);
                }
                else
                {
                    var newPosition = e.GetTilePosition();

                    if (oldPosition != newPosition)
                    {
                        RemoveEntityFromTile(oldPosition, e);
                        AddEntityToTile(newPosition, e);
                    }
                }
            }

            // Ambiant light
            var time = ((World.Time % 24000) / 24000f) ;
            dayNightTransition.Update(gameTime);
            AmbiantLight = GetAmbiantLightColor(time);
        }


        private Color GetAmbiantLightColor(float time, float dayDuration = 0.5f, float nightDuration = 0.5f)
        {
            ItsNight = time > dayDuration;

            dayNightTransition.Show = time > (dayDuration - 0.05);
            

            var day = DayColor * (1f - dayNightTransition.SinLinear);
            var night = NightColor * dayNightTransition.SinLinear;

            //Console.WriteLine($"{(int)(time * 100), 3} {ItsNight} {day} {night} {dayNightTransition.SinLinear}");

            return new Color(
                day.R + night.R,
                day.G + night.G,
                day.B + night.B,
                day.A + night.A);

        }

        public void Draw(SpriteBatch spriteBatch, SpriteBatch lightSb, Camera camera, GameTime gameTime, bool showDebug, bool renderTiles = true, bool renderEntities = true)
        {
            var focusEntity = camera.FocusEntity.GetTilePosition();
            
            // Get the render frame
            var dist = new Point(((camera.GetWidth()  / 2) / ConstVal.TileSize) + 4,
                                 ((camera.GetHeight() / 2) / ConstVal.TileSize) + 4);
            
            var begin = new Point(Math.Max(0, focusEntity.X - dist.X),
                                  Math.Max(0, focusEntity.Y - dist.Y + 1));

            var end = new Point(Math.Min(Width,  focusEntity.X + dist.X + 1),
                                Math.Min(Height, focusEntity.Y + dist.Y + 1));

            List<Entity> entitiesOnScreen = new List<Entity>();

            // Render tiles
            for (int tx = begin.X; tx < end.X; tx++)
            {
                for (int ty = begin.Y; ty < end.Y; ty++)
                {
                    entitiesOnScreen.AddRange(EntitiesOnTiles[tx, ty]);
                    if (renderTiles) GetTile(tx, ty).Draw(spriteBatch, gameTime, this, new TilePosition(tx, ty));
                    if (showDebug) spriteBatch.DrawRectangle(new Rectangle(tx * ConstVal.TileSize + 1, ty * ConstVal.TileSize + 1, ConstVal.TileSize - 2, ConstVal.TileSize - 2), new Color(255,255,255));
                }
            }

            entitiesOnScreen.Sort((a, b) => (a.Y + a.Height).CompareTo(b.Y + b.Height));

            // Render Entities
            foreach (var e in entitiesOnScreen)
            {
                if (renderEntities)    e.Draw(spriteBatch, gameTime);
                if (showDebug)       spriteBatch.FillRectangle(e.Bound, new Color(255, 0, 0) * 0.45f);
                if (e.Light.On) lightSb.Draw(Ressources.img_light, new Rectangle(e.X - e.Light.Power + e.Width / 2, e.Y - e.Light.Power + e.Height / 2, e.Light.Power * 2, e.Light.Power * 2), e.Light.Color);
            }

        }

        public static bool Save(Level level, string folderName)
        {
            var storedTile = new List<TileSaveStorage>();
            var storedEntity = new List<EntitySaveStorage>();
            var storedLevel = new LevelSaveStorage { Height = level.Height, Width = level.Width };

            for (int i = 0; i < level.Width * level.Height; i++)
            {
                storedTile.Add(new TileSaveStorage { ID = level.Tiles[i], Data = level.TilesData[i]});
            }

            foreach (var e in level.Entities)
            {
                storedEntity.Add(new EntitySaveStorage { Type = e.GetType().FullName, Data = e.ToJson() });
            }

            File.WriteAllText(folderName + "entities.json", storedEntity.ToJson());
            File.WriteAllText(folderName + "tiles.json", storedTile.ToJson());
            File.WriteAllText(folderName + "level.json", storedLevel.ToJson());

            return true;
        }

        public static Level Load(string fileName)
        {
            // TODO: level loading.
            return null;
        }
    }
}

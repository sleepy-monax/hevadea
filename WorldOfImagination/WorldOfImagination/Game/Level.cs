using Maker.Rise.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using WorldOfImagination.Game.Entities;
using WorldOfImagination.Game.SaveStorage;
using WorldOfImagination.Game.Tiles;
using WorldOfImagination.Json;

namespace WorldOfImagination.Game
{
    public class Level
    {
        public readonly int W;
        public readonly int H;

        private byte[] Tiles;
        private Dictionary<string, object>[] Data;

        public List<Entity> Entities;
        public List<Entity>[,] EntityOnTiles;
        public Player Player;

        private Random rnd;
        private World world;

        public Level(int w, int h)
        {
            W = w;
            
            H = h;
            Tiles = new byte[W * H];
            Data = new Dictionary<string, object>[W * H];
            Entities = new List<Entity>();
            EntityOnTiles = new List<Entity>[W, H];
            rnd = new Random();

            for (int x = 0; x < W; x++)
            {
                for (int y = 0; y < H; y++)
                {
                    EntityOnTiles[x, y] = new List<Entity>();
                    Data[x + y * W] = new Dictionary<string, object>();
                }
            }
        }

        // ENTITIES -----------------------------------------------------------

        public void AddEntity(Entity e)
        {
            if (e is Player p) { Player = p; };

            e.Removed = false;
            Entities.Add(e);

            e.Init(this, world);
            AddEntityToTile(e.Position.ToTilePosition(), e);
        }

        public void RemoveEntity(Entity e)
        {
            Entities.Remove(e);
            var tilePosition = e.Position.ToTilePosition();
            RemoveEntityFromTile(tilePosition, e);
        }

        private void AddEntityToTile(TilePosition p, Entity e)
        {
            if (p.X < 0 || p.Y < 0 || p.X >= W || p.Y >= H) return;
            EntityOnTiles[p.X, p.Y].Add(e);
        }

        private void RemoveEntityFromTile(TilePosition p, Entity e)
        {
            if (p.X < 0 || p.Y < 0 || p.X >= W || p.Y >= H) return;
            EntityOnTiles[p.X, p.Y].Remove(e);
        }

        public List<Entity> GetEntitiesOnArea(EntityPosition p, int width, int height)
        {
            var result = new List<Entity>();

            var from = p.ToTilePosition();
            from.Y--;
            from.Y--;

            var to = new EntityPosition(p.X + width, p.Y + height).ToTilePosition();
            to.X++;
            to.Y++;

            for (int x = from.X; x < to.X; x++)
            {
                for (int y = from.Y; y < to.Y; y++)
                {
                    if (x < 0 || y < 0 || x >= W || y >= H) continue;

                    var entities = EntityOnTiles[x, y];

                    foreach (var i in entities)
                    {
                        if (i.Colide(p, width, height)) { result.Add(i); }
                    }
                    
                }
            }

            return result;
        }

        // TILES --------------------------------------------------------------
        public Tile GetTile(int tx, int ty)
        {
            if (tx< 0 || ty < 0 || tx>= W || ty>= H) return Tile.Rock;
            return Tile.Tiles[Tiles[tx + ty * W]];
        }

        public void SetTile(int tx, int ty, byte id)
        {
            if (tx < 0 || ty < 0 || tx >= W || ty >= H) return;
            Tiles[tx + ty * W] = id;
        }

        public T GetData<T>(int tx, int ty, string dataName, T defaultValue)
        {
            if (Data[tx + ty * W].ContainsKey(dataName))
            {
                return (T)Data[tx + ty * W][dataName];
            }

            return defaultValue;
        }

        public void SetData<T>(int tx, int ty, string dataName, T Value)
        {
            Data[tx + ty * W][dataName] = Value;
        }

        // GAME LOOPS ---------------------------------------------------------

        public void Initialize(World world)
        {
            this.world = world;
        }

        public void Update(GameTime gameTime)
        {
            // Randome tick tiles.
            for (int i = 0; i < W * H / 50; i++)
            {
                var tx = rnd.Next(W);
                var ty = rnd.Next(H);
                GetTile(tx, ty).Update(this, tx, ty);
            }

            // Tick entities.
            for (int i = 0; i < Entities.Count; i++)
            {
                Entity e = Entities[i];

                var oldPosition = new EntityPosition(e.Position).ToTilePosition();

                e.Update(gameTime);

                if (e.Removed)
                {
                    Entities.RemoveAt(i--);
                    RemoveEntityFromTile(oldPosition, e);
                }
                else
                {
                    var newPosition = e.Position.ToTilePosition();

                    if (oldPosition != newPosition)
                    {
                        RemoveEntityFromTile(oldPosition, e);
                        AddEntityToTile(newPosition, e);
                    }
                }
            }
        }


        public void Draw(SpriteBatch sb, Camera camera, GameTime gameTime, bool showDebug)
        {
            var playerPos = Player.Position.ToTilePosition();
            
            var distX = ((camera.GetWidth() / 2) / ConstVal.TileSize);
            var distY = ((camera.GetHeight() / 2) / ConstVal.TileSize);
            
            var beginX = Math.Max(0, playerPos.X - distX - 5);
            var beginY = Math.Max(0, playerPos.Y - distY - 5);
            var endX = Math.Min(W, playerPos.X + distX + 5);
            var endY = Math.Min(H, playerPos.Y + distY + 5);

            List<Entity> EntityRenderList = new List<Entity>();

            for (int tx = beginX; tx < endX; tx++)
            {
                for (int ty = beginY; ty < endY; ty++)
                {
                    GetTile(tx, ty).Draw(sb, gameTime, this, new TilePosition(tx, ty));
                    if (showDebug) sb.DrawRectangle(new Rectangle(tx * ConstVal.TileSize, ty * ConstVal.TileSize, ConstVal.TileSize, ConstVal.TileSize), new Color(255,255,255) * 0.25f);
                    EntityRenderList.AddRange(EntityOnTiles[tx, ty]);
                }
            }

            EntityRenderList.Sort((a, b) => (a.Position.Y + a.Height).CompareTo(b.Position.Y + b.Height));

            foreach (var e in EntityRenderList)
            {
                if (showDebug) sb.DrawRectangle(e.ToRectangle(), new Color(255, 0, 0) * 0.25f);
                e.Draw(sb, gameTime);
            }
        }

        public static bool Save(Level level, string folderName)
        {
            List<TileSaveStorage> storedTile = new List<TileSaveStorage>();
            List<EntitySaveStorage> storedEntity = new List<EntitySaveStorage>();
            LevelSaveStorage storedLevel = new LevelSaveStorage { Height = level.H, Width = level.W };

            for (int i = 0; i < level.W * level.H; i++)
            {
                storedTile.Add(new TileSaveStorage { ID = level.Tiles[i], Data = level.Data[i]});
            }

            foreach (var e in level.Entities)
            {

                storedEntity.Add(new EntitySaveStorage() { Type = e.GetType().FullName, Data = e.ToJson() });
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

using Maker.Rise.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using WorldOfImagination.Game.Entities;
using WorldOfImagination.Game.Tiles;

namespace WorldOfImagination.Game
{
    public class Level
    {
        public readonly int W;
        public readonly int H;

        public byte[,] Tiles;
        public byte[,] Data;
        public List<Entity> Entities;
        public List<Entity>[,] EntityOnTiles;
        public Player Player;

        private Random rnd;

        public Level(int w, int h)
        {
            W = w;
            H = h;
            Tiles = new byte[W, H];
            Data = new byte[W, H];
            Entities = new List<Entity>();
            EntityOnTiles = new List<Entity>[W, H];
            rnd = new Random();

            for (int x = 0; x < W; x++)
            {
                for (int y = 0; y < H; y++)
                {
                    EntityOnTiles[x, y] = new List<Entity>();
                }
            }
        }


        // ENTITIES -----------------------------------------------------------

        public void AddEntity(Entity e)
        {
            if (e is Player p) { Player = p; };

            e.Removed = false;
            Entities.Add(e);

            e.Init(this);
            AddEntityToTile(e.Position.ToTilePosition(), e);
        }

        public void RemoveEntity(Entity e)
        {
            Entities.Remove(e);
            var tilePosition = e.Position.ToTilePosition();
            RemoveEntityFromTile(tilePosition, e);
        }

        public void AddEntityToTile(TilePosition p, Entity e)
        {
            if (p.X < 0 || p.Y < 0 || p.X >= W || p.Y >= H) return;
            EntityOnTiles[p.X, p.Y].Add(e);
        }

        public void RemoveEntityFromTile(TilePosition p, Entity e)
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
        public Tile GetTile(TilePosition p)
        {
            if (p.X < 0 || p.Y < 0 || p.X >= W || p.Y >= H) return null;
            return Tile.Tiles[Tiles[p.X, p.Y]];
        }

        public void SetTile(TilePosition p, byte id, byte data)
        {
            if (p.X < 0 || p.Y < 0 || p.X >= W || p.Y >= H) return;
            Tiles[p.X, p.Y] = id;
            Data[p.X, p.Y] = data;
        }

        // GAME LOOPS ---------------------------------------------------------

        public void Update(GameTime gameTime)
        {
            // Randome tick tiles.
            for (int i = 0; i < W * H / 50; i++)
            {
                var pos = new TilePosition(rnd.Next(W), rnd.Next(H));

                GetTile(pos).Update(this, pos);
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


        public void Draw(SpriteBatch sb, GameTime gameTime)
        {
            var playerPos = Player.Position.ToTilePosition();
            var dist = 15;
            var beginX = Math.Max(0, playerPos.X - dist + 1);
            var beginY = Math.Max(0, playerPos.Y - dist + 1);
            var endX = Math.Min(W, playerPos.X + dist + 1);
            var endY = Math.Min(H, playerPos.Y + dist + 1);

            for (int x = beginX; x < endX; x++)
            {
                for (int y = beginY; y < endY; y++)
                {
                    var pos = new TilePosition(x, y);
                    GetTile(pos).Draw(sb, gameTime, this, pos);
                    //sb.DrawRectangle(new Rectangle(pos.X * ConstVal.TileSize, pos.Y * ConstVal.TileSize, ConstVal.TileSize, ConstVal.TileSize), new Color(255,255,255,100));
                }
            }

            Entities.Sort((a, b) => a.Position.X.CompareTo(b.Position.Y));

            foreach (var e in Entities)
            {
                e.Draw(sb, gameTime);
            }
        }

        public static bool Save(Level level, string fileName)
        {
            // Todo: level saving.
            return false;
        }

        public static Level Load(string fileName)
        {
            // TODO: level loading.
            return null;
        }
    }
}

using Maker.Hevadea.Enum;
using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.Items;
using Maker.Hevadea.Game.Registry;
using Maker.Rise.Ressource;
using Maker.Rise.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Maker.Hevadea.Game.Tiles
{
    public class Tile
    {
        public readonly byte ID;
        public Sprite Sprite;
        public bool BackgroundDirt = true;
        public Sprite DirtSprite;

        public Tile(byte id)
        {
            ID = id;
            if (TILES.ById[id] != null) throw new Exception($"Duplicate tile ids {ID}!");
            TILES.ById[ID] = this;
            Sprite = new Sprite(Ressources.tile_tiles, 0);
            DirtSprite = new Sprite(Ressources.tile_tiles, 0);
        }

        public virtual void Update(Level level, int tx, int ty)
        {
        }

        #region Tile rendering

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime, Level level, TilePosition pos)
        {
            var onScreen = pos.ToOnScreenPosition().ToVector2();

            bool u = level.GetTile(pos.X, pos.Y - 1) == this;
            bool d = level.GetTile(pos.X, pos.Y + 1) == this;
            bool l = level.GetTile(pos.X - 1, pos.Y) == this;
            bool r = level.GetTile(pos.X + 1, pos.Y) == this;

            bool ul = level.GetTile(pos.X - 1, pos.Y - 1) == this;
            bool ur = level.GetTile(pos.X + 1, pos.Y - 1) == this;
            bool dl = level.GetTile(pos.X - 1, pos.Y + 1) == this;
            bool dr = level.GetTile(pos.X + 1, pos.Y + 1) == this;

            DrawCorner(spriteBatch, l, ul, u, new Point(0, 0), new Point(0, 2), new Point(0, 3), new Point(2, 0),
                new Point(2, 2), (int) (onScreen.X + 0), (int) (onScreen.Y + 0));
            DrawCorner(spriteBatch, u, ur, r, new Point(1, 0), new Point(1, 2), new Point(0, 2), new Point(3, 0),
                new Point(2, 2), (int) (onScreen.X + 8), (int) (onScreen.Y + 0));

            DrawCorner(spriteBatch, r, dr, d, new Point(1, 1), new Point(1, 3), new Point(1, 2), new Point(3, 1),
                new Point(2, 2), (int) (onScreen.X + 8), (int) (onScreen.Y + 8));
            DrawCorner(spriteBatch, d, dl, l, new Point(0, 1), new Point(0, 3), new Point(1, 3), new Point(2, 1),
                new Point(2, 2), (int) (onScreen.X), (int) (onScreen.Y + 8));
        }

        public void DrawCorner(SpriteBatch spriteBatch,
            bool a, bool b, bool c,
            Point case1, Point case2, Point case3, Point case4, Point case5,
            int x, int y)
        {
            if (BackgroundDirt && !(a & b & c))
                DirtSprite.DrawSubSprite(spriteBatch, new Vector2(x, y), new Point(0, 0), Color.White);

            if (!a & !c)
            {
                Sprite.DrawSubSprite(spriteBatch, new Vector2(x, y), case1, Color.White);
            }
            else if (a & !c)
            {
                Sprite.DrawSubSprite(spriteBatch, new Vector2(x, y), case2, Color.White);
            }
            else if (!a & c)
            {
                Sprite.DrawSubSprite(spriteBatch, new Vector2(x, y), case3, Color.White);
            }
            else if (!b)
            {
                Sprite.DrawSubSprite(spriteBatch, new Vector2(x, y), case4, Color.White);
            }
            else
            {
                Sprite.DrawSubSprite(spriteBatch, new Vector2(x, y), case5 + case1, Color.White);
            }
        }

        #endregion

        #region Tile Interaction

        public virtual void Hurt(Entity e, float damages, TilePosition tilePosition, Direction attackDirection)
        {
        }

        public virtual void Interacte(Entity mob, Item item, TilePosition pos, Direction attackDirection)
        {
        }

        /* What happens when you are inside the tile (ex: lava) */
        public virtual void SteppedOn(Entity e, TilePosition pos)
        {
        }

        #endregion

        #region Properties

        public virtual bool IsBlocking(Entity e, TilePosition pos)
        {
            return false;
        }

        #endregion

        public static bool IsColiding(TilePosition tile, Entity e, int width, int height)
        {
            return Colision.Check(tile.X * ConstVal.TileSize,
                tile.Y * ConstVal.TileSize,
                ConstVal.TileSize, ConstVal.TileSize,
                e.X,
                e.Y,
                width, height);
        }

        public static bool IsColiding(TilePosition tile, float x, float y, int width, int height)
        {
            return Colision.Check(tile.X * ConstVal.TileSize,
                tile.Y * ConstVal.TileSize,
                ConstVal.TileSize, ConstVal.TileSize,
                x,
                y,
                width, height);
        }
    }
}
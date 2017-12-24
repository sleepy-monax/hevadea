using Maker.Rise.GameComponent.Ressource;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WorldOfImagination.Game.Entities;

namespace WorldOfImagination.Game.Tiles
{
    public class RockTile : Tile
    {
        Sprite rockSprite;
        public RockTile(byte id) : base(id)
        {
            rockSprite = new Sprite(Ressources.tile_tiles, 1);
        }

        public override bool CanPass(Level level, TilePosition pos, Entity e)
        {
            return false;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime, Level level, TilePosition pos)
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

            DrawCorner(spriteBatch, l, ul, u, new Point(0, 0), new Point(0, 2), new Point(0, 3), new Point(2, 0), new Point(2, 2), (int)(onScreen.X + 0), (int)(onScreen.Y + 0));
            DrawCorner(spriteBatch, u, ur, r, new Point(1, 0), new Point(1, 2), new Point(0, 2), new Point(3, 0), new Point(2, 2), (int)(onScreen.X + 16), (int)(onScreen.Y + 0));

            DrawCorner(spriteBatch, r, dr, d, new Point(1, 1), new Point(1, 3), new Point(1, 2), new Point(3, 1), new Point(2, 2), (int)(onScreen.X + 16), (int)(onScreen.Y + 16));
            DrawCorner(spriteBatch, d, dl, l, new Point(0, 1), new Point(0, 3), new Point(1, 3), new Point(2, 1), new Point(2, 2), (int)(onScreen.X), (int)(onScreen.Y + 16));
        }

        public void DrawCorner(SpriteBatch spriteBatch,
                               bool a, bool b, bool c,
                               Point case1, Point case2, Point case3, Point case4, Point case5,
                               int x, int y)
        {
            if (!a & !c)
            {
                rockSprite.DrawSubSprite(spriteBatch, new Vector2(x, y), case1, Color.White);
            }
            else if (a & !c)
            {
                rockSprite.DrawSubSprite(spriteBatch, new Vector2(x, y), case2, Color.White);
            }
            else if (!a & c)
            {
                rockSprite.DrawSubSprite(spriteBatch, new Vector2(x, y), case3, Color.White);
            }
            else if (!b)
            {
                rockSprite.DrawSubSprite(spriteBatch, new Vector2(x, y), case4, Color.White);
            }
            else
            {
                rockSprite.DrawSubSprite(spriteBatch, new Vector2(x, y), case5, Color.White);
            }
        }
    }
}

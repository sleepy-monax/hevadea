using Maker.Rise.GameComponent.Ressource;
using Maker.Rise.Utils;
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
            rockSprite = new Sprite(Ressources.tile_tiles, 4);
        }

        public override bool CanPass(Level level, TilePosition pos, Entity e)
        {
            return false;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime, Level level, TilePosition pos)
        {
            spriteBatch.Draw(Ressources.tile_tiles, 4, pos.ToOnScreenPosition().ToVector2(), Color.White);

            var onScreen = pos.ToOnScreenPosition().ToVector2();

            bool u = level.GetTile(pos.X, pos.Y - 1) == this;
            bool d = level.GetTile(pos.X, pos.Y + 1) == this;
            bool l = level.GetTile(pos.X - 1, pos.Y) == this;
            bool r = level.GetTile(pos.X + 1, pos.Y) == this;

            if (u)
            {
                rockSprite.DrawSubSprite(spriteBatch, new Vector2(onScreen.X + 16, onScreen.Y + 0), new Point(1, 1), Color.White);
            }

            if (d)
            {
                rockSprite.DrawSubSprite(spriteBatch, new Vector2(onScreen.X + 16, onScreen.Y + 32), new Point(1, 1), Color.White);
            }

            if (l)
            {
                rockSprite.DrawSubSprite(spriteBatch, new Vector2(onScreen.X + 0, onScreen.Y + 16), new Point(1, 1), Color.White);
            }

            if (r)
            {
                rockSprite.DrawSubSprite(spriteBatch, new Vector2(onScreen.X + 32, onScreen.Y + 16), new Point(1, 1), Color.White);
            }

        }
    }
}

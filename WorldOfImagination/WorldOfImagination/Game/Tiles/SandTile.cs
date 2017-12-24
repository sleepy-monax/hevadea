using Maker.Rise.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WorldOfImagination.Game.Tiles
{
    public class SandTile : Tile
    {
        public SandTile(byte id) : base(id)
        {
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime, Level level, TilePosition pos)
        {
            spriteBatch.Draw(Ressources.tile_tiles, 2, pos.ToOnScreenPosition().ToVector2(), Color.White);
        }
    }
}

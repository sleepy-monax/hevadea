using Maker.Rise.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Game.Tiles
{
    public class VoidTile : Tile
    {
        public VoidTile(byte id) : base(id)
        {
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime, Level level, TilePosition pos)
        {
            spriteBatch.FillRectangle(new Rectangle(pos.X * ConstVal.TileSize, pos.Y * ConstVal.TileSize, ConstVal.TileSize, ConstVal.TileSize), Color.Black);
        }
    }
}

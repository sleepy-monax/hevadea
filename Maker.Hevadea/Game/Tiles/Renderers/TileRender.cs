using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Game.Tiles.Renderers
{
    public abstract class TileRenderer
    {
        public abstract void Draw(SpriteBatch spriteBatch, Vector2 position, TileConection connection);
    }
}

using Hevadea.Worlds;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Tiles.Renderers
{
    public abstract class TileRender
    {
        public Tile Tile { get; set; }
        public abstract void Draw(SpriteBatch spriteBatch, TilePosition position, Level level);
    }
}

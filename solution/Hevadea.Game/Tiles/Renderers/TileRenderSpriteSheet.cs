using Hevadea.Framework.Graphic;
using Hevadea.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Tiles.Renderers
{
    public class TileRenderSpriteSheet : TileRenderConnected
    {
        private SpriteSheet spriteSheet;

        public TileRenderSpriteSheet(SpriteSheet spriteSheet)
        {
            this.spriteSheet = spriteSheet;
        }

        public override void Draw(SpriteBatch spriteBatch, Coordinates coords, Level level)
        {
            var connection = GetTileConnection(level, coords);
            var onScreenPosition = coords.ToOnScreenPosition();
            spriteSheet.Draw(spriteBatch, connection.ToByte(),
                new Rectangle(onScreenPosition.X, onScreenPosition.Y, Game.Unit, Game.Unit), Color.White);
        }
    }
}
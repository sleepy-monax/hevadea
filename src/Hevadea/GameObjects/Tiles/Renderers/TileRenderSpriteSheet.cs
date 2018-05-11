using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Worlds.Level;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.GameObjects.Tiles.Renderers
{
    public class TileRenderSpriteSheet : TileRenderConnected
    {
        private SpriteSheet spriteSheet;

        public TileRenderSpriteSheet(SpriteSheet spriteSheet)
        {
            this.spriteSheet = spriteSheet;
        }

        public override void Draw(SpriteBatch spriteBatch, TilePosition position, Level level)
        {
            var connection = GetTileConnection(level, position);
            var onScreenPosition = position.ToOnScreenPosition();
            spriteSheet.Draw(spriteBatch, connection.ToByte(), new Rectangle(onScreenPosition.X, onScreenPosition.Y, GLOBAL.Unit, GLOBAL.Unit), Color.White);
        }
    }
}
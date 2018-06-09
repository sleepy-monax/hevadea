using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Worlds;
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

        public override void Draw(SpriteBatch spriteBatch, Coordinates position, Level level)
        {
            var connection = GetTileConnection(level, position);
            var onScreenPosition = position.ToOnScreenPosition();
            spriteSheet.Draw(spriteBatch, connection.ToByte(), new Rectangle(onScreenPosition.X, onScreenPosition.Y, Game.Unit, Game.Unit), Color.White);
        }
    }
}
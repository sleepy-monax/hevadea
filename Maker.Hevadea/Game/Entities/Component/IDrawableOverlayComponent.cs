using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Game.Entities.Component
{
    public interface IDrawableOverlayComponent
    {
        void DrawOverlay(SpriteBatch spriteBatch, GameTime gameTime);
    }
}

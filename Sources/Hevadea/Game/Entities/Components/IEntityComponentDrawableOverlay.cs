using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Game.Entities.Components
{
    public interface IEntityComponentDrawableOverlay
    {
        void DrawOverlay(SpriteBatch spriteBatch, GameTime gameTime);
    }
}
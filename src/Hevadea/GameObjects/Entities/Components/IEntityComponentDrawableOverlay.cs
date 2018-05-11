using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.GameObjects.Entities.Components
{
    public interface IEntityComponentDrawableOverlay
    {
        void DrawOverlay(SpriteBatch spriteBatch, GameTime gameTime);
    }
}
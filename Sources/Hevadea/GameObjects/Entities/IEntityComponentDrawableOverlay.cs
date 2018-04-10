using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Entities
{
    public interface IEntityComponentDrawableOverlay
    {
        void DrawOverlay(SpriteBatch spriteBatch, GameTime gameTime);
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Entities
{
    public abstract class Renderer
    {
        public abstract void Render(Entity e, SpriteBatch spriteBatch, GameTime gameTime);
    }
}
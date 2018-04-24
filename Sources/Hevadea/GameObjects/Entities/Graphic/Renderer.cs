using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.GameObjects.Entities.Graphic
{
    public abstract class Renderer
    {
        public abstract void Render(Entity e, SpriteBatch spriteBatch, GameTime gameTime);
    }
}
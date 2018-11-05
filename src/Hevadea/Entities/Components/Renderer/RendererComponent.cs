using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Entities.Components.Renderer
{
    public abstract class RendererComponent : EntityComponent
    {
        public abstract void Render(SpriteBatch spriteBatch, Entity entity, Vector2 position, GameTime gameTime);
    }
}

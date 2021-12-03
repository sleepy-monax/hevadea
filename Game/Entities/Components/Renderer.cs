using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Entities.Components
{
    public abstract class Renderer : EntityComponent
    {
        public abstract void Render(SpriteBatch spriteBatch, Entity entity, Vector2 position, GameTime gameTime);
    }
}
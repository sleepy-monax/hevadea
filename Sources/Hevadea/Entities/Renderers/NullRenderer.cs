using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Entities.Renderers
{
    public class NullRenderer : Renderer
    {
        public override void Render(Entity e, SpriteBatch spriteBatch, GameTime gameTime) { }
    }
}
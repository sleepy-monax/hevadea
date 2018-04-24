using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.GameObjects.Entities.Graphic
{
    public class NullRenderer : Renderer
    {
        public override void Render(Entity e, SpriteBatch spriteBatch, GameTime gameTime) { }
    }
}
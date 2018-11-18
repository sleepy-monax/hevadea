using Hevadea.Framework.Graphic.SpriteAtlas;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Entities.Components.Renderer
{
    public class SpriteRenderer : RendererComponent
    {
        public Sprite Sprite { get; set; }
        public Vector2 Offset { get; set; }

        public override void Render(SpriteBatch spriteBatch, Entity entity, Vector2 position, GameTime gameTime)
        {
            Sprite?.Draw(spriteBatch, position + Offset - Sprite.Size / 2f, 1f, Color.White);
        }
    }
}

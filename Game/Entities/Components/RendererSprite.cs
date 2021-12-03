using Hevadea.Framework.Extension;
using Hevadea.Framework.Graphic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Entities.Components
{
    public class RendererSprite : Renderer
    {
        public _Sprite Sprite { get; set; }
        public Vector2 Offset { get; set; }

        public RendererSprite(_Sprite sprite)
        {
            Sprite = sprite;
            Offset = Vector2.Zero;
        }

        public RendererSprite(_Sprite sprite, Vector2 offset)
        {
            Sprite = sprite;
            Offset = offset;
        }

        public override void Render(SpriteBatch spriteBatch, Entity entity, Vector2 position, GameTime gameTime)
        {
            spriteBatch.DrawSprite(Sprite, position + Offset - Sprite.Size / 2f, Color.White);
        }
    }
}
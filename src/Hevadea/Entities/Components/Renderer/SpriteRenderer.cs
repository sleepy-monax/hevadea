using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Framework.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hevadea.Entities.Components.Renderer
{
    public class SpriteRenderer : RendererComponent
    {
        public Sprite Sprite { get; }
        public Vector2 Offset { get; }

        public SpriteRenderer(Sprite sprite) : this(sprite, Vector2.Zero) { }
        public SpriteRenderer(Sprite sprite, Vector2 offset)
        {
            Sprite = sprite;
            Offset = offset;
        }

        public override void Draw(SpriteBatch spriteBatch, Entity entity, Vector2 position, GameTime gameTime)
        {
            Sprite.Draw(spriteBatch, position + Offset - Sprite.Size / 2f, 1f, Color.White);
        }
    }
}

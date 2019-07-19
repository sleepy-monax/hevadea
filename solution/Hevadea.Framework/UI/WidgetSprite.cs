using Hevadea.Framework.Graphic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Framework.UI
{
    public class WidgetSprite : Widget
    {
        public Sprite Sprite { get; set; } = null;

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Sprite?.Draw(spriteBatch, Bound, Color.White);
        }
    }
}
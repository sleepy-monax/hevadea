using Hevadea.Framework.Extension;
using Hevadea.Framework.Graphic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Framework.UI
{
    public class WidgetLabel : Widget
    {
        public string Text { get; set; } = "label";
        public float TextSize { get; set; } = 1f;
        public SpriteFont Font { get; set; } = Rise.Ui.DefaultFont;
        public Color TextColor { get; set; } = Color.White;
        public TextAlignement TextAlignement { get; set; } = TextAlignement.Center;

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Text != null)
                spriteBatch.DrawString(Font, Text, Host, TextAlignement, TextStyle.DropShadow, TextColor,
                    Rise.Ui.ScaleFactor * TextSize);
        }
    }
}
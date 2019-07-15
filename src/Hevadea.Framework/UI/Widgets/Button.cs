using Hevadea.Framework.Extension;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Framework.UI.Widgets
{
    public class Button : Widget
    {
        public Color OverColor { get; set; } = ColorPalette.Accent;
        public Color IdleColor { get; set; } = ColorPalette.Border;
        public Color TextColor { get; set; } = Color.White;
        public SpriteFont Font { get; set; } = Rise.Ui.DefaultFont;
        public string Text { get; set; } = "Button";

        public Button()
        {
            UnitBound = new Rectangle(0, 0, 256, 48);
        }

        public Button(string text)
        {
            Text = text;
            UnitBound = new Rectangle(0, 0, 256, 48);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (MouseState == MouseState.Over)
            {
                spriteBatch.FillRectangle(Bound, ColorPalette.Border * 0.5f);
                spriteBatch.DrawRectangle(Bound, ColorPalette.Border, Scale(4));
            }
            else if (MouseState == MouseState.Down)
            {
                spriteBatch.FillRectangle(Bound, ColorPalette.Accent * 0.5f);
                spriteBatch.DrawRectangle(Bound, ColorPalette.Accent, Scale(4));
            }
            else
            {
                spriteBatch.FillRectangle(Bound, ColorPalette.Border * 0.25f);
                spriteBatch.DrawRectangle(Bound, ColorPalette.Border * 0.25f, Scale(4));
            }

            spriteBatch.DrawString(Font, Text, Host, TextAlignement.Center, TextStyle.DropShadow, TextColor, Rise.Ui.ScaleFactor);
        }
    }
}
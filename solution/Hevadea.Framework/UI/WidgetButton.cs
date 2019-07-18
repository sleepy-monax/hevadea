using Hevadea.Framework.Extension;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Framework.UI
{
    public class WidgetButton : Widget
    {
        public string Text { get; set; } = "Button";
        public SpriteFont Font { get; set; } = Rise.Ui.DefaultFont;
        public Style IdleStyle { get; set; } = Style.Idle;
        public Style OverStyle { get; set; } = Style.Over;
        public Style DownStyle { get; set; } = Style.Focus;

        public WidgetButton()
        {
            UnitBound = new Rectangle(0, 0, 256, 48);
        }

        public WidgetButton(string text)
        {
            Text = text;
            UnitBound = new Rectangle(0, 0, 256, 48);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (MouseState == MouseState.Over)
            {
                OverStyle.Draw(spriteBatch, UnitBound);
            }
            else if (MouseState == MouseState.Down)
            {
                DownStyle.Draw(spriteBatch, UnitBound);
            }
            else
            {
                IdleStyle.Draw(spriteBatch, UnitBound);
            }

            spriteBatch.DrawString(Font, Text, Host, TextAlignement.Center, TextStyle.DropShadow, Color.White, Rise.Ui.ScaleFactor);
        }
    }
}
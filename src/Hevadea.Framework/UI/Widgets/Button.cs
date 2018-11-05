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
        EasingManager _easing = new EasingManager { Speed = 10f };

        public Button()
        {
        }

        public Button(string text)
        {
            Text = text;
            UnitBound = new Rectangle(0, 0, 256, 48);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _easing.Show = MouseState == MouseState.Over || MouseState == MouseState.Down;
            _easing.Update(gameTime.ElapsedGameTime.TotalSeconds);
            
            spriteBatch.FillRectangle(Host, IdleColor * 0.05f * _easing.GetValueInv(EasingFunctions.Linear));
            spriteBatch.DrawRectangle(Host, IdleColor * _easing.GetValueInv(EasingFunctions.Linear), Scale(4));

			var bounceW = (int)(Host.Width * (_easing.GetValue(EasingFunctions.QuadraticEaseInOut) + 9f) / 10f);
			var bounceH = (int)(Host.Height * (_easing.GetValue(EasingFunctions.QuadraticEaseInOut) + 9f) / 10f);

            var rect = new Rectangle(Host.X + Host.Width / 2 - bounceW / 2,
                Host.Y + Host.Height / 2 - bounceH / 2,
                                     bounceW, bounceH);

            spriteBatch.FillRectangle(rect, OverColor * 0.5f * _easing.GetValue(EasingFunctions.Linear));
            spriteBatch.DrawRectangle(rect, OverColor * _easing.GetValue(EasingFunctions.Linear), Scale(4));

            var texSize = Font.MeasureString(Text);

            spriteBatch.DrawString(Font, Text, Host, DrawText.Alignement.Center, DrawText.TextStyle.DropShadow, TextColor, Rise.Ui.ScaleFactor);
        }
    }
}
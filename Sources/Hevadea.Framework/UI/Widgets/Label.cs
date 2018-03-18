using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Hevadea.Framework.Graphic;

namespace Hevadea.Framework.UI.Widgets
{
    public class Label : Widget
    {
        public string Text { get; set; } = "label";
        public float Scale { get; set; } = 1f;
        public SpriteFont Font { get; set; } = Rise.Ui.DefaultFont;
        public Color TextColor { get; set; } = Color.White;
        public DrawText.Alignement TextAlignement { get; set; } = DrawText.Alignement.Center;
        
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Text != null)
            {
                var texSize = Font.MeasureString(Text) * Scale;
                spriteBatch.DrawString(Font, Text, Host, TextAlignement, DrawText.TextStyle.DropShadow, TextColor);
            }
        }
    }
}
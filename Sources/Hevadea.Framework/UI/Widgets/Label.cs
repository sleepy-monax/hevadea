using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Framework.UI.Widgets
{
    public class Label : Widget
    {
        public string Text { get; set; } = "label";
        public float Scale { get; set; } = 1f;
        public SpriteFont Font { get; set; } = Rise.Ui.DefaultFont;
        public Color TextColor { get; set; } = Color.White;
        
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var texSize = Font.MeasureString(Text) * Scale;
            spriteBatch.DrawString(Font, Text, Bound.Center.ToVector2() - (new Vector2(texSize.X / 2, (texSize.Y / 2) - 4f * Scale)), Color.Black * 0.1f, 0f, Vector2.Zero, Scale, SpriteEffects.None, 1f);
            spriteBatch.DrawString(Font, Text, Bound.Center.ToVector2() - (new Vector2(texSize.X / 2, texSize.Y / 2)), TextColor, 0f, Vector2.Zero, Scale, SpriteEffects.None, 1f);
            base.Draw(spriteBatch, gameTime);
        }
    }
}
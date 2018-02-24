using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Rise.UI.Widgets
{
    public class Label : Widget
    {
        public string Text { get; set; } = "label";
        public SpriteFont Font { get; set; } = EngineRessources.FontArial;
        public Color TextColor { get; set; } = Color.White;
        
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var texSize = Font.MeasureString(Text);
            spriteBatch.DrawString(Font, Text, Bound.Center.ToVector2() - (new Vector2(texSize.X / 2, (texSize.Y / 2) - 4f)), Color.Black * 0.1f);
            spriteBatch.DrawString(Font, Text, Bound.Center.ToVector2() - (new Vector2(texSize.X / 2, texSize.Y / 2)), TextColor);
            base.Draw(spriteBatch, gameTime);
        }
    }
}
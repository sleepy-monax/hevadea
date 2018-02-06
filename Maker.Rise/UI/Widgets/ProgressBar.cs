using Maker.Rise.Extension;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Rise.UI.Widgets
{
    public class ProgressBar : Widget
    {
        public Color ProgressColor { get; set; } = Color.Gold;
        public Color Color { get; set; } = Color.White * 0.1f;
        public float Value { get; set; } = 0f;

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.FillRectangle(Host, Color);
            spriteBatch.FillRectangle( new Rectangle(Host.Location, new Point((int)(Host.Width * Value), Host.Height)), ProgressColor);
        }
    }
}
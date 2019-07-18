using Hevadea.Framework.Extension;
using Hevadea.Framework.Graphic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Framework.UI
{
    public class WidgetProgress : Widget
    {
        public Color ProgressColor { get; set; } = ColorPalette.Accent;
        public Color Color { get; set; } = Color.White * 0.5f;
        public float Value { get; set; } = 0f;

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.FillRectangle(Host, Color);
            spriteBatch.FillRectangle(new Rectangle(Host.Location, new Point((int) (Host.Width * Value), Host.Height)),
                ProgressColor);
        }
    }
}
using Hevadea.Framework.Extension;
using Hevadea.Framework.Graphic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Framework.UI
{
    public class Style
    {
        public static Style Idle => new Style
        {
            Border =
            {
                Color = ColorPalette.BorderIdle,
            },
            BackgroundColor = ColorPalette.BackgroundIdle
        };

        public static Style Focus => new Style
        {
            Border = {
                Color = ColorPalette.BorderDown
            },
            BackgroundColor = ColorPalette.BackgroundDown,
        };

        public static Style Over => new Style
        {
            Border = {
                Color = ColorPalette.BorderOver
            },
            BackgroundColor = ColorPalette.BackgroundOver

        };

        public Padding Padding { get; set; }
        public Border Border { get; set; }
        public Margin Margin { get; set; }

        public SpriteFont Font { get; set; }

        public Color Accent { get; set; }
        public float TextScale { get; set; }
        public Color TextColor { get; set; } = Color.White;
        public Color BackgroundColor { get; set; }

        public Style()
        {
            Margin = new Margin(0);
            Border = new Border(4);
            Padding = new Padding(0);

            TextColor = Color.White;
            Accent = ColorPalette.Accent;
            Font = Rise.Ui.DefaultFont;
        }

        public Rectangle GetContent(Rectangle rectangle)
        {
            return Margin.Apply(Border.Apply(Padding.Apply(rectangle)));
        }

        public void Draw(SpriteBatch spriteBatch, RectangleF destination)
        {
            spriteBatch.DrawSpacing(Widget.Scale(Margin), Widget.Scale(destination), Margin.Color);
            if (Rise.Debug.UI)
            {
                spriteBatch.DrawSpacing(Widget.Scale(Margin), Widget.Scale(destination), Color.Cyan * 0.5f);
            }
            destination = Margin.Apply(destination);

            spriteBatch.DrawSpacing(Widget.Scale(Border), Widget.Scale(destination), Border.Color);
            destination = Border.Apply(destination);

            spriteBatch.FillRectangle(Widget.Scale(destination), BackgroundColor);

            if (Rise.Debug.UI)
            {
                spriteBatch.DrawSpacing(Widget.Scale(Padding), Widget.Scale(destination), Color.Magenta * 0.5f);
            }
        }
    }
}
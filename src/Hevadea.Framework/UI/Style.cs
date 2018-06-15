using Hevadea.Framework.Graphic;
using Hevadea.Framework.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Framework.UI
{
    public class Margins
    {
        public int All { set { Top = Bottom = Left = Right = value; } }
        public int Vertical { set { Top = Bottom = value; } }
        public int Horizontal { set { Left = Right = value; } }

        public int Top { get; set; } = 0;
        public int Bottom { get; set; } = 0;
        public int Left { get; set; } = 0;
        public int Right { get; set; } = 0;

        public Margins()
        {
        }

        public Margins(int all)
        {
            Top = Bottom = Left = Right = all;
        }

        public Margins(int horizontal, int vertical)
        {
            Top = Bottom = vertical;
            Left = Right = horizontal;
        }

        public Margins(int top, int bottom, int left, int right)
        {
            Top = top;
            Bottom = bottom;
            Left = left;
            Right = right;
        }

        public Rectangle Apply(Rectangle rect)
        {
            return new Rectangle(rect.X + Left, rect.Y + Top, rect.Width - Left - Right, rect.Height - Top - Bottom);
        }

        public static Margins Lerp(Margins from, Margins to, float v)
        {
            v = Mathf.Clamp01(v);

            return new Margins((int)Mathf.Lerp(from.Top, to.Top, v),
                               (int)Mathf.Lerp(from.Bottom, to.Bottom, v),
                               (int)Mathf.Lerp(from.Left, to.Left, v),
                               (int)Mathf.Lerp(from.Right, to.Right, v));
        }
    }

    public class Border
    {
        public Margins Margins { get; set; } = new Margins(0);
        public Color Color { get; set; } = ColorPalette.Border;

        public Border()
        {
        }

        public Border(Margins margins, Color color)
        {
            Margins = margins;
            Color = color;
        }

        public static Border Lerp(Border from, Border to, float v)
        {
            v = Mathf.Clamp01(v);
            return new Border(Margins.Lerp(from.Margins, to.Margins, v), Color.Lerp(from.Color, to.Color, v));
        }
    }

    public class Style
    {

        public static Style Empty => new Style(); 

        public Margins Margin { get; set; }
        public Border Border { get; set; }
        public Margins Padding { get; set; }

        public SpriteFont Font { get; set; }

        public Color TextColor { get; set; }
        public Color Background { get; set; }

        public Style()
        {
            Margin = new Margins();
            Border = new Border() { Margins = new Margins(4) };
            Padding = new Margins();

            Background = ColorPalette.Background;
            TextColor = Color.White;
            Font = Rise.Ui.DefaultFont;
        }

        public Rectangle GetContent(Rectangle rectangle)
            => Margin.Apply(Border.Margins.Apply(Padding.Apply(rectangle)));

        public void Draw(SpriteBatch spriteBatch, Rectangle destination)
        {
            // Margin
            destination = Margin.Apply(destination);

            // Borders
            spriteBatch.FillRectangle(Widget.Scale(destination), Border.Color);
            destination = Border.Margins.Apply(destination);

            // Padding
            destination = Padding.Apply(destination);

            spriteBatch.FillRectangle(Widget.Scale(destination), Background);
        }
    }
}
using Hevadea.Framework.Graphic;
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

        public Margins() { }

        public Margins(int all)
        {
            Top = Bottom = Left = Right = all;
        }

        public Margins(int horizontal, int vertical)
        {
            Top = Bottom = vertical;
            Left = Right = horizontal;
        }

        public Margins(int up, int down, int left, int right)
        {
            Top = up;
            Bottom = down;
            Left = left;
            Right = right;
        }

        public Rectangle Apply(Rectangle rect)
        {
            return new Rectangle(rect.X + Left, rect.Y + Top, rect.Width - Left - Right, rect.Height - Top - Bottom);
        }
    }


    public class Box
    {
        public Margins Margins { get; set; } = new Margins(0);
        public Color Color { get; set; } = Color.Transparent;

        public Box() { }

        public Box(Margins margins, Color color)
        {
            Margins = margins;
            Color = color;
        }
    }

    public class Style
    {

        public Box Margin { get; set; }
        public Box Border  { get; set; }
        public Box Padding { get; set; }

        public SpriteFont Font;

        public Color TextColor;
        public Color Background;

        public Style()
        {
            Margin = new Box();
            Border = new Box();
            Padding = new Box();
        }

        public Rectangle GetContent(Rectangle rectangle)
            => Margin.Margins.Apply(Border.Margins.Apply(Padding.Margins.Apply(rectangle)));

        public void Draw(SpriteBatch spriteBatch, Rectangle destination)
        {
            // Margin
            spriteBatch.FillRectangle(destination, Margin.Color);
            destination = Margin.Margins.Apply(destination);

            // Borders
            spriteBatch.FillRectangle(destination, Border.Color);
            destination = Border.Margins.Apply(destination);

            // Padding
            spriteBatch.FillRectangle(destination, Padding.Color);
            destination = Padding.Margins.Apply(destination);

            spriteBatch.FillRectangle(destination, Background);
        }
    }
}

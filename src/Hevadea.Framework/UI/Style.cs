using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Framework.UI
{
	public class BoxElement
	{
		public int All { set { Top = Bottom = Left = Right = value; } }
		public int Vertical { set { Top = Bottom = value; } }
		public int Horizontal { set { Left = Right = value; } }

		public int Top { get; set; } = 0;
		public int Bottom { get; set; } = 0;
		public int Left { get; set; } = 0;
		public int Right { get; set; } = 0;

		public BoxElement(){}

		public BoxElement(int all)
        {
            Top = Bottom = Left = Right = all;
        }

		public BoxElement(int horizontal, int vertical)
        {
            Top = Bottom = vertical;
            Left = Right = horizontal;
        }

		public BoxElement(int up, int down, int left, int right)
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
        
    public class Style
    {

		public BoxElement Marging;
		public BoxElement Border;
		public BoxElement Padding;

        public SpriteFont Font;

		public Color BackgroundColor;
		public Color BorderColor;
		public Color TextColor;

		public Style()
		{
			Marging = new BoxElement();
			Border = new BoxElement();
			Padding = new BoxElement();
		}
    }
}

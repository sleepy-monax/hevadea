using Microsoft.Xna.Framework;

namespace Hevadea.Framework
{
    public class Spacing
    {
        public float All
        {
            set => Top = Bottom = Left = Right = value;
        }

        public float Vertical
        {
            set => Top = Bottom = value;
        }

        public float Horizontal
        {
            set => Left = Right = value;
        }

        public float Top { get; set; } = 0;
        public float Bottom { get; set; } = 0;
        public float Left { get; set; } = 0;
        public float Right { get; set; } = 0;

        public Spacing()
        {
        }

        public Spacing(float all)
        {
            Top = Bottom = Left = Right = all;
        }

        public Spacing(float horizontal, float vertical)
        {
            Top = Bottom = vertical;
            Left = Right = horizontal;
        }

        public Spacing(float top, float bottom, float left, float right)
        {
            Top = top;
            Bottom = bottom;
            Left = left;
            Right = right;
        }

        public Rectangle Apply(Rectangle rect)
        {
            return new Rectangle((int)(rect.X + Left), (int)(rect.Y + Top), (int)(rect.Width - Left - Right), (int)(rect.Height - Top - Bottom));
        }

        public RectangleF Apply(RectangleF rect)
        {
            return new RectangleF(rect.X + Left, rect.Y + Top, rect.Width - Left - Right, rect.Height - Top - Bottom);
        }

        public static Spacing Lerp(Spacing from, Spacing to, float v)
        {
            v = Mathf.Clamp01(v);

            return new Spacing((int) Mathf.Lerp(from.Top, to.Top, v),
                (int) Mathf.Lerp(from.Bottom, to.Bottom, v),
                (int) Mathf.Lerp(from.Left, to.Left, v),
                (int) Mathf.Lerp(from.Right, to.Right, v));
        }
    }
}
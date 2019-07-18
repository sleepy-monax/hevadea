using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hevadea.Framework.Extension
{
    public enum Anchor
    {
        TopLeft,
        Top,
        TopRight,
        Left,
        Center,
        Right,
        BottomLeft,
        Bottom,
        BottomRight
    }

    public static class RectangleExtension
    {
        public static Rectangle Padding(this Rectangle rect, int top, int bottom, int left, int right)
        {
            return new Rectangle(rect.Location + new Point(left, top),
                rect.Size - new Point(left + right, top + bottom));
        }

        public static Rectangle Padding(this Rectangle rect, int all)
        {
            return rect.Padding(all, all, all, all);
        }

        public static Rectangle Marging(this Rectangle rect, int top, int bottom, int left, int right)
        {
            return new Rectangle(rect.Location - new Point(left, top),
                rect.Size + new Point(left + right, top + bottom));
        }

        public static Rectangle Marging(this Rectangle rect, int all)
        {
            return rect.Marging(all, all, all, all);
        }

        public static Point GetAnchorPoint(this Rectangle rect, Anchor anchor)
        {
            switch (anchor)
            {
                case Anchor.TopLeft: return new Point(0, 0);
                case Anchor.Top: return new Point(rect.Width / 2, 0);
                case Anchor.TopRight: return new Point(rect.Width, 0);
                case Anchor.Left: return new Point(0, rect.Height / 2);
                case Anchor.Center: return new Point(rect.Width / 2, rect.Height / 2);
                case Anchor.Right: return new Point(rect.Width, rect.Height / 2);
                case Anchor.BottomLeft: return new Point(0, rect.Height);
                case Anchor.Bottom: return new Point(rect.Width / 2, rect.Height);
                case Anchor.BottomRight: return new Point(rect.Width, rect.Height);
                default:
                    throw new ArgumentOutOfRangeException(nameof(anchor), anchor, null);
            }
        }
    }
}
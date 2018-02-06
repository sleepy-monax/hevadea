using System;
using Maker.Rise.UI.Widgets;
using Microsoft.Xna.Framework;

namespace Maker.Rise.Extension
{
    public static class RectangleExtension
    {
        public static Point GetAnchorPoint(this Rectangle rect, Anchor anchor)
        {
            switch (anchor)
            {
                case Anchor.TopLeft: return new Point(0, 0);                   
                case Anchor.Top: return new Point(rect.Width / 2, 0);
                case Anchor.TopRight: return new Point(rect.Width, 0);
                case Anchor.Left: return new Point(0, rect.Height / 2);
                case Anchor.Center: return new Point(rect.Width / 2, rect.Height / 2);
                case Anchor.Right: return new Point(rect.Width, rect.Width / 2);
                case Anchor.BottomLeft: return new Point(0, rect.Height);
                case Anchor.Bottom: return new Point(rect.Width / 2, rect.Height);
                case Anchor.BottomRight: return new Point(rect.Width, rect.Height);
                default:
                    throw new ArgumentOutOfRangeException(nameof(anchor), anchor, null);
            }
        }
    }
}
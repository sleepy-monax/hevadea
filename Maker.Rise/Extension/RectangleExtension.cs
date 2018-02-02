using System;
using Maker.Rise.Enums;
using Microsoft.Xna.Framework;

namespace Maker.Rise.Extension
{
    public static class RectangleExtension
    {
        public static Point GetAnchorPoint(this Rectangle rect, Anchor anchor)
        {
            switch (anchor)
            {
                case Anchor.TopLeft: return new Point(rect.Left, rect.Top);                   
                case Anchor.Top: return new Point(rect.Center.X, rect.Top);
                case Anchor.TopRight: return new Point(rect.Right, rect.Top);
                case Anchor.Left: return new Point(rect.Left, rect.Center.Y);
                case Anchor.Center: return rect.Center;
                case Anchor.Right: return new Point(rect.Right, rect.Center.Y);
                case Anchor.BottomLeft: return new Point(rect.Left, rect.Bottom);
                case Anchor.Bottom: return new Point(rect.Center.X, rect.Bottom);
                case Anchor.BottomRight: return new Point(rect.Right, rect.Bottom);
                default:
                    throw new ArgumentOutOfRangeException(nameof(anchor), anchor, null);
            }
        }
    }
}
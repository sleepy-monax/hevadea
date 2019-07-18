using Microsoft.Xna.Framework;
using System;
using System.Globalization;

namespace Hevadea.Framework
{
    public struct RectangleF
    {
        public static readonly RectangleF Empty = new RectangleF();

        public RectangleF(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public RectangleF(Vector2 location, Vector2 size)
        {
            X = location.X;
            Y = location.Y;
            Width = size.X;
            Height = size.Y;
        }

        public static RectangleF FromLtrb(float left, float top, float right, float bottom)
        {
            return new RectangleF(left,
                top,
                right - left,
                bottom - top);
        }

        public Vector2 Location
        {
            get => new Vector2(X, Y);
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        public Vector2 Size
        {
            get => new Vector2(Width, Height);
            set
            {
                Width = value.X;
                Height = value.Y;
            }
        }

        public float X { get; set; }

        public float Y { get; set; }

        public float Width { get; set; }

        public float Height { get; set; }

        public float Left => X;

        public float Top => Y;

        public float Right => X + Width;

        public float Bottom => Y + Height;

        public bool IsEmpty => Width <= 0 || Height <= 0;

        public override bool Equals(object obj)
        {
            if (!(obj is RectangleF))
                return false;

            var comp = (RectangleF) obj;

            return comp.X == X &&
                   comp.Y == Y &&
                   comp.Width == Width &&
                   comp.Height == Height;
        }

        public static bool operator ==(RectangleF left, RectangleF right)
        {
            return left.X == right.X
                   && left.Y == right.Y
                   && left.Width == right.Width
                   && left.Height == right.Height;
        }

        public static bool operator !=(RectangleF left, RectangleF right)
        {
            return !(left == right);
        }

        public bool Contains(float x, float y)
        {
            return X <= x &&
                   x < X + Width &&
                   Y <= y &&
                   y < Y + Height;
        }

        public bool Contains(Vector2 pt)
        {
            return Contains(pt.X, pt.Y);
        }

        public bool Contains(RectangleF rect)
        {
            return X <= rect.X &&
                   rect.X + rect.Width <= X + Width &&
                   Y <= rect.Y &&
                   rect.Y + rect.Height <= Y + Height;
        }

        public override int GetHashCode()
        {
            return unchecked((int) ((uint) X ^
                                    (((uint) Y << 13) | ((uint) Y >> 19)) ^
                                    (((uint) Width << 26) | ((uint) Width >> 6)) ^
                                    (((uint) Height << 7) | ((uint) Height >> 25))));
        }

        public RectangleF Inflate(float x, float y)
        {
            return new RectangleF(X - x / 2, Y - y / 2, Width + x, Height + y);
        }

        public void Intersect(RectangleF rect)
        {
            var result = Intersect(rect, this);

            X = result.X;
            Y = result.Y;
            Width = result.Width;
            Height = result.Height;
        }

        public static RectangleF Intersect(RectangleF a, RectangleF b)
        {
            var x1 = Math.Max(a.X, b.X);
            var x2 = Math.Min(a.X + a.Width, b.X + b.Width);
            var y1 = Math.Max(a.Y, b.Y);
            var y2 = Math.Min(a.Y + a.Height, b.Y + b.Height);

            if (x2 >= x1
                && y2 >= y1)
                return new RectangleF(x1, y1, x2 - x1, y2 - y1);
            return Empty;
        }

        public bool IntersectsWith(RectangleF rect)
        {
            return rect.X < X + Width &&
                   X < rect.X + rect.Width &&
                   rect.Y < Y + Height &&
                   Y < rect.Y + rect.Height;
        }

        public static RectangleF Union(RectangleF a, RectangleF b)
        {
            var x1 = Math.Min(a.X, b.X);
            var x2 = Math.Max(a.X + a.Width, b.X + b.Width);
            var y1 = Math.Min(a.Y, b.Y);
            var y2 = Math.Max(a.Y + a.Height, b.Y + b.Height);

            return new RectangleF(x1, y1, x2 - x1, y2 - y1);
        }

        public RectangleF Offset(Vector2 off)
        {
            return Offset(off.X, off.Y);
        }

        public RectangleF Offset(float ox, float oy)
        {
            return new RectangleF(X + ox, Y + oy, Width, Height);
        }

        public static implicit operator RectangleF(Rectangle r)
        {
            return new RectangleF(r.X, r.Y, r.Width, r.Height);
        }

        public override string ToString()
        {
            return "{X=" + X.ToString(CultureInfo.CurrentCulture) + ",Y=" + Y.ToString(CultureInfo.CurrentCulture) +
                   ",Width=" + Width.ToString(CultureInfo.CurrentCulture) +
                   ",Height=" + Height.ToString(CultureInfo.CurrentCulture) + "}";
        }
    }
}
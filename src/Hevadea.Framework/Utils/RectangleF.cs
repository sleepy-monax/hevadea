using Microsoft.Xna.Framework;
using System;
using System.Globalization;

namespace Hevadea.Framework.Utils
{
    public struct RectangleF
    {
        public static readonly RectangleF Empty = new RectangleF();

        public RectangleF(float x, float y, float width, float height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        public RectangleF(Vector2 location, Vector2 size)
        {
            this.X = location.X;
            this.Y = location.Y;
            this.Width = size.X;
            this.Height = size.Y;
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
                this.Width = value.X;
                this.Height = value.Y;
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

        public bool IsEmpty => (Width <= 0) || (Height <= 0);

        public override bool Equals(object obj)
        {
            if (!(obj is RectangleF))
                return false;

            var comp = (RectangleF)obj;

            return (comp.X == this.X) &&
                   (comp.Y == this.Y) &&
                   (comp.Width == this.Width) &&
                   (comp.Height == this.Height);
        }

        public static bool operator ==(RectangleF left, RectangleF right)
        {
            return (left.X == right.X
                     && left.Y == right.Y
                     && left.Width == right.Width
                     && left.Height == right.Height);
        }

        public static bool operator !=(RectangleF left, RectangleF right)
        {
            return !(left == right);
        }

        public bool Contains(float x, float y)
        {
            return this.X <= x &&
            x < this.X + this.Width &&
            this.Y <= y &&
            y < this.Y + this.Height;
        }

        public bool Contains(Vector2 pt)
        {
            return Contains(pt.X, pt.Y);
        }

        public bool Contains(RectangleF rect)
        {
            return (this.X <= rect.X) &&
                   ((rect.X + rect.Width) <= (this.X + this.Width)) &&
                   (this.Y <= rect.Y) &&
                   ((rect.Y + rect.Height) <= (this.Y + this.Height));
        }

        public override int GetHashCode()
        {
            return unchecked((int)((UInt32)X ^
            (((UInt32)Y << 13) | ((UInt32)Y >> 19)) ^
            (((UInt32)Width << 26) | ((UInt32)Width >> 6)) ^
            (((UInt32)Height << 7) | ((UInt32)Height >> 25))));
        }

        public void Inflate(float x, float y)
        {
            this.X -= x;
            this.Y -= y;
            this.Width += 2 * x;
            this.Height += 2 * y;
        }

        public void Inflate(Point size)
        {
            Inflate(size.X, size.Y);
        }

        public static RectangleF Inflate(RectangleF rect, float x, float y)
        {
            var r = rect;
            r.Inflate(x, y);
            return r;
        }

        public void Intersect(RectangleF rect)
        {
            var result = RectangleF.Intersect(rect, this);

            this.X = result.X;
            this.Y = result.Y;
            this.Width = result.Width;
            this.Height = result.Height;
        }

        public static RectangleF Intersect(RectangleF a, RectangleF b)
        {
            var x1 = Math.Max(a.X, b.X);
            var x2 = Math.Min(a.X + a.Width, b.X + b.Width);
            var y1 = Math.Max(a.Y, b.Y);
            var y2 = Math.Min(a.Y + a.Height, b.Y + b.Height);

            if (x2 >= x1
                && y2 >= y1)
            {
                return new RectangleF(x1, y1, x2 - x1, y2 - y1);
            }
            return RectangleF.Empty;
        }

        public bool IntersectsWith(RectangleF rect)
        {
            return (rect.X < this.X + this.Width) &&
                   (this.X < (rect.X + rect.Width)) &&
                   (rect.Y < this.Y + this.Height) &&
                   (this.Y < rect.Y + rect.Height);
        }

        public static RectangleF Union(RectangleF a, RectangleF b)
        {
            var x1 = Math.Min(a.X, b.X);
            var x2 = Math.Max(a.X + a.Width, b.X + b.Width);
            var y1 = Math.Min(a.Y, b.Y);
            var y2 = Math.Max(a.Y + a.Height, b.Y + b.Height);

            return new RectangleF(x1, y1, x2 - x1, y2 - y1);
        }

        public void Offset(Vector2 pos)
        {
            Offset(pos.X, pos.Y);
        }

        public void Offset(float x, float y)
        {
            this.X += x;
            this.Y += y;
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
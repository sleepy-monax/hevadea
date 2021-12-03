using Microsoft.Xna.Framework;

namespace Hevadea.Framework
{
    public struct CircleF
    {
        public float X { get; }
        public float Y { get; }
        public float Radius { get; }

        public Vector2 Position => new Vector2(X, Y);
        public RectangleF Bound => new RectangleF(X - Radius, Y - Radius, Radius * 2, Radius * 2);

        public CircleF(float x, float y, float radius)
        {
            X = x;
            Y = y;
            Radius = radius;
        }

        public CircleF(Vector2 pos, float radius)
        {
            X = pos.X;
            Y = pos.Y;
            Radius = radius;
        }

        public bool Containe(Vector2 p)
        {
            return Mathf.Distance(Position, p) <= Radius;
        }
    }
}
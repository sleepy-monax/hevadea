using Hevadea.Game;
using Microsoft.Xna.Framework;

namespace Hevadea
{
    public static class Extensions
    {
        public static Point ToPoint(this Direction dir)
        {
            switch (dir)
            {
                case Direction.Up:
                    return new Point(0, -1);
                case Direction.Right:
                    return new Point(1, 0);
                case Direction.Down:
                    return new Point(0, 1);
                case Direction.Left:
                    return new Point(-1, 0);
                default:
                    return new Point(0, 0);
            }
        }

        public static Direction ToDirection(this Vector2 vec)
        {
            if (-vec.Y > 0.3) return Direction.Up;
            if (vec.Y > 0.3) return Direction.Down;
            if (-vec.X > 0.3) return Direction.Left;
            if (vec.X > 0.3) return Direction.Right;
            return Direction.Down;
        }
    }
}
using Maker.Hevadea.Game;
using Microsoft.Xna.Framework;

namespace Maker.Hevadea
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
    }
}
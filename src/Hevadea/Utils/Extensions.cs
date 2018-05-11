using Microsoft.Xna.Framework;

namespace Hevadea.Utils
{
    public static partial class Extensions
    {
        public static Direction ToDirection(this Vector2 vec)
        {
            if (-vec.Y > 0.3) return Direction.North;
            if (vec.Y > 0.3) return Direction.South;
            if (-vec.X > 0.3) return Direction.West;
            if (vec.X > 0.3) return Direction.East;
            return Direction.South;
        }
    }
}
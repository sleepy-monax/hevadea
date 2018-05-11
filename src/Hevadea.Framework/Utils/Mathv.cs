using Microsoft.Xna.Framework;

namespace Hevadea.Framework.Utils
{
    public static class Mathv
    {
        public static Vector2 RadianToVector2(float radian)
        {
            return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
        }

        public static Vector2 RadianToVector2(float radian, float length)
        {
            return RadianToVector2(radian) * length;
        }

        public static Vector2 DegreeToVector2(float degree)
        {
            return RadianToVector2(degree * Mathf.Deg2Rad);
        }

        public static Vector2 DegreeToVector2(float degree, float length)
        {
            return RadianToVector2(degree * Mathf.Deg2Rad) * length;
        }

        public static Vector2 VectorTo(this Vector2 p0, Vector2 p1, bool normalize)
        {
            var dir = new Vector2(p0.X - p1.X, p0.Y - p1.Y);

            if (dir.Length() > 1f && normalize)
            {
                dir.Normalize();
            }

            return dir;
        }
    }
}
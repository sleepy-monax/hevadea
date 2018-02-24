using Hevadea.Framework.Utils;
using Microsoft.Xna.Framework;

namespace Maker.Rise
{
    public static class MathHelpers
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
    }
}

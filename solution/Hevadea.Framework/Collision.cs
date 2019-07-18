using Microsoft.Xna.Framework;

namespace Hevadea.Framework
{
    public static class Collision
    {
        public static bool Colliding(float x1, float y1, float w1, float h1, float x2, float y2, float w2, float h2)
        {
            return x1 < x2 + w2 &&
                   x2 < x1 + w1 &&
                   y1 < y2 + h2 &&
                   y2 < y1 + h1;
        }
    }
}
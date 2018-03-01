using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Framework.Utils
{
    public static class Texture2DUtils
    {

        public static Vector2 GetCenter(this Texture2D tex)
        {
            return tex.Bounds.Center.ToVector2();
        }

    }
}
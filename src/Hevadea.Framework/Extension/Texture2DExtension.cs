using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Framework.Extension
{
    public static class Texture2DExtension
    {
        public static Vector2 GetCenter(this Texture2D tex)
        {
            return tex.Bounds.Center.ToVector2();
        }

        public static void Clear(this Texture2D tex, Color c)
        {
            var size = tex.Width * tex.Height;
            var data = new Color[size];

            for (int i = 0; i < size; i++)
            {
                data[i] = c;
            }

            tex.SetData(data);
        }

        public static void SetPixel(this Texture2D tex, int x, int y, Color c)
        {
            if (x >= 0 && x < tex.Width && y >= 0 && y < tex.Height)
            {
                var r = new Rectangle(x, y, 1, 1);
                var color = new Color[] { c };

                tex.SetData(0, r, color, 0, 1);
            }
        }
    }
}
